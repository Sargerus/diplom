using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Backlog
    {
        [Key]
        public int BacklogId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.Text)]
        public virtual BacklogState BacklogState { get; set; }
    }
}