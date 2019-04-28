using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class ProjectTaskViewModel
    {
        public ProjectTask projectTask { get; set; }

        public string colorIndicator { get; set; }

        public String EndDate { get; set; }

        public double TaskDoneFor { get; set; }
    }
}