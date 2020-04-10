using System;
using System.Collections.Generic;

namespace BuildTheLanesAPI.Models
{
    public partial class EngineerCertifications
    {
        public string Email { get; set; }
        public string Certification { get; set; }
        public string Id { get; set; } 
        public virtual Engineers EmailNavigation { get; set; }
    }
}
