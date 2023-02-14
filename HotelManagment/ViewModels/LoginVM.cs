using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HotelManagment.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
