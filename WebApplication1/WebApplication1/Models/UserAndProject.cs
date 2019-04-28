using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserAndProject
    {
        public String User { get; set; }

        public ICollection<Project> projects { get; set; }
    }
}