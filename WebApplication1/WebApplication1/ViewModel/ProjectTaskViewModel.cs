using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class ProjectTaskViewModel
    {
        [Key]
        public int key { get; set; }

        public ProjectTask projectTask { get; set; }

        public string colorIndicator { get; set; }

        public String EndDate { get; set; }

        public double TaskDoneFor { get; set; }
    }
}