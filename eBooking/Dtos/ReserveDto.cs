using eBooking.Enum;

namespace eBooking.Dtos
{
    public class ReserveDto
    {
        public int ClientId { get; set; }

        public RoomAvailabilty Availabilty { get; set; }


    }
}
