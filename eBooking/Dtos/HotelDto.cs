using eBooking.Models;
using System.ComponentModel.DataAnnotations;

namespace eBooking.Dtos
{
    public class HotelDto
    {
        
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Location { get; set; }
    }
}
