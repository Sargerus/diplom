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
        public BacklogTask()
        {
            this.Reports = new HashSet<Report>();
            this.ResponsibleUsers = new HashSet<ApplicationUser>();
        }

        [Key]
        public int TaskId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Task")]
        [Required]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("CreatedByFK")]
        [Display(Name = "Created by:")]
        [Required]
        public String CreatedBy { get; set; }
        public virtual ApplicationUser CreatedByFK { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date created:")]
        [Required]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Estimated (hrs): ")]
        public Nullable<int> HoursEstimated { get; set; }

        [Display(Description = "Done (hrs): ")]
        public Nullable<int> HoursDone { get; set; }


        [Required]
        [Display(Name = "Backlog:")]
        public int Backlog { get; set; }

        [ForeignKey("Backlog")]
        public virtual Backlog BacklogRef { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<ApplicationUser> ResponsibleUsers { get; set; }
    }


}