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
        [Key]
        public int ReportId { get; set; }
        
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