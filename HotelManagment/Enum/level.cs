using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HotelManagment.Enum
{
    public enum level
    {
        [Display(Name = "Level 1")] Level_1 = 0,
        [Display(Name = "Level 2")] Level_2 = 1,
        [Display(Name = "Level 3")] Level_3 = 2,
    }
}
