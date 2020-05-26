using System;

namespace BuildTheLanesAPI.Models
{
    public class UpdateModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set;}
        public string Token { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Roles { get; set; }
        public decimal? AmountDonated { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime? Created { get; set; }
    }
} 