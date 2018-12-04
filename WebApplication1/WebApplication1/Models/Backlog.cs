using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Backlog
    {

        public Backlog()
        {
            this.Tasks = new HashSet<BacklogTask>();
        }

        [Key]
        public int BacklogId { get; set; }

        [DataType(DataType.Date)]
        [Display(Description = "Date created:")]
        [Required]
        public DateTime CreatedOn { get; set; }

        [Display(Description = "Project description:")]
        [Required]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("ProjectFK")]
        [Required]
        public int Project { get; set; }
        public virtual Project ProjectFK { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("CreatedByFK")]
        [Display(Description = "Created by:")]
        [Required]
        public String CreatedBy { get; set; }
        public virtual ApplicationUser CreatedByFK { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("BacklogStateFK")]
        [Display(Description = "Status:")]
        [Required]
        public String BacklogState { get; set; }
        public virtual BacklogState BacklogStateFK { get; set; }

        public virtual ICollection<BacklogTask> Tasks { get; set; }
    }
}