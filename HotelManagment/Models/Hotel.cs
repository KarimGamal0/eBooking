using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagment.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Display(Name = "Hotel Name")]
        [Required(ErrorMessage = "Hotel Name is required")]
        public string Name { get; set; }

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Image Url is required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Hotel Location")]
        [Required(ErrorMessage = "Hotel location is required")]
        public string Location { get; set; }
    }
}
