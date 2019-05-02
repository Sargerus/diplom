using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Report
    {

        public Report()
        {
            ReportedOn = DateTime.Now;
            HoursReported = 8;
            Comment = "Task Done";
        }

        [Key, Column(Order = 0)]
        public int ReportId { get; set; }

        [Key, Column(Order = 1)]
        public int TaskKey { get; set; }

        [Key, Column(Order = 2)]
        public int ProjectKey { get; set; }

        [ForeignKey("TaskKey,ProjectKey")]
        public virtual ProjectTask ProjectTask { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("ReportedByFK")]
        [Display(Description = "Reported by:")]
        [Required]
        public String ReportedBy { get; set; }
        public virtual ApplicationUser ReportedByFK { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Description = "Date reported:")]
        [Required]
        public DateTime ReportedOn { get; set; }

        [Display(Description = "Hours Reported: ")]
        [Required]
        public int HoursReported { get; set; }

        [Display(Description = "Comment")]
        [Required]
        public String Comment { get; set; }

    }
}