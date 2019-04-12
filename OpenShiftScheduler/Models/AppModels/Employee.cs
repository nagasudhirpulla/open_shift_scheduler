using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Models.AppModels
{
    public class Employee
    {
        public Employee()
        {
            // setting default attribute value - https://stackoverflow.com/questions/40936566/how-to-set-a-default-value-on-a-boolean-in-a-code-first-model
            IsActive = true;
        }

        public int EmployeeId { get; set; }

        public int? OfficeId { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required, StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        public string Phone { get; set; }

        [StringLength(75)]
        public string Email { get; set; }

        public DateTime? Dob { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public ShiftRole ShiftRole { get; set; }

        public ShiftGroup ShiftGroup { get; set; }

        public ICollection<ShiftSkill> ShiftSkills { get; set; }

    }
}
