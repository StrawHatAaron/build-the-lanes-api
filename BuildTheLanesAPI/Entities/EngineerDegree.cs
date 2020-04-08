using System.ComponentModel.DataAnnotations;

namespace BuildTheLanesAPI.Entities
{
  public class EngineerDegree{
    [Key]
    public string email {get; set;}
    public string degrees {get; set;}
  }
}