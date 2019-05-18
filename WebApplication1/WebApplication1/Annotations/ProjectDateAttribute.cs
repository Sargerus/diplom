using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ProjectDateAttribute : RangeAttribute
    {
        //private string errormessage;

        //public virtual string ErrorMessage
        //{
        //    get { return errormessage; }
        //}
        public DateTime _minvalue { get; set; }
        public DateTime _maxvalue { get; set; }

        private const string message = "Enter date between {0} and {1}"; 

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(message, _minvalue.ToString("{0:yyyy-MM-dd}"), _maxvalue.ToString("{0:yyyy-MM-dd}"));
        }

        public ProjectDateAttribute(int a, int b)
              : base(typeof(DateTime), DateTime.Now.AddDays(a).ToShortDateString(), DateTime.Now.AddDays(b).ToShortDateString()) { _minvalue = DateTime.Now.AddDays(a); _maxvalue = DateTime.Now.AddDays(b); }

    }
}