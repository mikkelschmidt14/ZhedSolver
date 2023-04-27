using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;
using ZhedSolverMikkel.Extensions;

namespace ZhedSolverMikkel
{
    public class SolutionStep
    {
        public Position Position { get; private set; }
        public Direction Direction { get; private set; }
        public List<Position> PreviousPositions { get; private set; } = new List<Position>();

        public SolutionStep(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        public override bool Equals(object? obj)
        {
            var other = obj as SolutionStep;
            return Position.Equals(other?.Position) && Direction == other.Direction;
        }

        public override string ToString()
        {
            return $"({Position.X}, {Position.Y}) => {Direction}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash * 16777619) ^ Position.GetHashCode();
                hash = (hash * 16777619) ^ Direction.GetHashCode();
                return hash;
            }
        }

        public SolutionStep Clone()
        {
            var clone = new SolutionStep(Position, Direction);

            clone.PreviousPositions.AddRange(PreviousPositions);            

            return clone;
        }

        public void AddPreviousPositions(List<Position> positions)
        {
            PreviousPositions.AddRange(positions);
        }

        internal void AddPreviousPositionsFromSteps(HashSet<SolutionStep> possibleSolution)
        {
            foreach (var step in possibleSolution)
            {
                PreviousPositions.Add(step.Position);
            }
        }
    }
}
