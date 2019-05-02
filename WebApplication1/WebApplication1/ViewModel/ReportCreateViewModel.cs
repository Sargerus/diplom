using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModel
{
    public class ReportCreateViewModel
    {
       
        public int ReportId { get; set; }
                
        public String ReportedBy { get; set; }
        
        public DateTime ReportedOn { get; set; }
        
        public int HoursReported { get; set; }
        
        public String Comment { get; set; }

        public int ProjectId { get; set; }

        public int TaskId { get; set; }
    }
}