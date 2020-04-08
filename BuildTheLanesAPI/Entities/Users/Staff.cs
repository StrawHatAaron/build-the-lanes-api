namespace BuildTheLanesAPI.Entities
{
    public class Staff
    {
        public int id { get; set; }
        public string email { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string roles { get; set; }
        /*For: Staff */
        public string title { get; set; }
        /*For: Engineer */
        public string type { get; set; }
        /*For: Admin */
        public string created { get; set; }
    }
}