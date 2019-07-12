using System.ComponentModel.DataAnnotations;

namespace Helper.Enums
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
