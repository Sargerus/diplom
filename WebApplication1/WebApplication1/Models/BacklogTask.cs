using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BacklogTask
    {

        [Key]
        public int TaskId { get; set; }

        [DataType(DataType.Text)]
        [Display(Description = "Task description: ")]
        [Required]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("CreatedByFK")]
        [Display(Description = "Created by:")]
        [Required]
        public String CreatedBy { get; set; }
        public virtual ApplicationUser CreatedByFK { get; set; }

        [DataType(DataType.Date)]
        [Display(Description = "Date created:")]
        [Required]
        public DateTime CreatedOn { get; set; }

        [Display(Description = "Estimated (hrs): ")]
        public Nullable<int> HoursEstiimated { get; set; }

        [Display(Description = "Done (hrs): ")]
        public Nullable<int> HoursDone { get; set; }

        [ForeignKey("BacklogRef")]
        [Required]
        public int Backlog { get; set; }
        public Backlog BacklogRef { get; set; }
    }


}