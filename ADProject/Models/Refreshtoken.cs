using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADProject.Models
{
    public class Refreshtoken
    {
        [Key]
        [Required]
        [MaxLength(50)]
        public string TokenId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string RefreshToken { get; set; }
        public bool? IsActive { get; set; }
    }
}
