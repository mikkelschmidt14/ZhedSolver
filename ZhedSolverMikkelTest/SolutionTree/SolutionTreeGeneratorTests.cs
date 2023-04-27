using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel;
using ZhedSolverMikkel.Board;
using ZhedSolverMikkel.SolutionTree;

namespace ZhedSolverMikkelTest.SolutionTree
{
    [TestClass]
    public class SolutionTreeGeneratorTests
    {
        #region GenerateMultiNodes

        [TestMethod]
        public void GenerateMultiNodes_Always_GetPerpendicularStepsToLineSegmentIsInvoked()
        {
            var step = new SolutionStep(new Position(5, 2), Direction.Left);
            var towardsPosition = new Position(1, 2);
            var singleNode = new SingleNode(step, towardsPosition, null);

            var perpendicularSteps = new List<SolutionStep>
            {
                new(new Position(1,1), Direction.Right),
            };

            var valueOfValueCell = 2;
            var valueCell = new ValueCell(0, 0, valueOfValueCell);

            var boardMock = new Mock<IBoard>();
            boardMock.Setup(m => m.GetStepsFromBehind(It.IsAny<Position>(), It.IsAny<Position>(), It.IsAny<IDirectionResolver>()))
                .Returns(new List<SolutionStep>());

            boardMock.Setup(m => m.GetPerpendicularStepsToLineSegment(It.IsAny<Position>(), It.IsAny<Position>(), It.IsAny<IDirectionResolver>()))
                .Returns(perpendicularSteps);

            boardMock.Setup(m => m.GetValueCell(It.IsAny<Position>()))
                .Returns(valueCell);

            var stepPermuterMock = new Mock<IStepCombinator>();
            stepPermuterMock.Setup(m => m.GetCombinations(It.IsAny<List<SolutionStep>>(), It.IsAny<int>()))
                .Returns(new List<List<SolutionStep>>());

            var towardsPositionResolverMock = new Mock<ITowardsPositionResolver>();

            var rootGeneratorMock = new Mock<IRootGenerator>();

            var directionResolverMock = new Mock<IDirectionResolver>();

            var sut = new SolutionTreeGenerator(directionResolverMock.Object, stepPermuterMock.Object, towardsPositionResolverMock.Object, rootGeneratorMock.Object);
            sut.GenerateMultiNodes(boardMock.Object, singleNode);

            boardMock.Verify(b => b.GetPerpendicularStepsToLineSegment(step.Position, towardsPosition, It.IsAny<IDirectionResolver>()));
        }

        [TestMethod]
        public void GenerateMultiNodes_Always_EmptyDistanceBetweenIsInvoked()
        {
            var step = new SolutionStep(new Position(5, 2), Direction.Left);
            var towardsPosition = new Position(1, 2);
            var singleNode = new SingleNode(step, towardsPosition, null);

            var perpendicularSteps = new List<SolutionStep>
            {
                new(new Position(1,1), Direction.Right),
            };

            var valueOfValueCell = 2;
            var valueCell = new ValueCell(0, 0, valueOfValueCell);

            var boardMock = new Mock<IBoard>();
            boardMock.Setup(m => m.GetStepsFromBehind(It.IsAny<Position>(), It.IsAny<Position>(), It.IsAny<IDirectionResolver>()))
                .Returns(new List<SolutionStep>());

            boardMock.Setup(m => m.GetPerpendicularStepsToLineSegment(It.IsAny<Position>(), It.IsAny<Position>(), It.IsAny<IDirectionResolver>()))
                .Returns(perpendicularSteps);

            boardMock.Setup(m => m.GetValueCell(It.IsAny<Position>()))
                .Returns(valueCell);

            var stepPermuterMock = new Mock<IStepCombinator>();
            stepPermuterMock.Setup(m => m.GetCombinations(It.IsAny<List<SolutionStep>>(), It.IsAny<int>()))
                .Returns(new List<List<SolutionStep>>());

            var towardsPositionResolverMock = new Mock<ITowardsPositionResolver>();

            var rootGeneratorMock = new Mock<IRootGenerator>();

            var directionResolverMock = new Mock<IDirectionResolver>();

            var sut = new SolutionTreeGenerator(directionResolverMock.Object, stepPermuterMock.Object, towardsPositionResolverMock.Object, rootGeneratorMock.Object);
            sut.GenerateMultiNodes(boardMock.Object, singleNode);

            boardMock.Verify(b => b.EmptyDistanceBetween(step.Position, towardsPosition));
        }

        [TestMethod]
        public void GenerateMultiNodes_Always_PermuteStepsIsInvoked()
        {
            var step = new SolutionStep(new Position(5, 2), Direction.Left);
            var towardsPosition = new Position(1, 2);
            var singleNode = new SingleNode(step, towardsPosition, null);

            var perpendicularSteps = new List<SolutionStep>
            {
                new(new Position(1,1), Direction.Right),
            };

            var distance = 5;
            var valueOfValueCell = 2;
            var valueCell = new ValueCell(0, 0, valueOfValueCell);

            var boardMock = new Mock<IBoard>();
            boardMock.Setup(m => m.GetStepsFromBehind(It.IsAny<Position>(), It.IsAny<Position>(), It.IsAny<IDirectionResolver>()))
                .Returns(new List<SolutionStep>());

            boardMock.Setup(m => m.GetPerpendicularStepsToLineSegment(It.IsAny<Position>(), It.IsAny<Position>(), It.IsAny<IDirectionResolver>()))
                .Returns(perpendicularSteps);

            boardMock.Setup(m => m.EmptyDistanceBetween(It.IsAny<Position>(), It.IsAny<Position>()))
                .Returns(distance);

            boardMock.Setup(m => m.GetValueCell(It.IsAny<Position>()))
                .Returns(valueCell);

            var stepPermuterMock = new Mock<IStepCombinator>();
            stepPermuterMock.Setup(m => m.GetCombinations(It.IsAny<List<SolutionStep>>(), It.IsAny<int>()))
                .Returns(new List<List<SolutionStep>>());

            var towardsPositionResolverMock = new Mock<ITowardsPositionResolver>();

            var rootGeneratorMock = new Mock<IRootGenerator>();

            var directionResolverMock = new Mock<IDirectionResolver>();

            var sut = new SolutionTreeGenerator(directionResolverMock.Object, stepPermuterMock.Object, towardsPositionResolverMock.Object, rootGeneratorMock.Object);
            sut.GenerateMultiNodes(boardMock.Object, singleNode);

            stepPermuterMock.Verify(m => m.GetCombinations(perpendicularSteps, distance - valueOfValueCell));
        }

        #endregion
    }
}
