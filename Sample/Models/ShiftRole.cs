using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class ShiftRole
    {
        [Key]
        public int ShiftRoleId { get; set; }

        // unique constraint will be defined in OnModelCreating method of dbcontext
        [Required, StringLength(100, MinimumLength =1)]
        public string RoleName { get; set; }
    }
}
