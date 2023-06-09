﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;
using ZhedSolverMikkel.Extensions;

namespace ZhedSolverMikkel.SolutionTree
{
    [DebuggerDisplay("[{DDisplay}]")]
    public class MultiNode
    {
        private string DDisplay => $"{String.Join(", ", Children.Select(sn => $"{sn.Step}"))}";
        public SingleNode Parent { get; private set; }
        public List<SingleNode> Children { get; private set; } = new List<SingleNode>();

        public MultiNode(SingleNode parent)
        {
            Parent = parent;
            Parent.AddChild(this);
        }

        public void AddChild(SingleNode child)
        {
            Children.Add(child);
        }

        public bool IsPositionUsedInPathToRoot(Position position)
        {
            return Parent.IsPositionUsedInPathToRoot(position);
        }

        public List<HashSet<SolutionStep>> GetPossibleSolutions()
        {
            if (!Children.Any())
            {
                return new List<HashSet<SolutionStep>>();
            }

            return GetPossibleSolutions(Children);
        }

        public List<HashSet<SolutionStep>> GetPossibleSolutions(IEnumerable<SingleNode> singleNodes)
        {
            if (singleNodes.Count() == 1)
            {
                return singleNodes.First().GetPossibleSolutions();
            }

            var firstSingleNode = singleNodes.First();

            var remainingSingleNodes = singleNodes.Skip(1);

            var possibleSolutionForFirstSingleNode = firstSingleNode.GetPossibleSolutions();

            var restOfPossibleSolutions = GetPossibleSolutions(remainingSingleNodes);

            var possibleSolutions = new List<HashSet<SolutionStep>>();

            foreach (var firstPartOfPossibleSolution in possibleSolutionForFirstSingleNode)
            {
                foreach (var restOfPossibleSolution in restOfPossibleSolutions)
                {
                    var possibleSolution = CreatePossibleSolutionFromBothParts(firstPartOfPossibleSolution, restOfPossibleSolution);

                    if (!IsPossibleSolutionContradicting(possibleSolution))
                    {
                        MergePreviousSteps(possibleSolution, firstPartOfPossibleSolution, restOfPossibleSolution);
                        
                        possibleSolutions.Add(possibleSolution);
                    }
                }
            }

            return possibleSolutions;
        }

        private static HashSet<SolutionStep> CreatePossibleSolutionFromBothParts(HashSet<SolutionStep> firstPart, HashSet<SolutionStep> secondPart)
        {
            var possibleSolution = new HashSet<SolutionStep>();

            foreach (var step in firstPart)
            {
                possibleSolution.Add(step.Clone());
            }

            foreach (var step in secondPart)
            {
                possibleSolution.Add(step.Clone());
            }

            return possibleSolution;
        }

        private static void MergePreviousSteps(HashSet<SolutionStep> possibleSolution, HashSet<SolutionStep> firstPartOfPossibleSolution, HashSet<SolutionStep> restOfPossibleSolution)
        {
            foreach (var step in possibleSolution)
            {
                if(firstPartOfPossibleSolution.Contains(step))
                {
                    var isInRestOfPossibleSolution = restOfPossibleSolution.TryGetValue(step, out var stepFromRestOfPossibleSolution);

                    if (isInRestOfPossibleSolution)
                    {
                        step.AddPreviousPositions(stepFromRestOfPossibleSolution.PreviousPositions);
                    }
                }
            }
        }

        public static bool IsPossibleSolutionContradicting(HashSet<SolutionStep> steps)
        {
            var alreadyUsed = new HashSet<Position>();

            foreach (var step in steps)
            {
                if (alreadyUsed.Contains(step.Position))
                {
                    return true;
                }

                alreadyUsed.Add(step.Position);
            }

            return false;
        }
    }
}
