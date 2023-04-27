using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;
using ZhedSolverMikkel.OldStuff;

namespace ZhedSolverMikkel.OldStuff
{
    public class SolverOld
    {
        private readonly IDirectionResolver _directionResolver = new DirectionResolver();
        private readonly ITowardsPositionResolver _lineIntersector = new TowardsPositionResolver();

        public List<SolutionStep> Solve(IBoard board)
        {
            var leafNodes = CreateSolutionTree(board);

            foreach (var node in leafNodes)
            {
                var newBoard = board.Clone();

                var (success, steps) = CheckSolutionFromLeafNode(newBoard, node);

                if (success)
                {
                    return steps;
                }
            }

            return new();
        }

        public (bool, List<SolutionStep>) CheckSolutionFromLeafNode(IBoard board, SolutionNodeOld node)
        {
            var steps = new List<SolutionStep>();

            while (node != null)
            {
                board.ApplyStep(node.Step);

                steps.Add(node.Step);

                node = node.Parent;
            }

            var success = board.GetCell(board.GoalPosition.X, board.GoalPosition.Y) is FullCell;

            return (success, steps);
        }

        public List<SolutionNodeOld> CreateSolutionTree(IBoard board)
        {
            var leafNodes = new List<SolutionNodeOld>();

            var lastPotentialSteps = GetLastPotentialSolutionSteps(board);

            foreach (var potentialLastStep in lastPotentialSteps)
            {
                var stepsBefore = GetPotentialStepsBefore(board, potentialLastStep, board.GoalPosition);

                var parentNode = new SolutionNodeOld(potentialLastStep);

                foreach (var step in stepsBefore)
                {
                    var node = new SolutionNodeOld(step, parentNode);

                    var subLeafNodes = CreateSubSolutionTree(board, node);

                    leafNodes.AddRange(subLeafNodes);
                }
            }

            return leafNodes;
        }

        public List<SolutionNodeOld> CreateSubSolutionTree(IBoard board, SolutionNodeOld parentNode)
        {
            var leafNodes = new List<SolutionNodeOld>();

            var towardsPosition = _lineIntersector.ResolveTowardsPosition(parentNode.Step, parentNode.Parent?.Step);

            var stepsBefore = GetPotentialStepsBefore(board, parentNode.Step, towardsPosition);

            foreach (var step in stepsBefore)
            {
                if (parentNode.IsPositionUsed(step.Position))
                {
                    continue;
                }

                var node = new SolutionNodeOld(step, parentNode);

                var newLeafNodes = CreateSubSolutionTree(board, node);

                leafNodes.AddRange(newLeafNodes);
            }

            if (leafNodes.Count == 0)
            {
                leafNodes.Add(parentNode);
            }

            return leafNodes;
        }

        public List<SolutionStep> GetPotentialStepsBefore(IBoard board, SolutionStep step, Position towardsPosition)
        {
            var potentialStepsBefore = new List<SolutionStep>();

            var potentialPositionsOnLine = board.GetValueCellPositionsBehind(step.Position, towardsPosition);

            foreach (var position in potentialPositionsOnLine)
            {
                var direction = _directionResolver.ResolveDirection(position, towardsPosition);

                var potentialStep = new SolutionStep(position, direction);

                potentialStepsBefore.Add(potentialStep);
            }

            var potentialCellsCrossingLine = board.GetPerpendicularStepsToLineSegment(step.Position, towardsPosition, _directionResolver);

            potentialStepsBefore.AddRange(potentialCellsCrossingLine);

            return potentialStepsBefore;
        }

        public List<SolutionStep> GetLastPotentialSolutionSteps(IBoard board)
        {
            var potentialSteps = new List<SolutionStep>();

            foreach (var potentialCell in board.GetIntersectingValueCells(board.GoalPosition))
            {
                var direction = _directionResolver.ResolveDirection(potentialCell.Position, board.GoalPosition);

                var potentialStep = new SolutionStep(potentialCell.Position, direction);

                potentialSteps.Add(potentialStep);
            }

            return potentialSteps;
        }
    }
}
