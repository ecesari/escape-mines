using System.ComponentModel.DataAnnotations;

namespace Helper.Enums
{
    public enum Movement
    {
        [Display(Name = "L")]
        Left,
        [Display(Name = "R")]
        Right,
        [Display(Name = "M")]
        Move
    }
}
