using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeesRegister.Models.Database
{
    [Table("Employee")]
    public class Employee
    {
        public Employee()
        {
            this.IsActive = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        public int Age { get; set; }

        [MaxLength(1)]
        public string Gender { get; set; }

        public int DepartmentId { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department {get ;set;}

        [InverseProperty("Employee")]
        public virtual List<Skills> Skills { get; set; }
    }
}