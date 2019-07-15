using System.ComponentModel.DataAnnotations;

namespace Helper.Enums
{
    public enum TurtleStatus
    {
        [Display(Name = "Still in Danger")]
        InDanger,
        [Display(Name = "Mine Hit")]
        Dead,
        [Display(Name = "Success")]
        Freed
    }
}
