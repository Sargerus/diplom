using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Project
    {

        public Project()
        {
            this.UserAssigned = new HashSet<ApplicationUser>();
        }
        [Key]
        public int ProjectId { get; set; }
        public string ProjectDescription { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
        
        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual ICollection<ApplicationUser> UserAssigned { get; set; }
    }
}