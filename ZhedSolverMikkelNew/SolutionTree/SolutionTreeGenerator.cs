using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkel.SolutionTree
{
    public interface ISolutionTreeGenerator
    {
        IEnumerable<SingleNode> GenerateSolutionTree(IBoard board);
    }

    public class SolutionTreeGenerator : ISolutionTreeGenerator
    {
        private readonly IDirectionResolver _directionResolver;
        private readonly IStepCombinator _stepCombinator;
        private readonly ITowardsPositionResolver _towardsPositionResolver;
        private readonly IRootGenerator _rootGenerator;

        public SolutionTreeGenerator(IDirectionResolver directionResolver, IStepCombinator stepPermuter, ITowardsPositionResolver towardsPositionResolver, IRootGenerator rootGenerator)
        {
            _directionResolver = directionResolver;
            _stepCombinator = stepPermuter;
            _towardsPositionResolver = towardsPositionResolver;
            _rootGenerator = rootGenerator;
        }

        public IEnumerable<SingleNode> GenerateSolutionTree(IBoard board)
        {
            var roots = _rootGenerator.GenerateRoots(board);

            foreach (var root in roots)
            {
                GenerateMultiNodes(board, root);

                yield return root;
            }
        }

        public void GenerateSingleNodes(IBoard board, List<SolutionStep> steps, MultiNode parent)
        {
            foreach (SolutionStep step in steps)
            {
                if (parent.IsPositionUsedInPathToRoot(step.Position))
                {
                    return;
                }

                var towardsPosition = _towardsPositionResolver.ResolveTowardsPosition(step, parent.Parent.Step);

                var singleNode = new SingleNode(step, towardsPosition, parent);

                GenerateMultiNodes(board, singleNode);
            }
        }

        public void GenerateMultiNodes(IBoard board, SingleNode singleNode)
        {
            var stepsFromBehind = board.GetStepsFromBehind(singleNode.Step.Position, singleNode.TowardsPosition, _directionResolver);

            var supportingSteps = board.GetPerpendicularStepsToLineSegment(singleNode.Step.Position, singleNode.TowardsPosition, _directionResolver);
            supportingSteps.AddRange(stepsFromBehind);

            var emptyDistanceToTarget = board.EmptyDistanceBetween(singleNode.Step.Position, singleNode.TowardsPosition);

            var amountInEachCombination = emptyDistanceToTarget - board.GetValueCell(singleNode.Step.Position).Value;

            var combinations = _stepCombinator.GetCombinations(supportingSteps, amountInEachCombination);

            foreach (var combination in combinations)
            {
                var multiNode = new MultiNode(singleNode);

                GenerateSingleNodes(board, combination, multiNode);
            }
        }
    }
}
