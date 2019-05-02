using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class ApplicatioUserViewModel
    {
        public List<ApplicationUser> users { get; set; }

        public int ProjectId { get; set; }

        public string ProjectDescription { get; set; }

        public ApplicationUser TeamLead { get; set; }
        
        //public ApplicationUser Manager { get; set; }
    }
}