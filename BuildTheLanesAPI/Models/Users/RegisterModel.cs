﻿using System.ComponentModel.DataAnnotations;

namespace BuildTheLanesAPI.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        public string Roles { get; set; }


        /*For: Donator */
        public string amount_donated { get; set; }
        /*For: Staff */
        public string title { get; set; }
        /*For: Engineer */
        public string type { get; set; }
        /*For: Admin */
        public string created { get; set; }
    }
}