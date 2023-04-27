using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhedSolverMikkel.Extensions
{
    public static class HashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> existing, IEnumerable<T> others)
        {
            foreach (var item in others)
            {
                existing.Add(item);
            } 
        }
    }
}
