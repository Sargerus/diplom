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
            RequiredStartDate = DateTime.Today;
            RequiredEndDate = DateTime.Today.AddDays(365);
            this.Reports = new HashSet<Report>();
        }

        
        [Key, Column(Order = 0)]
        [Display(Name = "Task ID")]
        public int TaskKey { get; set; }

        [Display(Name = "Project ID")]
        [Key, Column(Order = 1)]
        public int ProjectKey { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public String ShortText { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Required Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime RequiredStartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Required End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? RequiredEndDate { get; set; }

        [Display(Name = "Task Done")]
        public int TaskDone { get; set; }

        [Display(Name = "Task Estimated")]
        public int TaskEstimated { get; set; }

        [Display(Name = "User Assigned to Task")]
        public String UserAssigned { get; set; }

        [Display(Name = "Task Assigned By")]
        public String AssignedBy { get; set; }

        public bool notVisible { get; set; }
        
        public virtual ICollection<Report> Reports { get; set; }
    }
}