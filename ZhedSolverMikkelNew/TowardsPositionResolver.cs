using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkel
{
    public interface ITowardsPositionResolver
    {
        public Position ResolveTowardsPosition(SolutionStep step1, SolutionStep step2);
    }

    public class TowardsPositionResolver : ITowardsPositionResolver
    {
        public Position ResolveTowardsPosition(SolutionStep step1, SolutionStep step2)
        {
            var direction1 = step1.Direction;
            var direction2 = step2.Direction;

            if (direction1 == direction2) 
            { 
                return ResolveTowardsPositionWhenDirectionsAreTheSame(step1, step2);
            }

            if (direction1 == Direction.Up || direction1 == Direction.Down)
            {
                (step2, step1) = (step1, step2);
            }

            return new Position(step2.Position.X, step1.Position.Y);

            throw new NotImplementedException();
        }

        private Position ResolveTowardsPositionWhenDirectionsAreTheSame(SolutionStep step1, SolutionStep step2)
        {
            var direction = step1.Direction;

            return direction switch
            {
                Direction.Right => new Position(Math.Max(step1.Position.X, step2.Position.X), step1.Position.Y),
                Direction.Left => new Position(Math.Min(step1.Position.X, step2.Position.X), step1.Position.Y),
                Direction.Down => new Position(step1.Position.X, Math.Max(step1.Position.Y, step2.Position.Y)),
                Direction.Up => new Position(step1.Position.X, Math.Min(step1.Position.Y, step2.Position.Y)),
                _ => throw new NotImplementedException(),
            };
        }
    }

}