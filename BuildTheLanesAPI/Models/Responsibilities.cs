using System;
using System.Collections.Generic;

namespace BuildTheLanesAPI.Models
{
    public partial class Responsibilities
    {
        public int Number { get; set; }
        public string StaffEmail { get; set; }
        public int ProjectNum { get; set; }

        public virtual Projects ProjectNumNavigation { get; set; }
        public virtual Staffs StaffEmailNavigation { get; set; }
    }
}
