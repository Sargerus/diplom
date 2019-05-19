using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AssignedUser
    {
        [Key, Column(Order = 0)]
        public string Name { get; set; }

        [Key, Column(Order = 1)]
        public int ProjectId { get; set; }

        public bool isLead { get; set; }

        public string TeamLead { get; set; }

        public bool isManager { get; set; }

        public bool isDev { get; set; }
    }
}