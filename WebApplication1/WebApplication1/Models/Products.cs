using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        public String Description { get; set; }
    }
}