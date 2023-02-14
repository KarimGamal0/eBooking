using eBooking.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBooking.Models
{
    public class Room
    {
        public int Id { get; set; }

        public level Level { get; set; }

        public RoomType RoomType { get; set; }

        public RoomAvailabilty Availabilty { get; set; }

        public int RoomCounter { get; set; }
        
        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }

        //Relation
        public int? HotelId { get; set; }

        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }


        public int? ClientId { get; set; }

       
        public List<Client> Client { get; set; }

    }
}
