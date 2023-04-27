using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;
using ZhedSolverMikkel.SolutionTree;

namespace ZhedSolverMikkel
{
    public class ZhedSolver
    {
        private readonly IDirectionResolver _directionResolver;
        private readonly IStepCombinator _stepPermuter;
        private readonly ITowardsPositionResolver _towardsPositionResolver;
        private readonly IRootGenerator _rootGenerator;
        private readonly ISolutionTreeGenerator _solutionTreeGenerator;

        private readonly ISolutionValidator _solutionValidator;

        public ZhedSolver()
        {
            _directionResolver = new DirectionResolver();
            _stepPermuter = new StepCombinator();
            _towardsPositionResolver = new TowardsPositionResolver();
            _rootGenerator = new RootGenerator(_directionResolver);
            _solutionTreeGenerator = new SolutionTreeGenerator(_directionResolver, _stepPermuter, _towardsPositionResolver, _rootGenerator);

            _solutionValidator = new SolutionValidator();
        }

        public HashSet<SolutionStep> Solve(IBoard board)
        {
            var solutionTree = _solutionTreeGenerator.GenerateSolutionTree(board);

            foreach (var root in solutionTree)
            {
                var possibleSolutions = root.GetPossibleSolutions();

                foreach (var possibleSolution in possibleSolutions)
                {
                    var isSolutionValid = _solutionValidator.ValidateSolution(board, possibleSolution);

                    if (isSolutionValid)
                    {
                        return possibleSolution;
                    }
                }
            }

            throw new NotImplementedException();
        }
    }
}
