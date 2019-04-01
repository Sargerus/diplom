using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class ProjectViewModel
    {
        public Project project { get; set; }
        public string[] managers { get; set; }
    }
}