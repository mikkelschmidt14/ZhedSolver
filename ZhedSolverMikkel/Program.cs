using BenchmarkDotNet.Running;
using System.Diagnostics;
using ZhedSolverMikkel.Board;

namespace ZhedSolverMikkel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<MapBenchmarker>();

            var mb = new MapBenchmarker();

            //mb.Level_01();
            //mb.Level_02();
            //mb.Level_03();
            //mb.Level_04();
            //mb.Level_05();
            //mb.Level_06();
            //mb.Level_07();
            //mb.Level_08();
            //mb.Level_09();
            //mb.Level_10();
            //mb.Level_11();
            //mb.Level_12();
            //mb.Level_13();
            //mb.Level_14();
            //mb.Level_15();
            //mb.Level_16();
            //mb.Level_17();
            //mb.Level_18();
            //mb.Level_19();
            //mb.Level_20();
            //mb.Level_21();
            //mb.Level_22();
            //mb.Level_23();
            //mb.Level_24();
            //mb.Level_25();
            //mb.Level_26();
            //mb.Level_27();
            //mb.Level_28();
            //mb.Level_29();
            //mb.Level_30();
            //mb.Level_31();
            //mb.Level_32();
            //mb.Level_33();
            //mb.Level_34();
            //mb.Level_35();
            //mb.Level_36();
            //mb.Level_37();
            //mb.Level_38();
            //mb.Level_39();
            //mb.Level_40();
            //mb.Level_41();
            //////mb.Level_42(); //
            //mb.Level_43();
            //mb.Level_44();
            //mb.Level_45();
            //////mb.Level_46(); //
            //////mb.Level_47(); //
        }
    }
}