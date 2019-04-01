using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Project_ApplicationUser
    {
        [Key]
        public int ID { get; set; }
        
        [ForeignKey("ProjectFK")]
        [Display(Description = "Project:")]
        [Required]
        public ApplicationUser Project { get; set; }
        public virtual ApplicationUser ProjectFK { get; set; }

        [ForeignKey("ManagerFK")]
        [Display(Description = "Manager:")]
        [Required]
        public ApplicationUser Manager { get; set; }
        public virtual ApplicationUser ManagerFK { get; set; }
    }
}