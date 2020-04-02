﻿using System.ComponentModel.DataAnnotations;

namespace BuildTheLanesAPI.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}