using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Models.AppModels
{
    public class EmployeeShiftSkill
    {
        public int EmployeeShiftSkillId { get; set; }

        public Employee Employee { get; set; }
        [Required]
        public int EmployeeId { get; set; }

        public ShiftSkill ShiftSkill { get; set; }
        [Required]
        public int ShiftSkillId { get; set; }
    }
}
