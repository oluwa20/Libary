using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libary.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        public List<Book>? BorrowedBooks { get; set; }

        public int NumberOfBorrowedBooks { get; set; }

        [Required]
        public DateTime LastActivityDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid fine amount.")]
        public decimal FineAmount { get; set; }

        public virtual Role? Role { get; set; }

    }
}

