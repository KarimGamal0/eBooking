using eBooking.Enum;
using eBooking.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eBooking.Dtos
{
    public class RoomDto
    {
        public level Level { get; set; }

        public RoomType RoomType { get; set; }

        public RoomAvailabilty Availabilty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //Relation
        public int HotelId { get; set; }

        public int ClientId { get; set; }
    }
}
