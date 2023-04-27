using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkel
{
    public interface ISolutionValidator
    {
        bool ValidateSolution(IBoard board, List<SolutionStep> solutionSteps);
    }

    public class SolutionValidator : ISolutionValidator
    {
        private HashSet<Position> _alreadyAppliedPositions;
        private HashSet<Position> _alreadyVisitedPositions;
        private List<SolutionStep> _solutionSteps;

        public bool ValidateSolution(IBoard board, List<SolutionStep> solutionSteps)
        {
            _alreadyAppliedPositions = new HashSet<Position>();
            _alreadyVisitedPositions = new HashSet<Position>();
            _solutionSteps = solutionSteps;

            var boardClone = board.Clone();

            var lastStep = solutionSteps.Last();

            TrySolutionFromStep(boardClone, lastStep);

            var goalCell = boardClone.GetCell(boardClone.GoalPosition.X, boardClone.GoalPosition.Y);

            return goalCell is FullCell;
        }

        public bool TrySolutionFromStep(IBoard board, SolutionStep step)
        {
            if (_alreadyAppliedPositions.Contains(step.Position))
            {
                return true;
            }

            if (_alreadyVisitedPositions.Contains(step.Position))
            {
                return false;
            }

            _alreadyVisitedPositions.Add(step.Position);

            foreach (var prevPosition in step.PreviousPositions)
            {
                var prevStepFromSolution = _solutionSteps.Where(ss => ss.Position.Equals(prevPosition)).FirstOrDefault();

                if (prevStepFromSolution is not null)
                {
                    var couldApply = TrySolutionFromStep(board, prevStepFromSolution);

                    if (!couldApply)
                    {
                        return false;
                    }
                }
            }

            board.ApplyStep(step);

            _alreadyAppliedPositions.Add(step.Position);

            return true;
        }
    }
}
