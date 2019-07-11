using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Enums
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
