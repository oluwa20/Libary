using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libary.Models
{
    public class SubCategory
    {
        // Properties
        [Key]
        public int SubcategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string SubcategoryName { get; set; }

     
        [ForeignKey("MainCategory")]
        public int MainCategoryId { get; set; }

       
      /*  public MainCategory MainCategory { get; set; }*/
    }
}

