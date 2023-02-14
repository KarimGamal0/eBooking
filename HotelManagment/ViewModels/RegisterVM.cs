using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HotelManagment.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "Full name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Full name is required")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

    }
}
