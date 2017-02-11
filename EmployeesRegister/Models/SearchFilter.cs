using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeesRegister.Models
{
    public class SearchFilter
    {
        [Required(ErrorMessage = "Min length: 2")]
        [StringLength(1000, MinimumLength = 2, ErrorMessage ="Min length: 2")]
        public string Filter { get; set; }
    }
}