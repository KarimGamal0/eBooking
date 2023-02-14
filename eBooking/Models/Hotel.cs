using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBooking.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
 
        public string Location { get; set; }

        public int? RoomId { get; set; }

        //Relations
        [ForeignKey("RoomId")]
        public List<Room> Rooms { get; set; }

    }
}
