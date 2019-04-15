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
            this.UsersAssigned = new HashSet<ApplicationUser>();
        }

        [Key]
        public int BacklogId { get; set; }

        [Display(Description = "Backlog type")]
        [Required]
        [ForeignKey("BacklogTypeFK")]
        public String BacklogType { get; set; }
        public virtual BacklogType BacklogTypeFK { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("CreatedByFK")]
        [Display(Description = "Created by:")]
        [Required]
        public String CreatedBy { get; set; }
        public virtual ApplicationUser CreatedByFK { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Description = "Date created:")]
        [Required]
        public DateTime CreatedOn { get; set; }

        [Display(Description = "Project description:")]
        public string ProjectDescription { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("ProjectFK")]
        [Required]
        public int Project { get; set; }
        public virtual Project ProjectFK { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("BacklogStateFK")]
        [Display(Description = "Status:")]
        [Required]
        public String BacklogState { get; set; }
        public virtual BacklogState BacklogStateFK { get; set; }

        [Display(Description = "Backlog description")]
        public String BacklogDescription { get; set; }

        public virtual ICollection<ApplicationUser> UsersAssigned { get; set; }
        public virtual ICollection<BacklogTask> Tasks { get; set; }
        //to future if i will become good in asp
        //public virtual ICollection<File> Files{get;set;}
    }
}