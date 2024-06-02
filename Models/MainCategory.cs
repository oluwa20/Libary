using System;
using System.ComponentModel.DataAnnotations;

namespace Libary.Models
{
    public class MainCategory
    {
        // Properties
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string? CategoryName { get; set; }

        // Navigation property for subcategories
        public virtual ICollection<SubCategory> SubCategories { get; set; }

        // Constructor
        public MainCategory()
        {
            SubCategories = new List<SubCategory>();
        }
    }
}

