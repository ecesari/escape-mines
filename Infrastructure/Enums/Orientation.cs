using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Enums
{
    public enum Orientation
    {
        [Display(Name = "N")]
        North = 1,
        [Display(Name = "W")]
        West = 2,
        [Display(Name = "S")]
        South = 3,
        [Display(Name = "E")]
        East = 4
    }
}
