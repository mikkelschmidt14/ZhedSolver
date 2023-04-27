using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhedSolverMikkel.Board
{
    public class Board2D : IBoard
    {
        public int Width { get; }
        public int Height { get; }
        public Position GoalPosition { get; }
        public int AmountOfValueCells { get; }
        private readonly Cell[,] _cells;

        public Board2D(Cell[,] cells, Position goalPosition)
        {
            Width = cells.GetLength(0);
            Height = cells.GetLength(1);
            GoalPosition = goalPosition;
            _cells = cells;
        }

        public Board2D(Cell[,] cells, Position goalPosition, int amountOfValueCells)
            : this(cells, goalPosition)
        {
            AmountOfValueCells = amountOfValueCells;
        }

        public Cell GetCell(int x, int y)
        {
            return _cells[x, y];
        }

        public ValueCell GetValueCell(Position position)
        {
            return (ValueCell)_cells[position.X, position.Y];
        }

        public void FillCell(Position position)
        {
            _cells[position.X, position.Y] = new FullCell(position.X, position.Y);
        }

        public List<ValueCell> GetIntersectingValueCells(Position position)
        {
            var valueCells = new List<ValueCell>();

            valueCells.AddRange(GetHorizontalIntersectingValueCells(position));

            valueCells.AddRange(GetVerticalIntersectingValueCells(position));

            return valueCells;
        }

        public IBoard Clone()
        {
            var cells = (Cell[,])_cells.Clone();
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    cells[x, y] = _cells[x, y].Clone();

            return new Board2D(cells, GoalPosition);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var sign = _cells[x, y] switch
                    {
                        GoalCell => 'X',
                        FullCell => 'f',
                        EmptyCell => '-',
                        ValueCell valueCell => (char)(valueCell.Value + '0'),
                        _ => throw new NotImplementedException(),
                    };
                    sb.Append(sign);

                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
        
        public void ApplyStep(SolutionStep step)
        {
            var cell = (ValueCell)GetCell(step.Position.X, step.Position.Y);

            var positionToAdd = step.Direction switch
            {
                Direction.Up => new Position(0, -1),
                Direction.Down => new Position(0, 1),
                Direction.Left => new Position(-1, 0),
                Direction.Right => new Position(1, 0),
                _ => throw new NotImplementedException(),
            };

            var nextPosition = cell.Position.Add(positionToAdd);

            while (cell.Value > 0 && !IsPositionOutOfBounds(nextPosition))
            {
                var cellToFill = GetCell(nextPosition.X, nextPosition.Y);

                if (cellToFill is EmptyCell || cellToFill is GoalCell)
                {
                    FillCell(nextPosition);
                    cell.Decrement();
                }

                nextPosition = nextPosition.Add(positionToAdd);
            }

            FillCell(cell.Position);
        }

        public int EmptyDistanceBetween(Position position1, Position position2)
        {
            return EmptyCellsBetween(position1, position2).Count + 1;
        }

        public List<EmptyCell> EmptyCellsBetween(Position position1, Position position2)
        {
            if (position1.X != position2.X && position1.Y != position2.Y)
            {
                throw new ArgumentException($"Positions are not on a line ({position1} {position2})");
            }

            if (position1.Equals(position2))
            {
                throw new ArgumentException($"Positions are equal ({position1} {position2})");
            }

            return position1.X == position2.X ?
                VerticalEmptyCellsBetween(position1, position2) :
                HorizontalEmptyCellsBetween(position1, position2);
        }

        public int NumberOfPerpendicularLinesThatHasValueCells(Position position1, Position position2)
        {
            var numberOfPerpendicularLinesThatHasValueCells = 0;

            var emptyCells = EmptyCellsBetween(position1, position2);

            List<ValueCell> valueCellExtractor(Position position) => position1.X == position2.X 
                ? GetHorizontalIntersectingValueCells(position) 
                : GetVerticalIntersectingValueCells(position);

            foreach (var emptyCell in emptyCells)
            {
                if (valueCellExtractor(emptyCell.Position).Any())
                {
                    numberOfPerpendicularLinesThatHasValueCells++;
                }
            }

            return numberOfPerpendicularLinesThatHasValueCells;
        }

        private List<EmptyCell> HorizontalEmptyCellsBetween(Position position1, Position position2)
        {
            var emptyCells = new List<EmptyCell>();

            for (int x = Math.Min(position1.X, position2.X) + 1; x < Math.Max(position1.X, position2.X); x++)
            {
                if (GetCell(x, position1.Y) is EmptyCell emptyCell)
                {
                    emptyCells.Add(emptyCell);
                }
            }

            return emptyCells;
        }

        private List<EmptyCell> VerticalEmptyCellsBetween(Position position1, Position position2)
        {
            var emptyCells = new List<EmptyCell>();

            for (int y = Math.Min(position1.Y, position2.Y) + 1; y < Math.Max(position1.Y, position2.Y); y++)
            {
                if (GetCell(position1.X, y) is EmptyCell emptyCell)
                {
                    emptyCells.Add(emptyCell);
                }
            }

            return emptyCells;
        }

        private List<ValueCell> GetHorizontalIntersectingValueCells(Position position)
        {
            var valueCells = new List<ValueCell>();

            for (int x = 0; x < Width; x++)
            {
                if (_cells[x, position.Y] is ValueCell intersectingCell && intersectingCell.Position != position)
                {
                    valueCells.Add(intersectingCell);
                }
            }

            return valueCells;
        }

        private List<ValueCell> GetVerticalIntersectingValueCells(Position position)
        {
            var valueCells = new List<ValueCell>();

            for (int y = 0; y < Height; y++)
            {
                if (_cells[position.X, y] is ValueCell intersectingCell && intersectingCell.Position != position)
                {
                    valueCells.Add(intersectingCell);
                }
            }

            return valueCells;
        }

        private bool IsPositionOutOfBounds(Position position)
        {
            return position.X < 0
                || position.X >= Width
                || position.Y < 0
                || position.Y >= Height;
        }

        public List<Position> GetValueCellPositionsBehind(Position position, Position towardsPosition)
        {
            var positions = new List<Position>();

            if (position.X == towardsPosition.X && position.Y < towardsPosition.Y)
            {
                for (int y = 0; y < position.Y; y++)
                {
                    if (_cells[position.X, y] is ValueCell)
                    {
                        positions.Add(new Position(position.X, y));
                    }
                }
            }
            else if (position.X == towardsPosition.X && position.Y > towardsPosition.Y)
            {
                for (int y = position.Y + 1; y < Height; y++)
                {
                    if (_cells[position.X, y] is ValueCell)
                    {
                        positions.Add(new Position(position.X, y));
                    }
                }
            }
            else if (position.X < towardsPosition.X && position.Y == towardsPosition.Y)
            {
                for (int x = 0; x < position.X; x++)
                {
                    if (_cells[x, position.Y] is ValueCell)
                    {
                        positions.Add(new Position(x, position.Y));
                    }
                }
            }
            else if (position.X > towardsPosition.X && position.Y == towardsPosition.Y)
            {
                for (int x = position.X + 1; x < Width; x++)
                {
                    if (_cells[x, position.Y] is ValueCell)
                    {
                        positions.Add(new Position(x, position.Y));
                    }
                }
            }

            return positions;
        }

        public List<SolutionStep> GetPerpendicularStepsToLineSegment(Position fromPosition, Position towardsPosition, IDirectionResolver directionResolver)
        {
            var perpendicularSteps = new List<SolutionStep>();

            if (fromPosition.X == towardsPosition.X)
            {
                for (int y = Math.Min(fromPosition.Y, towardsPosition.Y) + 1; y < Math.Max(fromPosition.Y, towardsPosition.Y); y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (_cells[x, y] is ValueCell && x != fromPosition.X)
                        {
                            var valueCellPosition = new Position(x, y);

                            var direction = directionResolver.ResolveDirection(valueCellPosition, new Position(fromPosition.X, y));

                            var step = new SolutionStep(valueCellPosition, direction);

                            perpendicularSteps.Add(step);
                        }
                    }
                }
            } 
            else if (fromPosition.Y == towardsPosition.Y)
            {
                for (int x = Math.Min(fromPosition.X, towardsPosition.X) + 1; x < Math.Max(fromPosition.X, towardsPosition.X); x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (_cells[x, y] is ValueCell && y != fromPosition.Y)
                        {
                            var valueCellPosition = new Position(x, y);

                            var direction = directionResolver.ResolveDirection(valueCellPosition, new Position(x, fromPosition.Y));

                            var step = new SolutionStep(valueCellPosition, direction);

                            perpendicularSteps.Add(step);
                        }
                    }
                }
            }

            return perpendicularSteps;
        }

        public List<SolutionStep> GetStepsFromBehind(Position position, Position towardsPosition, IDirectionResolver directionResolver)
        {
            var stepsFromBehind = new List<SolutionStep>();

            var direction = directionResolver.ResolveDirection(position, towardsPosition);

            if (direction == Direction.Right)
            {
                for (int x = 0; x < position.X; x++)
                {
                    if (_cells[x, position.Y] is ValueCell)
                    {
                        var valueCellPosition = new Position(x, position.Y);

                        var step = new SolutionStep(valueCellPosition, direction);

                        stepsFromBehind.Add(step);
                    }
                }
            }
            else if (direction == Direction.Left)
            {
                for (int x = position.X + 1; x < Width; x++)
                {
                    if (_cells[x, position.Y] is ValueCell)
                    {
                        var valueCellPosition = new Position(x, position.Y);

                        var step = new SolutionStep(valueCellPosition, direction);

                        stepsFromBehind.Add(step);
                    }
                }
            }
            else if (direction == Direction.Down)
            {
                for (int y = 0; y < position.Y; y++)
                {
                    if (_cells[position.X, y] is ValueCell)
                    {
                        var valueCellPosition = new Position(position.X, y);

                        var step = new SolutionStep(valueCellPosition, direction);

                        stepsFromBehind.Add(step);
                    }
                }
            }
            else
            {
                for (int y = position.Y + 1; y < Height; y++)
                {
                    if (_cells[position.X, y] is ValueCell)
                    {
                        var valueCellPosition = new Position(position.X, y);

                        var step = new SolutionStep(valueCellPosition, direction);

                        stepsFromBehind.Add(step);
                    }
                }
            }

            return stepsFromBehind;
        }
    }
}
