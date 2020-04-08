using System.ComponentModel.DataAnnotations;

namespace BuildTheLanesAPI.Entities
{
    public class Project
    {
        [Key]
        public int project_number { get; set; }
        public string start_date { get; set; }
        public string status { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
    }
}