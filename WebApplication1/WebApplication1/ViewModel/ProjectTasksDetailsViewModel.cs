using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class ProjectTasksDetailsViewModel
    {
        [Key]
        public int key { get; set; }

        public ProjectTask projectTask { get; set; }

        public List<Attacments> Attacments { get; set; }
    }
}