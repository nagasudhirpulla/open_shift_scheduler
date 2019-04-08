using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Models.AppModels
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public int? OfficeId { get; set; }

        [Required, StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        public string Phone { get; set; }

        [StringLength(75)]
        public string Email { get; set; }

        public DateTime? Dob { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public ShiftRole ShiftRole { get; set; }

    }
}
