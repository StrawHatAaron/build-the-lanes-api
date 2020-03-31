using System.ComponentModel.DataAnnotations;

namespace BuildTheLanesAPI.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        //Aaron added this one
        [Required]
        public string Roles { get; set; }
    }
}