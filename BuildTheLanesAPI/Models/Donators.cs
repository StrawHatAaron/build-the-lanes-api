using System;
using System.Collections.Generic;

namespace BuildTheLanesAPI.Models
{
    public partial class Donators
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Token { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Roles { get; set; }
        public decimal AmountDonated { get; set; }

        public virtual Users EmailNavigation { get; set; }
    }
}
