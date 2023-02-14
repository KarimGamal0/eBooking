﻿using eBooking.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBooking.Dtos
{
    public class ClientDetailsDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsCheckedBefore { get; set; }

        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }
}
