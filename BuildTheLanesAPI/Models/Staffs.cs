using System;
using System.Collections.Generic;

namespace BuildTheLanesAPI.Models
{
    public partial class Staffs
    {
        public Staffs()
        {
            Responsibilities = new HashSet<Responsibilities>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Token { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Roles { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime? Created { get; set; }

        public virtual Users EmailNavigation { get; set; }
        public virtual Admins Admins { get; set; }
        public virtual Engineers Engineers { get; set; }
        public virtual ICollection<Responsibilities> Responsibilities { get; set; }
    }
}
