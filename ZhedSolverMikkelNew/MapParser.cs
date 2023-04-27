using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel.Board;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZhedSolverMikkel
{
    public class MapParser
    {
        public IBoard ParseFile(string filePath)
        {
            var boardString = File.ReadAllText(filePath);

            return ParseString(boardString);

        }

        public IBoard ParseString(string boardString)
        {
            var lines = boardString.Split(new[] { "\r\n", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray()!;
            if (lines.GroupBy(s => s.Length).Count() > 1) throw new InvalidOperationException();

            var width = lines[0].Length;
            var height = lines.Count();

            var cells = new Cell[width, height];
            Position goalPosition = new Position(-1, -1);

            for (int y = 0; y < height; y++)
            {
                var line = lines[y];
                for (int x = 0; x < width; x++)
                {
                    var currentCharacter = line[x];

                    Cell cell = currentCharacter switch
                    {
                        '-' => new EmptyCell(x, y),
                        'x' => new GoalCell(x, y),
                        char c when int.TryParse(c.ToString(), out int num) => new ValueCell(x, y, num),
                        _ => throw new InvalidOperationException()
                    };
                    cells[x, y] = cell;

                    if (currentCharacter == 'x')
                    {
                        goalPosition = new Position(x, y);
                    }

                }
            }

            return new Board2D(cells, goalPosition);
        }
    }
}
