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
    public class DirectionResolverTests
    {
        [TestMethod]
        public void ResolveDirection_PositionsAreEqual_ThrowsInvalidOperationException()
        {
            var fromPosition = new Position(1, 1);
            var toPosition = new Position(1, 1);

            var sut = new DirectionResolver();

            sut.Invoking(s => s.ResolveDirection(fromPosition, toPosition))
                .Should()
                .Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void ResolveDirection_PositionsDontLineUp_ThrowsInvalidOperationException()
        {
            var fromPosition = new Position(1, 0);
            var toPosition = new Position(0, 1);

            var sut = new DirectionResolver();

            sut.Invoking(s => s.ResolveDirection(fromPosition, toPosition))
                .Should()
                .Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void ResolveDirection_FromXIsSmallerThanToX_ReturnsRight()
        {
            var fromPosition = new Position(0, 0);
            var toPosition = new Position(1, 0);

            var sut = new DirectionResolver();
            var result = sut.ResolveDirection(fromPosition, toPosition);

            var expectedDirection = Direction.Right;

            result.Should().Be(expectedDirection);
        }

        [TestMethod]
        public void ResolveDirection_FromXIsLargerThanToX_ReturnsLeft()
        {
            var fromPosition = new Position(1, 0);
            var toPosition = new Position(0, 0);

            var sut = new DirectionResolver();
            var result = sut.ResolveDirection(fromPosition, toPosition);

            var expectedDirection = Direction.Left;

            result.Should().Be(expectedDirection);
        }

        [TestMethod]
        public void ResolveDirection_FromYIsSmallerThanToY_ReturnsDown()
        {
            var fromPosition = new Position(0, 0);
            var toPosition = new Position(0, 1);

            var sut = new DirectionResolver();
            var result = sut.ResolveDirection(fromPosition, toPosition);

            var expectedDirection = Direction.Down;

            result.Should().Be(expectedDirection);
        }

        [TestMethod]
        public void ResolveDirection_FromYIsLargerThanToY_ReturnsUp()
        {
            var fromPosition = new Position(0, 1);
            var toPosition = new Position(0, 0);

            var sut = new DirectionResolver();
            var result = sut.ResolveDirection(fromPosition, toPosition);

            var expectedDirection = Direction.Up;

            result.Should().Be(expectedDirection);
        }
    }
}
