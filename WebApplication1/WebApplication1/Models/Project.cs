using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.Annotations;

namespace WebApplication1.Models
{
    public class Project
    {
        private string today { get; set; }

        public Project()
        {
            today = DateTime.Today.Date.ToString("{0:yyyy - MM - dd}");
            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(365);
            this.UserAssigned = new HashSet<ApplicationUser>();
            this.Tasks = new HashSet<ProjectTask>();
        }
        [Key]
        public int ProjectId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        [Required]
        public string ProjectDescription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //[ProjectDate(-2,18250)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Long description")]
        public string LongDescription { get; set; }

        //[ProjectDate(-2,18250)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Budget")]
        public Double Budget { get; set; }

        //count of team
        [Display(Name = "Team")]
        public int Team { get; set; }

        [Display(Description = "Total estimate")]
        public int TotalEstimate { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("CreatedByRef")]
        [Display(Name = "Created by")]
        public String CreatedBy { get; set; }
        public ApplicationUser CreatedByRef { get; set; }

        [Display(Name = "Head")]
        [ForeignKey("HeadOfProjectRef")]
        [Required]
        public String HeadOfProject { get; set; }
        public ApplicationUser HeadOfProjectRef { get; set; }

        public virtual ICollection<ApplicationUser> UserAssigned { get; set; }
        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }
}