using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libary.Models
{
    public class Book
    {
        // Properties
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Required]
        [StringLength(50)]
        public string? Author { get; set; }

        [Required]
        [StringLength(50)]
        public string? Publisher { get; set; }

        [Required]
        [StringLength(50)]
        public string? Type { get; set; }

        [Required]
        public bool Availability { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfCopies { get; set; }

        public string? Description { get; set; }

        public bool? isShow { get; set; }

        // Foreign Key for Main Category
        [ForeignKey("MainCategory")]
        public int MainCategoryId { get; set; }
        public MainCategory? MainCategory { get; set; }

        // Foreign Key for Sub Category
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }

        // Foreign Key for Book State
        [ForeignKey("BookState")]
        public int BookStateId { get; set; }
        public BookState? BookState { get; set; }


    }
}

