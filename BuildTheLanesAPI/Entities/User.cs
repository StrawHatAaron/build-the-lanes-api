using System;
namespace BuildTheLanesAPI.Entities
{

    /*in the database "User" is "Users" because sql server already has a table called User for SQL developers*/
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
