using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhedSolverMikkel.Board
{
    public abstract class Cell
    {
        public Position Position { get; }

        public Cell(int x, int y)
        {
            Position = new Position(x, y);
        }

        public abstract Cell Clone();

        public abstract string OnToString();

        public override string ToString() => OnToString();
    }

    public class EmptyCell : Cell
    {
        public const string CellString = "-";

        public EmptyCell(int x, int y) : base(x, y)
        {
        }

        public override string OnToString() => CellString;
        public override Cell Clone() => new EmptyCell(Position.X, Position.Y);
    }

    public class GoalCell : Cell
    {
        public const string CellString = "X";

        public GoalCell(int x, int y) : base(x, y)
        {
        }

        public override string OnToString() => CellString;
        public override Cell Clone() => new GoalCell(Position.X, Position.Y);
    }

    public class FullCell : Cell
    {
        public const string CellString = "#";

        public FullCell(int x, int y) : base(x, y)
        {
        }

        public override string OnToString() => CellString;
        public override Cell Clone() => new FullCell(Position.X, Position.Y);
    }

    [DebuggerDisplay("ValueCell '{Value}'")]
    public class ValueCell : Cell
    {
        public override string OnToString() => Convert.ToString(Value);

        public ValueCell(int x, int y, int value) : base(x, y)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public void SetValue(int value)
        {
            Value = value;
        }

        public override Cell Clone()
        {
            return new ValueCell(Position.X, Position.Y, Value);
        }

        public void Decrement() => Value -= 1;
    }
}
