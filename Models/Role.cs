using System;
using System.ComponentModel.DataAnnotations;

namespace Libary.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(60)]
        public string SubcategoryName { get; set; }

        public Role(int roleId, string subcategoryName)
        {
            RoleId = roleId;
            SubcategoryName = subcategoryName;
        }
    }
}

