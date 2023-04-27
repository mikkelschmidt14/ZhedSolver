using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkel.SolutionTree
{
    public interface IRootGenerator
    {
        IEnumerable<SingleNode> GenerateRoots(IBoard board);
    }

    public class RootGenerator : IRootGenerator
    {
        private readonly IDirectionResolver _directionResolver;

        public RootGenerator(IDirectionResolver directionResolver)
        {
            _directionResolver = directionResolver;
        }

        public IEnumerable<SingleNode> GenerateRoots(IBoard board)
        {
            var possibleEndingCells = board.GetIntersectingValueCells(board.GoalPosition);

            foreach (var possibleEndingCell in possibleEndingCells)
            {
                var direction = _directionResolver.ResolveDirection(possibleEndingCell.Position, board.GoalPosition);

                var possibleEndingStep = new SolutionStep(possibleEndingCell.Position, direction);

                var root = new SingleNode(possibleEndingStep, board.GoalPosition, null);

                yield return root;
            }
        }
    }
}
