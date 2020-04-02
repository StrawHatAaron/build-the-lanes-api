using System.ComponentModel.DataAnnotations;

namespace BuildTheLanesAPI.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Roles { get; set; }


        /*For: Donator */
        public string AmountDonated { get; set; }
        /*For: Staff */
        public string Title { get; set; }
        /*For: Engineer */
        public string Type { get; set; }
        /*For: Admin */
        public string Created { get; set; }
    }
}