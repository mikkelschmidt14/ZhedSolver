using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkelTest
{
    [TestClass]
    public class TowardsPositionResolverTests
    {
        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreRightAndDown_ReturnPositionIsX2Y1()
        {
            var step1 = new SolutionStep(new(1, 4), Direction.Right);
            var step2 = new SolutionStep(new(3, 2), Direction.Down);

            var expectedPosition = new Position(3, 4);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreRightAndUp_ReturnPositionIsX2Y1()
        {

            var step1 = new SolutionStep(new(1, 4), Direction.Right);
            var step2 = new SolutionStep(new(3, 5), Direction.Up);

            var expectedPosition = new Position(3, 4);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreLeftAndDown_ReturnPositionIsX2Y1()
        {
            
            var step1 = new SolutionStep(new(5, 4), Direction.Left);
            var step2 = new SolutionStep(new(3, 2), Direction.Down);

            var expectedPosition = new Position(3, 4);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreLeftAndUp_ReturnPositionIsX2Y1()
        {
            
            var step1 = new SolutionStep(new(5, 4), Direction.Left);
            var step2 = new SolutionStep(new(3, 5), Direction.Up);

            var expectedPosition = new Position(3, 4);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreDownAndRight_ReturnPositionIsX1Y2()
        {            
            var step1 = new SolutionStep(new(3, 2), Direction.Down);
            var step2 = new SolutionStep(new(1, 4), Direction.Right);

            var expectedPosition = new Position(3, 4);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreDownAndLeft_ReturnPositionIsX1Y2()
        {            
            var step1 = new SolutionStep(new(3, 2), Direction.Down);
            var step2 = new SolutionStep(new(1, 4), Direction.Left);

            var expectedPosition = new Position(3, 4);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreUpAndRight_ReturnPositionIsX1Y2()
        {            
            var step1 = new SolutionStep(new(3, 2), Direction.Up);
            var step2 = new SolutionStep(new(1, 4), Direction.Right);

            var expectedPosition = new Position(3, 4);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreUpAndLeft_ReturnPositionIsX1Y2()
        {            
            var step1 = new SolutionStep(new(3, 2), Direction.Up);
            var step2 = new SolutionStep(new(1, 4), Direction.Left);

            var expectedPosition = new Position(3, 4);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreRight_ReturnPositionIsMaxXY1()
        {
            var step1 = new SolutionStep(new(4, 3), Direction.Right);
            var step2 = new SolutionStep(new(1, 3), Direction.Right);

            var expectedPosition = new Position(4, 3);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreLeft_ReturnPositionIsMinXY1()
        {
            var step1 = new SolutionStep(new(4, 3), Direction.Left);
            var step2 = new SolutionStep(new(1, 3), Direction.Left);

            var expectedPosition = new Position(1, 3);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreDown_ReturnPositionIsX1MaxY()
        {
            var step1 = new SolutionStep(new(3, 4), Direction.Down);
            var step2 = new SolutionStep(new(3, 1), Direction.Down);

            var expectedPosition = new Position(3, 4);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }

        [TestMethod]
        public void ResolveTowardsPosition_DirectionsAreRight_ReturnPositionIsX1MinY()
        {
            var step1 = new SolutionStep(new(3, 4), Direction.Up);
            var step2 = new SolutionStep(new(3, 1), Direction.Up);

            var expectedPosition = new Position(3, 1);

            var sut = new TowardsPositionResolver();
            var result = sut.ResolveTowardsPosition(step1, step2);

            result.Should().Be(expectedPosition);
        }
    }
}
