﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Libary.Models
{
    public class Laibrarian
    {
        // Properties
        [Key]
        public int LaibrarianId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Mobile { get; set; }

        // Navigation property
        public virtual Role? Role { get; set; }

    }
}

