namespace BuildTheLanesAPI.Entities
{
    public class Engineer
    {
        public int id { get; set; }
        public string email { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string roles { get; set; }
        /*For: Donator */
        public string amount_donated { get; set; }
        /*For: Staff */
        public string title { get; set; }
        /*For: Engineer */
        public string type { get; set; }
        
    }
}