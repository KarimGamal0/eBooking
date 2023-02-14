using eBooking.Enum;

namespace eBooking.Dtos
{
    public class RoomDetailsDto
    {
        public int Id { get; set; }

        public level Level { get; set; }

        public RoomType RoomType { get; set; }

        public RoomAvailabilty Availabilty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //Relation
        public int HotelId { get; set; }

        public string HotelName { get; set;}

        public int ClientId { get; set; }

        public string Client { get; set; }
    }
}
