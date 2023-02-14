using eBooking.Enum;

namespace eBooking.Dtos
{
    public class RoomUpdateDto
    {
        public level Level { get; set; }

        public RoomType RoomType { get; set; }

        public RoomAvailabilty Availabilty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
