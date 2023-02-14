using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagment.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public bool IsCheckedBefore { get; set; }

        public int? RoomId { get; set; }

        [ForeignKey("RoomId")]
        public List<Room> Rooms { get; set; }
    }
}
