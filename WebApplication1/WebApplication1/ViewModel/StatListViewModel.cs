using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class StatListViewModel
    {
        [Key, Column(Order = 0)]
        public String ProjectName { get; set; }

        [Key, Column(Order = 1)]
        public String UserName { get; set; }

        //public DateTime StartDayOfWeek { get; set; }

        //public DateTime EndDayOfWeek { get; set; }

        public ICollection<ProjectTask> Tasks { get; set; }
    }
}