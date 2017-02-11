using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeesRegister.Models.Database
{
    [Table("User")]
    public class User
    {
        public User()
        {
            this.CreatedUtc = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Login { get; set; }

        [MaxLength(32)]
        [Required]
        public byte[] PasswordMD5 { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime? LastActionUtc { get; set; }
    }
}