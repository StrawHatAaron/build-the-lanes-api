using System;
using System.ComponentModel.DataAnnotations;

namespace BuildTheLanesAPI.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Token { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Roles { get; set; }
        public decimal? AmountDonated { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime? Created { get; set; }
        public virtual Donators Donators { get; set; }
        public virtual Staffs Staffs { get; set; }
    }
}
