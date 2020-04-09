using System;
using System.Collections.Generic;

namespace BuildTheLanesAPI.Models
{
    public partial class Projects
    {
        public Projects()
        {
            Responsibilities = new HashSet<Responsibilities>();
        }

        public int ProjectNum { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Responsibilities> Responsibilities { get; set; }
    }
}
