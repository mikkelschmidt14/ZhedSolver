using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhedSolverMikkel.Board
{
    public interface IBoard
    {
        int Width { get; }
        int Height { get; }
        public Position GoalPosition { get; }
        int AmountOfValueCells { get; }

        Cell GetCell(int x, int y);
        void FillCell(Position position);
        IBoard Clone();
        string ToString();
        List<ValueCell> GetIntersectingValueCells(Position position);
        void ApplyStep(SolutionStep step);
        int EmptyDistanceBetween(Position position1, Position position2);
        List<EmptyCell> EmptyCellsBetween(Position position1, Position position2);
        int NumberOfPerpendicularLinesThatHasValueCells(Position position1, Position position2);
        List<Position> GetValueCellPositionsBehind(Position position, Position towardsPosition);
        List<SolutionStep> GetPerpendicularStepsToLineSegment(Position fromTosition, Position towardsPosition, IDirectionResolver directionResolver);
        ValueCell GetValueCell(Position position);
        List<SolutionStep> GetStepsFromBehind(Position position, Position towardsPosition, IDirectionResolver directionResolver);
    }
}
