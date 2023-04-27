using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhedSolverMikkel;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkelTest.Board
{
    public static class BoardCreator
    {
        public static IBoard CreateBoardFromString(string boardString)
        {
            var parser = new MapParser();

            return MapParser.ParseString(boardString);
        }
    }
}
