using System.ComponentModel.DataAnnotations;

namespace BuildTheLanesAPI.Entities
{
    public class Responsibility{
        [Key]
        public int number {get; set;}
        public string staff_email {get; set;}
        public int project_num {get; set;}
    }
}