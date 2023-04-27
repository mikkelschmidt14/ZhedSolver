using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhedSolverMikkel.SolutionTree
{
    public interface IStepCombinator
    {
        List<List<SolutionStep>> GetCombinations(List<SolutionStep> steps, int amountInEachPermutation);
    }

    public class StepCombinator : IStepCombinator
    {
        public List<List<SolutionStep>> GetCombinations(List<SolutionStep> steps, int amountInEachPermutation)
        {
            if (!steps.Any() || amountInEachPermutation <= 0)
            {
                return new List<List<SolutionStep>>();
            }

            var result = new List<List<SolutionStep>>();

            foreach (var combination in CombinationCalculation.GetCombinations(steps, amountInEachPermutation))
            {
                if (IsCombinationValid(combination))
                {
                    result.Add(combination.ToList());
                }
            }

            return result;
        }

        public static bool IsCombinationValid(IEnumerable<SolutionStep> combination)
        {
            var firstDirection = combination.First().Direction;

            var isCombinationHorizontal = firstDirection == Direction.Left || firstDirection == Direction.Right;

            var alreadyChecked = new HashSet<int>();

            if (isCombinationHorizontal)
            {
                // Ys should be different
                //foreach (var y in combination.Select(ss => ss.Position.Y))
                foreach (var step in combination)
                {
                    var y = step.Position.Y;

                    if (alreadyChecked.Contains(y))
                    {
                        return false;
                    }

                    alreadyChecked.Add(y);
                }
            }
            else
            {
                // Xs should be different
                //foreach (var x in combination.Select(ss => ss.Position.X))
                foreach (var step in combination)
                {
                    var x = step.Position.X;

                    if (alreadyChecked.Contains(x))
                    {
                        return false;
                    }

                    alreadyChecked.Add(x);
                }
            }

            return true;
        }
    }

    public static class CombinationCalculation
    {
        private static bool NextCombination(IList<int> num, int n, int k)
        {
            bool finished;

            var changed = finished = false;

            if (k <= 0) return false;

            for (var i = k - 1; !finished && !changed; i--)
            {
                if (num[i] < n - 1 - (k - 1) + i)
                {
                    num[i]++;

                    if (i < k - 1)
                        for (var j = i + 1; j < k; j++)
                            num[j] = num[j - 1] + 1;
                    changed = true;
                }
                finished = i == 0;
            }

            return changed;
        }

        public static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> elements, int k)
        {
            var elem = elements.ToArray();
            var size = elem.Length;

            if (k > size) yield break;

            var numbers = new int[k];

            for (var i = 0; i < k; i++)
                numbers[i] = i;

            do
            {
                yield return numbers.Select(n => elem[n]);
            } while (NextCombination(numbers, size, k));
        }
    }
}
