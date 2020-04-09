using System;
using System.Collections.Generic;

namespace BuildTheLanesAPI.Models
{
    public partial class EngineerDegrees
    {
        public string Email { get; set; }
        public string Degree { get; set; }

        public virtual Engineers EmailNavigation { get; set; }
    }
}
