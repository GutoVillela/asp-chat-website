﻿using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ChatRoomViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}