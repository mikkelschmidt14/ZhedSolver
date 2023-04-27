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
using ZhedSolverMikkelTest.Board;

namespace ZhedSolverMikkelTest.SolutionTree
{
    [TestClass]
    public class RootGeneratorTests
    {
        [TestMethod]
        public void GenerateRoots_OneIntersectingValueCell_RootPropertiesAreCorrect()
        {
            var boardString = "-----\n" +
                              "2--x-\n" +
                              "-----\n" +
                              "-----";

            var board = BoardCreator.CreateBoardFromString(boardString);

            var expectedDirection = Direction.Up;
            var expectedPosition = new Position(0, 1);

            var directionResolverMock = new Mock<IDirectionResolver>();
            directionResolverMock.Setup(m => m.ResolveDirection(It.IsAny<Position>(), It.IsAny<Position>()))
                .Returns(expectedDirection);


            var sut = new RootGenerator(directionResolverMock.Object);
            var result = sut.GenerateRoots(board);

            using (new AssertionScope())
            {
                result.Should().HaveCount(1);

                foreach (var item in result)
                {
                    item.Parent.Should().BeNull();
                    item.Step.Direction.Should().Be(expectedDirection);
                    item.Step.Position.Should().Be(expectedPosition);
                    item.TowardsPosition.Should().Be(board.GoalPosition);
                    item.Children.Should().BeEmpty();
                }
            }
        }

        [TestMethod]
        public void GenerateRoots_OneValueInEachDirection_ReturnsFoutRoots()
        {
            var boardString = "---1-\n" +
                              "--2x3\n" +
                              "-----\n" +
                              "---4-";

            var board = BoardCreator.CreateBoardFromString(boardString);

            var directionResolverMock = new Mock<IDirectionResolver>();

            var sut = new RootGenerator(directionResolverMock.Object);
            var result = sut.GenerateRoots(board);

            result.Should().HaveCount(4);
        }

        [TestMethod]
        public void GenerateRoots_TwoValuesInOneDirection_ReturnsTwoRoots()
        {
            var boardString = "-----\n" +
                              "---x-\n" +
                              "---1-\n" +
                              "---2-";

            var board = BoardCreator.CreateBoardFromString(boardString);

            var directionResolverMock = new Mock<IDirectionResolver>();

            var sut = new RootGenerator(directionResolverMock.Object);
            var result = sut.GenerateRoots(board);

            result.Should().HaveCount(2);
        }
    }
}
