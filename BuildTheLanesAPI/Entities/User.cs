using System;
namespace BuildTheLanesAPI.Entities
{

    /*in the database "User" is "Users" because sql server already has a table called User for SQL developers*/
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
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
