using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkelTest.Board
{
    [TestClass]
    public class Board2DTests
    {
        #region GetCell

        [TestMethod]
        [DataRow(5)]
        [DataRow(-3)]
        [DataRow(-1)]
        [DataRow(2)]
        public void GetCell_XIsOutOfBounds_ThrowsIndexOutOfRangeException(int x)
        {
            var cells = new Cell[2, 2];
            var y = 1;

            var boardSut = new Board2D(cells, new Position(0,0));

            boardSut.Invoking(sut => sut.GetCell(x, y))
                .Should()
                .Throw<IndexOutOfRangeException>();
        }

        [TestMethod]
        [DataRow(5)]
        [DataRow(-3)]
        [DataRow(-1)]
        [DataRow(2)]
        public void GetCell_YIsOutOfBounds_ThrowsIndexOutOfRangeException(int y)
        {
            var cells = new Cell[2, 2];
            var x = 1;

            var boardSut = new Board2D(cells, new Position(0,0));

            boardSut.Invoking(sut => sut.GetCell(x, y))
                .Should()
                .Throw<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void GetCell_XAndYIsWithinBounds_ReturnsCell()
        {
            var cells = new Cell[2, 2];
            var x = 0;
            var y = 1;
            var valueCell = new ValueCell(x, y, 123);
            cells[x, y] = valueCell;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.GetCell(x, y);

            result.Should().Be(valueCell);
        }
        
        #endregion

        #region GetIntersectingValueCells

        [TestMethod]
        public void GetIntersectingValueCells_NoOthreValueCells_ReturnsEmptyList()
        {
            var cells = new Cell[2, 2];
            var x = 0;
            var y = 1;
            var position = new Position(x, y);

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.GetIntersectingValueCells(position);

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetIntersectingValueCells_Always_DoesNotReturnItself()
        {
            var cells = new Cell[2, 2];
            var x = 0;
            var y = 1;
            var valueCell = new ValueCell(x, y, 123);
            cells[x, y] = valueCell;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.GetIntersectingValueCells(valueCell.Position);

            result.Should().NotContain(valueCell);
        }

        [TestMethod]
        public void GetIntersectingValueCells_HasValueCellsToTheRight_ReturnsTheValueCellsToTheRight()
        {
            var cells = new Cell[5, 5];
            var x = 0;
            var y = 0;
            var position = new Position(x, y);

            var valueCellsToFind = new List<ValueCell>()
            {
                new(2, 0, 111),
                new(3, 0, 222),
            };

            foreach (var valueCell in valueCellsToFind)
            {
                cells[valueCell.Position.X, valueCell.Position.Y] = valueCell;
            }

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.GetIntersectingValueCells(position);

            result.Should().OnlyContain(c => valueCellsToFind.Contains(c));
        }

        [TestMethod]
        public void GetIntersectingValueCells_HasValueCellsToTheLeft_ReturnsTheValueCellsToTheLeft()
        {
            var cells = new Cell[5, 5];
            var x = 4;
            var y = 0;
            var position = new Position(x, y);

            var valueCellsToFind = new List<ValueCell>()
            {
                new(1, 0, 111),
                new(2, 0, 222),
            };

            foreach (var valueCell in valueCellsToFind)
            {
                cells[valueCell.Position.X, valueCell.Position.Y] = valueCell;
            }

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.GetIntersectingValueCells(position);

            result.Should().OnlyContain(c => valueCellsToFind.Contains(c));
        }

        [TestMethod]
        public void GetIntersectingValueCells_HasValueCellsToTheBelow_ReturnsTheValueCellsBelow()
        {
            var cells = new Cell[5, 5];
            var x = 0;
            var y = 0;
            var position = new Position(x, y);

            var valueCellsToFind = new List<ValueCell>()
            {
                new(0, 2, 111),
                new(0, 3, 222),
            };

            foreach (var valueCell in valueCellsToFind)
            {
                cells[valueCell.Position.X, valueCell.Position.Y] = valueCell;
            }

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.GetIntersectingValueCells(position);

            result.Should().OnlyContain(c => valueCellsToFind.Contains(c));
        }

        [TestMethod]
        public void GetIntersectingValueCells_HasValueCellsToTheAbove_ReturnsTheValueCellsAbove()
        {
            var cells = new Cell[5, 5];
            var x = 0;
            var y = 4;
            var position = new Position(x, y);

            var valueCellsToFind = new List<ValueCell>()
            {
                new(0, 1, 111),
                new(0, 2, 222),
            };

            foreach (var valueCell in valueCellsToFind)
            {
                cells[valueCell.Position.X, valueCell.Position.Y] = valueCell;
            }

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.GetIntersectingValueCells(position);

            result.Should().OnlyContain(c => valueCellsToFind.Contains(c));
        }

        #endregion

        #region ApplyStep

        [TestMethod]
        public void ApplyStep_Always_CellsAreFilled()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var x = 0;
            var y = 4;
            var value = 2;
            var valueCellToApplyStepFrom = new ValueCell(x, y, value);
            cells[x, y] = valueCellToApplyStepFrom;

            var direction = Direction.Up;
            var solutionStep = new SolutionStep(valueCellToApplyStepFrom.Position, direction);

            var boardSut = new Board2D(cells, new Position(0,0));
            boardSut.ApplyStep(solutionStep);

            boardSut.GetCell(x, y).Should().BeAssignableTo<FullCell>();
        }

        [TestMethod]
        public void ApplyStep_TriesToFillOutOfBounds_CellIsFilled()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var x = 2;
            var y = 2;
            var value = 17;
            var valueCellToApplyStepFrom = new ValueCell(x, y, value);
            cells[x, y] = valueCellToApplyStepFrom;

            var direction = Direction.Right;
            var solutionStep = new SolutionStep(valueCellToApplyStepFrom.Position, direction);

            var boardSut = new Board2D(cells, new Position(0,0));
            boardSut.ApplyStep(solutionStep);

            boardSut.GetCell(x, y).Should().BeAssignableTo<FullCell>();
        }

        [TestMethod]
        public void ApplyStep_DirectionIsRight_CellsToTheRightAreFilled()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var x = 0;
            var y = 0;
            var value = 2;
            var valueCellToApplyStepFrom = new ValueCell(x, y, value);
            cells[x, y] = valueCellToApplyStepFrom;

            var direction = Direction.Right;
            var solutionStep = new SolutionStep(valueCellToApplyStepFrom.Position, direction);

            var expectedPositionsToBeFilled = new List<Position>()
            {
                new(0, 0),
                new(1, 0),
                new(2, 0),
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            boardSut.ApplyStep(solutionStep);

            using (new AssertionScope())
            {
                var cellsOnBoard = GetAllCellsOnBoard(boardSut);
                var expectedCellsToBeFilled = expectedPositionsToBeFilled.Select(p => boardSut.GetCell(p.X, p.Y));

                expectedCellsToBeFilled.Should().AllBeAssignableTo<FullCell>();
                cellsOnBoard.OfType<FullCell>().Should().OnlyContain(c => expectedCellsToBeFilled.Contains(c));
            }
        }

        [TestMethod]
        public void ApplyStep_DirectionIsLeft_CellsToTheLeftAreFilled()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var x = 4;
            var y = 0;
            var value = 2;
            var valueCellToApplyStepFrom = new ValueCell(x, y, value);
            cells[x, y] = valueCellToApplyStepFrom;

            var direction = Direction.Left;
            var solutionStep = new SolutionStep(valueCellToApplyStepFrom.Position, direction);

            var expectedPositionsToBeFilled = new List<Position>()
            {
                new(2, 0),
                new(3, 0),
                new(4, 0),
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            boardSut.ApplyStep(solutionStep);

            using (new AssertionScope())
            {
                var cellsOnBoard = GetAllCellsOnBoard(boardSut);
                var expectedCellsToBeFilled = expectedPositionsToBeFilled.Select(p => boardSut.GetCell(p.X, p.Y));

                expectedCellsToBeFilled.Should().AllBeAssignableTo<FullCell>();
                cellsOnBoard.OfType<FullCell>().Should().OnlyContain(c => expectedCellsToBeFilled.Contains(c));
            }
        }

        [TestMethod]
        public void ApplyStep_DirectionIsDown_CellsBelowAreFilled()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var x = 0;
            var y = 0;
            var value = 2;
            var valueCellToApplyStepFrom = new ValueCell(x, y, value);
            cells[x, y] = valueCellToApplyStepFrom;

            var direction = Direction.Down;
            var solutionStep = new SolutionStep(valueCellToApplyStepFrom.Position, direction);

            var expectedPositionsToBeFilled = new List<Position>()
            {
                new(0, 0),
                new(0, 1),
                new(0, 2),
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            boardSut.ApplyStep(solutionStep);

            using (new AssertionScope())
            {
                var cellsOnBoard = GetAllCellsOnBoard(boardSut);
                var expectedCellsToBeFilled = expectedPositionsToBeFilled.Select(p => boardSut.GetCell(p.X, p.Y));

                expectedCellsToBeFilled.Should().AllBeAssignableTo<FullCell>();
                cellsOnBoard.OfType<FullCell>().Should().OnlyContain(c => expectedCellsToBeFilled.Contains(c));
            }
        }

        [TestMethod]
        public void ApplyStep_DirectionIsUp_CellsAboveAreFilled()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var x = 0;
            var y = 4;
            var value = 2;
            var valueCellToApplyStepFrom = new ValueCell(x, y, value);
            cells[x, y] = valueCellToApplyStepFrom;

            var direction = Direction.Up;
            var solutionStep = new SolutionStep(valueCellToApplyStepFrom.Position, direction);

            var expectedPositionsToBeFilled = new List<Position>()
            {
                new(0, 2),
                new(0, 3),
                new(0, 4),
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            boardSut.ApplyStep(solutionStep);

            using (new AssertionScope())
            {
                var cellsOnBoard = GetAllCellsOnBoard(boardSut);
                var expectedCellsToBeFilled = expectedPositionsToBeFilled.Select(p => boardSut.GetCell(p.X, p.Y));

                expectedCellsToBeFilled.Should().AllBeAssignableTo<FullCell>();
                cellsOnBoard.OfType<FullCell>().Should().OnlyContain(c => expectedCellsToBeFilled.Contains(c));
            }
        }

        [TestMethod]
        public void ApplyStep_FullCellIsInTheWay_FullCellIsSteppedOver()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var x = 0;
            var y = 4;
            var value = 1;
            var valueCellToApplyStepFrom = new ValueCell(x, y, value);
            cells[x, y] = valueCellToApplyStepFrom;

            cells[0, 3] = new FullCell(0, 3);

            var direction = Direction.Up;
            var solutionStep = new SolutionStep(valueCellToApplyStepFrom.Position, direction);

            var expectedPositionsToBeFilled = new List<Position>()
            {
                new(0, 2),
                new(0, 3),
                new(0, 4),
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            boardSut.ApplyStep(solutionStep);

            using (new AssertionScope())
            {
                var cellsOnBoard = GetAllCellsOnBoard(boardSut);
                var expectedCellsToBeFilled = expectedPositionsToBeFilled.Select(p => boardSut.GetCell(p.X, p.Y));

                expectedCellsToBeFilled.Should().AllBeAssignableTo<FullCell>();
                cellsOnBoard.OfType<FullCell>().Should().OnlyContain(c => expectedCellsToBeFilled.Contains(c));
            }
        }

        [TestMethod]
        public void ApplyStep_ValueCellIsInTheWay_ValueCellIsSteppedOverAndNotChanged()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var x = 0;
            var y = 4;
            var value = 1;
            var valueCellToApplyStepFrom = new ValueCell(x, y, value);
            cells[x, y] = valueCellToApplyStepFrom;

            cells[0, 3] = new ValueCell(0, 3, 111);

            var direction = Direction.Up;
            var solutionStep = new SolutionStep(valueCellToApplyStepFrom.Position, direction);

            var expectedPositionsToBeFilled = new List<Position>()
            {
                new(0, 2),
                new(0, 4),
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            boardSut.ApplyStep(solutionStep);

            using (new AssertionScope())
            {
                var cellsOnBoard = GetAllCellsOnBoard(boardSut);
                var expectedCellsToBeFilled = expectedPositionsToBeFilled.Select(p => boardSut.GetCell(p.X, p.Y));

                expectedCellsToBeFilled.Should().AllBeAssignableTo<FullCell>();
                cellsOnBoard.OfType<FullCell>().Should().OnlyContain(c => expectedCellsToBeFilled.Contains(c));

                boardSut.GetCell(0, 3).Should().BeAssignableTo<ValueCell>();
            }
        }

        #endregion

        #region FillCell

        [TestMethod]
        [DataRow(5)]
        [DataRow(-3)]
        [DataRow(-1)]
        [DataRow(2)]
        public void FillCell_XIsOutOfBounds_ThrowsIndexOutOfRangeException(int x)
        {
            var cells = new Cell[2, 2];
            var y = 1;
            var position = new Position(x, y);

            var boardSut = new Board2D(cells, new Position(0,0));

            Assert.ThrowsException<IndexOutOfRangeException>(() => boardSut.FillCell(position));
        }

        [TestMethod]
        [DataRow(5)]
        [DataRow(-3)]
        [DataRow(-1)]
        [DataRow(2)]
        public void FillCell_YIsOutOfBounds_ThrowsIndexOutOfRangeException(int y)
        {
            var cells = new Cell[2, 2];
            var x = 1;
            var position = new Position(x, y);

            var boardSut = new Board2D(cells, new Position(0,0));

            Assert.ThrowsException<IndexOutOfRangeException>(() => boardSut.FillCell(position));
        }

        [TestMethod]
        public void FillCell_XAndYIsWithinBounds_ReturnsCell()
        {
            var cells = new Cell[2, 2];
            var x = 0;
            var y = 1;
            var position = new Position(x, y);
            var valueCell = new ValueCell(x, y, 123);
            cells[x, y] = valueCell;

            var boardSut = new Board2D(cells, new Position(0,0));
            boardSut.FillCell(position);

            boardSut.GetCell(x, y).Should().BeAssignableTo<FullCell>();
        }

        #endregion

        #region EmptyCellsBetween

        [TestMethod]
        public void EmptyCellsBetween_PositionsAreNotOnALine_ThrowsException()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(2, 4);

            var boardSut = new Board2D(cells, new Position(0,0));

            boardSut.Invoking(sut => sut.EmptyCellsBetween(position1, position2))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Positions are not on a line*");
        }

        [TestMethod]
        public void EmptyCellsBetween_PositionsAreEqual_ThrowsException()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 1);

            var boardSut = new Board2D(cells, new Position(0,0));

            boardSut.Invoking(sut => sut.EmptyCellsBetween(position1, position2))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Positions are equal*");
        }

        [TestMethod]
        public void EmptyCellsBetween_OnlyEmptyCellsAreBetweenOnXAxis_EmptyCellsAreReturned()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(4, 1);

            var expectedEmptyCells = new List<EmptyCell>()
            {
                (EmptyCell)cells[2, 1],
                (EmptyCell)cells[3, 1],
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyCellsBetween(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(ec => expectedEmptyCells.Contains(ec));
                result.Should().Contain(expectedEmptyCells);
            }
        }

        [TestMethod]
        public void EmptyCellsBetween_OnlyEmptyCellsAreBetweenOnYAxis_EmptyCellsAreReturned()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            var expectedEmptyCells = new List<EmptyCell>()
            {
                (EmptyCell)cells[1, 2],
                (EmptyCell)cells[1, 3],
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyCellsBetween(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(ec => expectedEmptyCells.Contains(ec));
                result.Should().Contain(expectedEmptyCells);
            }
        }

        [TestMethod]
        public void EmptyCellsBetween_FullCellIsBetweenOnXAxis_EmptyCellsAreReturned()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(4, 1);

            cells[3, 1] = new FullCell(3, 1);

            var expectedEmptyCells = new List<EmptyCell>()
            {
                (EmptyCell)cells[2, 1],
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyCellsBetween(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(ec => expectedEmptyCells.Contains(ec));
                result.Should().Contain(expectedEmptyCells);
            }
        }

        [TestMethod]
        public void EmptyCellsBetween_FullCellIsBetweenOnYAxis_EmptyCellsAreReturned()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            cells[1, 3] = new FullCell(1, 3);

            var expectedEmptyCells = new List<EmptyCell>()
            {
                (EmptyCell)cells[1, 2],
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyCellsBetween(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(ec => expectedEmptyCells.Contains(ec));
                result.Should().Contain(expectedEmptyCells);
            }
        }

        [TestMethod]
        public void EmptyCellsBetween_ValueCellIsBetweenOnXAxis_EmptyCellsAreReturned()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(4, 1);

            cells[3, 1] = new ValueCell(3, 1, 111);

            var expectedEmptyCells = new List<EmptyCell>()
            {
                (EmptyCell)cells[2, 1],
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyCellsBetween(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(ec => expectedEmptyCells.Contains(ec));
                result.Should().Contain(expectedEmptyCells);
            }
        }

        [TestMethod]
        public void EmptyCellsBetween_ValueCellIsBetweenOnYAxis_EmptyCellsAreReturned()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            cells[1, 3] = new ValueCell(1, 3, 111);

            var expectedEmptyCells = new List<EmptyCell>()
            {
                (EmptyCell)cells[1, 2],
            };

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyCellsBetween(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(ec => expectedEmptyCells.Contains(ec));
                result.Should().Contain(expectedEmptyCells);
            }
        }

        #endregion

        #region EmptyDistanceBetween

        [TestMethod]
        public void EmptyDistanceBetween_AllEmptyCellsOnXAxis_ReturnsDistance()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(4, 1);

            var expectedDistance = 3;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyDistanceBetween(position1, position2);

            result.Should().Be(expectedDistance);
        }

        [TestMethod]
        public void EmptyDistanceBetween_AllEmptyCellsOnYAxis_ReturnsDistance()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            var expectedDistance = 3;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyDistanceBetween(position1, position2);

            result.Should().Be(expectedDistance);
        }

        [TestMethod]
        public void EmptyDistanceBetween_FullCellBetween_ReturnsDistanceWithoutFullCell()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            cells[1, 2] = new FullCell(1, 2);

            var expectedDistance = 2;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyDistanceBetween(position1, position2);

            result.Should().Be(expectedDistance);
        }

        [TestMethod]
        public void EmptyDistanceBetween_ValueCellBetween_ReturnsDistanceWithoutValueCell()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            cells[1, 2] = new ValueCell(1, 2, 111);

            var expectedDistance = 2;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.EmptyDistanceBetween(position1, position2);

            result.Should().Be(expectedDistance);
        }

        #endregion

        #region NumberOfPerpendicularLinesThatHasValueCells

        [TestMethod]
        public void NumberOfPerpendicularLinesThatHasValueCells_NoEmptyCellsBetweenPositions_ReturnsZero()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 3);

            cells[1, 2] = new FullCell(1, 2);

            var expectedResult = 0;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.NumberOfPerpendicularLinesThatHasValueCells(position1, position2);

            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void NumberOfPerpendicularLinesThatHasValueCells_NoPerpendicularValueCells_ReturnsZero()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 3);

            var expectedResult = 0;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.NumberOfPerpendicularLinesThatHasValueCells(position1, position2);

            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void NumberOfPerpendicularLinesThatHasValueCells_TwoPerpendicularValueCellsOnSameLine_ReturnsOne()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            cells[0, 2] = new ValueCell(0, 2, 111);
            cells[3, 2] = new ValueCell(3, 2, 111);

            var expectedResult = 1;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.NumberOfPerpendicularLinesThatHasValueCells(position1, position2);

            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void NumberOfPerpendicularLinesThatHasValueCells_TwoPerpendicularValueCellsOnDifferentLinesOnAlongYAxis_ReturnsTwo()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            cells[0, 2] = new ValueCell(0, 2, 111);
            cells[3, 3] = new ValueCell(3, 3, 111);

            var expectedResult = 2;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.NumberOfPerpendicularLinesThatHasValueCells(position1, position2);

            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void NumberOfPerpendicularLinesThatHasValueCells_TwoPerpendicularValueCellsOnDifferentLinesOnAlongXAxis_ReturnsTwo()
        {
            var cells = new Cell[5, 5];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(4, 1);

            cells[2, 0] = new ValueCell(2, 0, 111);
            cells[3, 3] = new ValueCell(3, 3, 111);

            var expectedResult = 2;

            var boardSut = new Board2D(cells, new Position(0,0));
            var result = boardSut.NumberOfPerpendicularLinesThatHasValueCells(position1, position2);

            result.Should().Be(expectedResult);
        }

        #endregion

        #region GetValueCellPositionsBehind

        [TestMethod]
        public void GetValueCellPositionsBehind_DirectionIsRight_ReturnsPositionsBeforeBotNotBetweenOrAfter()
        {
            var cells = new Cell[6, 6];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(4, 1);

            cells[0, 1] = new ValueCell(0, 1, 0);
            cells[1, 1] = new ValueCell(0, 1, 0);
            cells[3, 1] = new ValueCell(0, 1, 0);
            cells[4, 1] = new ValueCell(0, 1, 0);
            cells[5, 1] = new ValueCell(0, 1, 0);

            var expectedPositions = new List<Position>()
            {
                new Position(0, 1),
            };

            var boardSut = new Board2D(cells, new Position(0, 0));
            var result = boardSut.GetValueCellPositionsBehind(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(p => expectedPositions.Contains(p));
                expectedPositions.Should().OnlyContain(p => result.Contains(p));
            }
        }

        [TestMethod]
        public void GetValueCellPositionsBehind_DirectionIsLeft_ReturnsPositionsBeforeBotNotBetweenOrAfter()
        {
            var cells = new Cell[6, 6];
            InitCells(cells);

            var position1 = new Position(4, 1);
            var position2 = new Position(1, 1);

            cells[0, 1] = new ValueCell(0, 1, 0);
            cells[1, 1] = new ValueCell(0, 1, 0);
            cells[3, 1] = new ValueCell(0, 1, 0);
            cells[4, 1] = new ValueCell(0, 1, 0);
            cells[5, 1] = new ValueCell(0, 1, 0);

            var expectedPositions = new List<Position>()
            {
                new Position(5, 1),
            };

            var boardSut = new Board2D(cells, new Position(0, 0));
            var result = boardSut.GetValueCellPositionsBehind(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(p => expectedPositions.Contains(p));
                expectedPositions.Should().OnlyContain(p => result.Contains(p));
            }
        }

        [TestMethod]
        public void GetValueCellPositionsBehind_DirectionDown_ReturnsPositionsBeforeBotNotBetweenOrAfter()
        {
            var cells = new Cell[6, 6];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            cells[1, 0] = new ValueCell(0, 1, 0);
            cells[1, 1] = new ValueCell(0, 1, 0);
            cells[1, 3] = new ValueCell(0, 1, 0);
            cells[1, 4] = new ValueCell(0, 1, 0);
            cells[1, 5] = new ValueCell(0, 1, 0);

            var expectedPositions = new List<Position>()
            {
                new Position(1, 0),
            };

            var boardSut = new Board2D(cells, new Position(0, 0));
            var result = boardSut.GetValueCellPositionsBehind(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(p => expectedPositions.Contains(p));
                expectedPositions.Should().OnlyContain(p => result.Contains(p));
            }
        }

        [TestMethod]
        public void GetValueCellPositionsBehind_DirectionIsUp_ReturnsPositionsBeforeBotNotBetweenOrAfter()
        {
            var cells = new Cell[6, 6];
            InitCells(cells);

            var position1 = new Position(1, 4);
            var position2 = new Position(1, 1);

            cells[1, 0] = new ValueCell(0, 1, 0);
            cells[1, 1] = new ValueCell(0, 1, 0);
            cells[1, 3] = new ValueCell(0, 1, 0);
            cells[1, 4] = new ValueCell(0, 1, 0);
            cells[1, 5] = new ValueCell(0, 1, 0);

            var expectedPositions = new List<Position>()
            {
                new Position(1, 5),
            };

            var boardSut = new Board2D(cells, new Position(0, 0));
            var result = boardSut.GetValueCellPositionsBehind(position1, position2);

            using (new AssertionScope())
            {
                result.Should().OnlyContain(p => expectedPositions.Contains(p));
                expectedPositions.Should().OnlyContain(p => result.Contains(p));
            }
        }

        #endregion

        #region GetPerpendicularStepsToLineSegment

        [TestMethod]
        public void GetPerpendicularStepsToLineSegment_DirectionIsRight_ReturnsCorrectPosition()
        {
            var cells = new Cell[6, 6];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(4, 1);

            cells[2, 0] = new ValueCell(0, 1, 0);
            cells[3, 0] = new ValueCell(0, 1, 0);
            cells[2, 3] = new ValueCell(0, 1, 0);
            cells[3, 4] = new ValueCell(0, 1, 0);
            cells[2, 5] = new ValueCell(0, 1, 0);
            cells[3, 5] = new ValueCell(0, 1, 0);

            cells[0, 0] = new ValueCell(0, 1, 0);
            cells[5, 5] = new ValueCell(0, 1, 0);
            cells[0, 4] = new ValueCell(0, 1, 0);
            cells[5, 4] = new ValueCell(0, 1, 0);

            var expectedPositions = new List<SolutionStep>()
            {
                new SolutionStep(new Position(2, 0), Direction.Down),
                new SolutionStep(new Position(3, 0), Direction.Down),
                new SolutionStep(new Position(2, 3), Direction.Up),
                new SolutionStep(new Position(3, 4), Direction.Up),
                new SolutionStep(new Position(2, 5), Direction.Up),
                new SolutionStep(new Position(3, 5), Direction.Up),
            };

            var boardSut = new Board2D(cells, new Position(0, 0));
            var result = boardSut.GetPerpendicularStepsToLineSegment(position1, position2, new DirectionResolver());

            using (new AssertionScope())
            {
                result.Should().OnlyContain(p => expectedPositions.Contains(p));
                expectedPositions.Should().OnlyContain(p => result.Contains(p));
            }
        }

        [TestMethod]
        public void GetPerpendicularStepsToLineSegment_DirectionIsLeft_ReturnsCorrectPosition()
        {
            var cells = new Cell[6, 6];
            InitCells(cells);

            var position1 = new Position(4, 1);
            var position2 = new Position(1, 1);

            cells[2, 0] = new ValueCell(0, 1, 0);
            cells[3, 0] = new ValueCell(0, 1, 0);
            cells[2, 3] = new ValueCell(0, 1, 0);
            cells[3, 4] = new ValueCell(0, 1, 0);
            cells[2, 5] = new ValueCell(0, 1, 0);
            cells[3, 5] = new ValueCell(0, 1, 0);

            cells[0, 0] = new ValueCell(0, 1, 0);
            cells[5, 5] = new ValueCell(0, 1, 0);
            cells[0, 4] = new ValueCell(0, 1, 0);
            cells[5, 4] = new ValueCell(0, 1, 0);

            var expectedPositions = new List<SolutionStep>()
            {
                new SolutionStep(new Position(2, 0), Direction.Down),
                new SolutionStep(new Position(3, 0), Direction.Down),
                new SolutionStep(new Position(2, 3), Direction.Up),
                new SolutionStep(new Position(3, 4), Direction.Up),
                new SolutionStep(new Position(2, 5), Direction.Up),
                new SolutionStep(new Position(3, 5), Direction.Up),
            };

            var boardSut = new Board2D(cells, new Position(0, 0));
            var result = boardSut.GetPerpendicularStepsToLineSegment(position1, position2, new DirectionResolver());

            using (new AssertionScope())
            {
                result.Should().OnlyContain(p => expectedPositions.Contains(p));
                expectedPositions.Should().OnlyContain(p => result.Contains(p));
            }
        }

        [TestMethod]
        public void GetPerpendicularStepsToLineSegment_DirectionIsDown_ReturnsCorrectPosition()
        {
            var cells = new Cell[6, 6];
            InitCells(cells);

            var position1 = new Position(1, 1);
            var position2 = new Position(1, 4);

            cells[0, 2] = new ValueCell(0, 1, 0);
            cells[0, 3] = new ValueCell(0, 1, 0);
            cells[3, 2] = new ValueCell(0, 1, 0);
            cells[4, 3] = new ValueCell(0, 1, 0);
            cells[5, 2] = new ValueCell(0, 1, 0);
            cells[5, 3] = new ValueCell(0, 1, 0);

            cells[0, 0] = new ValueCell(0, 1, 0);
            cells[5, 5] = new ValueCell(0, 1, 0);
            cells[4, 0] = new ValueCell(0, 1, 0);
            cells[4, 5] = new ValueCell(0, 1, 0);

            var expectedPositions = new List<SolutionStep>()
            {
                new SolutionStep(new Position(0, 2), Direction.Right),
                new SolutionStep(new Position(0, 3), Direction.Right),
                new SolutionStep(new Position(3, 2), Direction.Left),
                new SolutionStep(new Position(4, 3), Direction.Left),
                new SolutionStep(new Position(5, 2), Direction.Left),
                new SolutionStep(new Position(5, 3), Direction.Left),
            };

            var boardSut = new Board2D(cells, new Position(0, 0));
            var result = boardSut.GetPerpendicularStepsToLineSegment(position1, position2, new DirectionResolver());

            using (new AssertionScope())
            {
                result.Should().OnlyContain(p => expectedPositions.Contains(p));
                expectedPositions.Should().OnlyContain(p => result.Contains(p));
            }
        }

        [TestMethod]
        public void GetPerpendicularStepsToLineSegment_DirectionIsUp_ReturnsCorrectPosition()
        {
            var cells = new Cell[6, 6];
            InitCells(cells);

            var position1 = new Position(1, 4);
            var position2 = new Position(1, 1);

            cells[0, 2] = new ValueCell(0, 1, 0);
            cells[0, 3] = new ValueCell(0, 1, 0);
            cells[3, 2] = new ValueCell(0, 1, 0);
            cells[4, 3] = new ValueCell(0, 1, 0);
            cells[5, 2] = new ValueCell(0, 1, 0);
            cells[5, 3] = new ValueCell(0, 1, 0);

            cells[0, 0] = new ValueCell(0, 1, 0);
            cells[5, 5] = new ValueCell(0, 1, 0);
            cells[4, 0] = new ValueCell(0, 1, 0);
            cells[4, 5] = new ValueCell(0, 1, 0);

            var expectedPositions = new List<SolutionStep>()
            {
                new SolutionStep(new Position(0, 2), Direction.Right),
                new SolutionStep(new Position(0, 3), Direction.Right),
                new SolutionStep(new Position(3, 2), Direction.Left),
                new SolutionStep(new Position(4, 3), Direction.Left),
                new SolutionStep(new Position(5, 2), Direction.Left),
                new SolutionStep(new Position(5, 3), Direction.Left),
            };

            var boardSut = new Board2D(cells, new Position(0, 0));
            var result = boardSut.GetPerpendicularStepsToLineSegment(position1, position2, new DirectionResolver());

            using (new AssertionScope())
            {
                result.Should().OnlyContain(p => expectedPositions.Contains(p));
                expectedPositions.Should().OnlyContain(p => result.Contains(p));
            }
        }

        #endregion

        #region Helpers
        private static Cell[] GetAllCellsOnBoard(IBoard board)
        {
            var cells = new Cell[board.Width * board.Height];

            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    cells[x*board.Height + y] = board.GetCell(x, y);
                }
            }

            return cells;
        }

        private static void InitCells(Cell[,] cells)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y] = new EmptyCell(x, y);
                }
            }
        }
        #endregion
    }
}
