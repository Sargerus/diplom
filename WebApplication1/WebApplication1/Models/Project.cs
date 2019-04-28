using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Project
    {
        public Project()
        {
            this.UserAssigned = new HashSet<ApplicationUser>();
            this.Tasks = new HashSet<ProjectTask>();
        }
        [Key]
        public int ProjectId { get; set; }

        [DataType(DataType.Text)]
        [Display(Description = "Description")]
        [Required]
        public string ProjectDescription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Description = "Created on")]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Description = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Description = "End Date")]
        public DateTime EndDate { get; set; }
        
        [Display(Description = "Budget")]
        public Double Budget { get; set; }

        //count of team
        [Display(Description = "Team")]
        public int Team { get; set; }

        [Display(Description = "Total estimate")]
        public int TotalEstimate { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("CreatedByRef")]
        [Display(Description = "Created by")]
        public String CreatedBy { get; set; }
        public ApplicationUser CreatedByRef { get; set; }

        [Display(Description = "Head")]
        [ForeignKey("HeadOfProjectRef")]
        [Required]
        public String HeadOfProject { get; set; }
        public ApplicationUser HeadOfProjectRef { get; set; }

        public virtual ICollection<ApplicationUser> UserAssigned { get; set; }
        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }
}