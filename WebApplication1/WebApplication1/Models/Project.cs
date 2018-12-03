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
            this.Backlogs = new HashSet<Backlog>();
        }
        [Key]
        public int ProjectId { get; set; }

        [DataType(DataType.Text)]
        [Display(Description = "Project dedscription:")]
        [Required]
        public string ProjectDescription { get; set; }

        [DataType(DataType.Date)]
        [Display(Description = "Created on:")]
        [Required]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Text)]
        [ForeignKey("CreatedByRef")]
        [Display(Description = "Created by:")]
        [Required]
        public String CreatedBy { get; set; }
        public ApplicationUser CreatedByRef { get; set; }

        public virtual ICollection<ApplicationUser> UserAssigned { get; set; }
        public virtual ICollection<Backlog> Backlogs { get; set; }
    }
}