using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserAndProject
    {
        [Key]
        public String User { get; set; }

        public ICollection<Project> projects { get; set; }
    }
}