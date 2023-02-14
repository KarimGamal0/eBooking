using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HotelManagment.Enum;

namespace HotelManagment.Models
{
    public class Room
    {
        public int Id { get; set; }


        [Display(Name = "Room Level")]
        [Required(ErrorMessage = "Room level is required")]
        public level Level { get; set; }

        [Display(Name = "Room type")]
        [Required(ErrorMessage = "Room level is required")]
        public RoomType RoomType { get; set; }

        [Display(Name = "Room Availabilty")]
        public RoomAvailabilty Availabilty { get; set; }
        public int RoomCounter { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }


        public int? clientId { get; set; }

        public List<Client> client { get; set; }


    }
}
