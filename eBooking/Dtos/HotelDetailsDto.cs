using eBooking.Models;
using System.ComponentModel.DataAnnotations;

namespace eBooking.Dtos
{
    public class HotelDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }


        public string Location { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
