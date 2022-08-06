using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADProject.Models
{
    public class Employee
    {
            [Key]
            public int Id { get; set; }

            [Required]
            [MaxLength(50)]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(50)]
            public string LastName { get; set; }

            [Required]
            [MaxLength(50)]
            public string Email { get; set; }

            [MaxLength(20)]
            public string Phone { get; set; }

            [Required]
            [MaxLength(50)]
            public string Position { get; set; }
    }
}
