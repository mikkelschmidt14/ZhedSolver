using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkel.SolutionTree
{

    [DebuggerDisplay("step = {Step}, towards = {TowardsPosition}")]
    public class SingleNode
    {
        public MultiNode? Parent { get; private set; }
        public List<MultiNode> Children { get; private set; } = new List<MultiNode>();
        public SolutionStep Step { get; private set; }
        public Position TowardsPosition { get; private set; }

        public SingleNode(SolutionStep step, Position towardsPosition, MultiNode? parent)
        {
            Step = step;
            TowardsPosition = towardsPosition;
            Parent = parent;
            
            Parent?.AddChild(this);
        }

        public void AddChild(MultiNode child)
        {
            Children.Add(child);
        }

        public bool IsPositionUsedInPathToRoot(Position position)
        {
            if (Parent is null)
            {
                return position.Equals(Step.Position);
            }

            if (position.Equals(Step.Position))
            {
                return true;
            }

            return Parent.IsPositionUsedInPathToRoot(position);
        }

        public List<List<SolutionStep>> GetPossibleSolutions()
        {
            if (!Children.Any())
            {
                return new List<List<SolutionStep>>
                {
                    new List<SolutionStep>
                    {
                        Step,
                    }
                };
            }

            var possibleSolutions = new List<List<SolutionStep>>();

            foreach (var child in Children)
            {
                var childsPossibleSolution = child.GetPossibleSolutions();

                foreach (var possibleSolution in childsPossibleSolution)
                {
                    var newStep = Step.Clone();

                    newStep.AddPreviousPositionsFromSteps(possibleSolution);

                    possibleSolution.Add(newStep);

                    possibleSolutions.Add(possibleSolution);
                }
            }

            return possibleSolutions;
        }
    }
}
