using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZhedSolverMikkel
{
    public enum Direction
    {
        [Display(Name = "Up")]
        Up,
        [Display(Name = "Down")]
        Down,
        [Display(Name = "Left")]
        Left,
        [Display(Name = "Right")]
        Right
    }
}
