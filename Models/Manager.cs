using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Libary.Models
{
    public class Manager
    {
        // Properties
        [Key]
        public int ManagerId { get; set; }

        [Required]
        public string Username { get; set; }

/*        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }*/

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string Mobile { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

