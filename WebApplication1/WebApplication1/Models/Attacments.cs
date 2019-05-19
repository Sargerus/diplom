using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Attacments
    {
        [Key, Column(Order = 0)]
        [Display(Name = "Attachment Id")]
        public int AttacmentId { get; set; }

        [Display(Name = "Project Id")]
        [Key, Column(Order = 1)]
        public int ProjectId { get; set; }

        [Key, Column(Order = 2)]
        [Display(Name = "Task Id")]
        public int TaskId { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        public string PathToFile { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created On")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Created By")]
        [ForeignKey("CreatedByFK")]
        public string CreatedBy { get; set; }
        public ApplicationUser CreatedByFK { get; set; }

    }
}