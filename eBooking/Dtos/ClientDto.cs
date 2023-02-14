using eBooking.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBooking.Dtos
{
    public class ClientDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //public int RoomId { get; set; }

    }
}
