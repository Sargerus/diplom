using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProjectTask
    {
        public ProjectTask()
        {
            this.Reports = new HashSet<Report>();
        }

        
        [Key, Column(Order = 0)]
        public int TaskKey { get; set; }

        [Key, Column(Order = 1)]
        public int ProjectKey { get; set; }

        [Required]
        public String ShortText { get; set; }
        
        public String Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime RequiredStartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime RequiredEndDate { get; set; }

        public int TaskDone { get; set; }

        public int TaskEstimated { get; set; }

        [Required]
        public String UserAssigned { get; set; }
        
        public String AssignedBy { get; set; }
        
        public virtual ICollection<Report> Reports { get; set; }
    }
}