using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkel.OldStuff
{
    public class SolutionNodeOld
    {
        public SolutionStep Step { get; private set; }
        public SolutionNodeOld? Parent { get; private set; }

        public SolutionNodeOld(SolutionStep step)
        {
            Step = step;
        }

        public SolutionNodeOld(SolutionStep step, SolutionNodeOld parent)
            : this(step)
        {
            Parent = parent;
        }

        public bool IsPositionUsed(Position position)
        {
            if (Step.Position.Equals(position))
            {
                return true;
            }

            return Parent is not null && Parent.IsPositionUsed(position);
        }
    }
}
