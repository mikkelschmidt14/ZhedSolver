using BenchmarkDotNet.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhedSolverMikkel
{
    [MemoryDiagnoser]
    public class MapBenchmarker
    {
        public static HashSet<SolutionStep> SolveForMap(string mapname)
        {
            var board = MapParser.ParseFile(mapname);

            var solver = new ZhedSolver();

            var solution = solver.Solve(board);

            return solution;
        }

        [Benchmark]
        public void Level_01()
        {
            var mapname = "Maps/Level_01.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_02()
        {
            var mapname = "Maps/Level_02.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_03()
        {
            var mapname = "Maps/Level_03.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_04()
        {
            var mapname = "Maps/Level_04.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_05()
        {
            var mapname = "Maps/Level_05.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_06()
        {
            var mapname = "Maps/Level_06.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_07()
        {
            var mapname = "Maps/Level_07.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_08()
        {
            var mapname = "Maps/Level_08.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_09()
        {
            var mapname = "Maps/Level_09.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_10()
        {
            var mapname = "Maps/Level_10.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_11()
        {
            var mapname = "Maps/Level_11.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_12()
        {
            var mapname = "Maps/Level_12.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_13()
        {
            var mapname = "Maps/Level_13.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_14()
        {
            var mapname = "Maps/Level_14.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_15()
        {
            var mapname = "Maps/Level_15.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_16()
        {
            var mapname = "Maps/Level_16.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_17()
        {
            var mapname = "Maps/Level_17.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_18()
        {
            var mapname = "Maps/Level_18.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_19()
        {
            var mapname = "Maps/Level_19.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_20()
        {
            var mapname = "Maps/Level_20.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_21()
        {
            var mapname = "Maps/Level_21.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_22()
        {
            var mapname = "Maps/Level_22.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_23()
        {
            var mapname = "Maps/Level_23.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_24()
        {
            var mapname = "Maps/Level_24.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_25()
        {
            var mapname = "Maps/Level_25.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_26()
        {
            var mapname = "Maps/Level_26.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_27()
        {
            var mapname = "Maps/Level_27.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_28()
        {
            var mapname = "Maps/Level_28.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_29()
        {
            var mapname = "Maps/Level_29.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_30()
        {
            var mapname = "Maps/Level_30.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_31()
        {
            var mapname = "Maps/Level_31.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_32()
        {
            var mapname = "Maps/Level_32.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_33()
        {
            var mapname = "Maps/Level_33.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_34()
        {
            var mapname = "Maps/Level_34.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_35()
        {
            var mapname = "Maps/Level_35.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_36()
        {
            var mapname = "Maps/Level_36.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_37()
        {
            var mapname = "Maps/Level_37.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_38()
        {
            var mapname = "Maps/Level_38.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_39()
        {
            var mapname = "Maps/Level_39.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_40()
        {
            var mapname = "Maps/Level_40.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_41()
        {
            var mapname = "Maps/Level_41.txt";

            var result = SolveForMap(mapname);
        }

        //[Benchmark]
        //public void Level_42()
        //{
        //    var mapname = "Maps/Level_42.txt";

        //    var result = SolveForMap(mapname);
        //}

        [Benchmark]
        public void Level_43()
        {
            var mapname = "Maps/Level_43.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_44()
        {
            var mapname = "Maps/Level_44.txt";

            var result = SolveForMap(mapname);
        }

        [Benchmark]
        public void Level_45()
        {
            var mapname = "Maps/Level_45.txt";

            var result = SolveForMap(mapname);
        }

        //[Benchmark]
        //public void Level_46()
        //{
        //    var mapname = "Maps/Level_46.txt";

        //    var result = SolveForMap(mapname);
        //}

        //[Benchmark]
        //public void Level_47()
        //{
        //    var mapname = "Maps/Level_47.txt";

        //    var result = SolveForMap(mapname);
        //}
    }
}
