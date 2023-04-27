using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkel
{
    public interface IDirectionResolver
    {
        Direction ResolveDirection(Position fromPosition, Position toPosition);
    }

    public class DirectionResolver : IDirectionResolver
    {
        public Direction ResolveDirection(Position fromPosition, Position toPosition)
        {
            if (fromPosition.X < toPosition.X && fromPosition.Y == toPosition.Y)
            {
                return Direction.Right;
            }
            else if (fromPosition.X > toPosition.X && fromPosition.Y == toPosition.Y)
            {
                return Direction.Left;
            }
            else if (fromPosition.X == toPosition.X && fromPosition.Y < toPosition.Y)
            {
                return Direction.Down;
            }
            else if (fromPosition.X == toPosition.X && fromPosition.Y > toPosition.Y)
            {
                return Direction.Up;
            }

            throw new InvalidOperationException("Positions are equal or they don't line up");
        }
    }
}
