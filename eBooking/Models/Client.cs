using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBooking.Models
{
    public class Client : IdentityUser<int>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public string Email { get; set; }

        //public string Password { get; set; }

        public bool IsCheckedBefore { get; set; }

        public int? RoomId { get; set; }

        [ForeignKey("RoomId")]
        public List<Room> Rooms { get; set; }

    }
}
