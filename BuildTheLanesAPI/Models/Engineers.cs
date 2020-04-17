using System;
using System.Collections.Generic;

namespace BuildTheLanesAPI.Models
{
    public partial class Engineers
    {
        public Engineers()
        {
            EngineerCertifications = new HashSet<EngineerCertifications>();
            EngineerDegrees = new HashSet<EngineerDegrees>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Token { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Roles { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public virtual Staffs EmailNavigation { get; set; }
        public virtual ICollection<EngineerCertifications> EngineerCertifications { get; set; }
        public virtual ICollection<EngineerDegrees> EngineerDegrees { get; set; }
    }
}
