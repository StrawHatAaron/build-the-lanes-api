using System.ComponentModel.DataAnnotations;

namespace BuildTheLanesAPI.Entities
{
  public class EngineerCertification{
    [Key]
    public string email {get; set;}
    public string certification {get; set;}
  }
}