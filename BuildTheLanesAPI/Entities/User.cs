using System;
namespace BuildTheLanesAPI.Entities
{

    /*in the database "User" is "Users" because sql server already has a table called User for SQL developers*/
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Token { get; set; }
    }
}
