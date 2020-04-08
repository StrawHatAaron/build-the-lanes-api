namespace BuildTheLanesAPI.Entities
{

    /*in the database "User" is "Users" because sql server already has a table called User for SQL developers*/
    public class Admin
    {
        public int id { get; set; }
        public string email { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string roles { get; set; }
        /*For: Staff */
        public string title { get; set; }
        /*For: Admin */
        public string created { get; set; }
    }
}