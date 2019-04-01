using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BacklogType
    {
            [Key]
            public string Type { get; set; }
    }
}