using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Project_User
    {
        [Key, Column(Order = 0)]
        public int ProjectId { get; set; }

        [Key, Column(Order = 1)]
        public String User { get; set; }

        public bool isManager { get; set; }

        public bool isLead { get; set; }

        public bool isDev { get; set; }
    }
}