using System.Collections.Generic;

namespace OSS.Domain.Entities
{
    public class ShiftSkill : BaseEntity
    {
        public ShiftSkill()
        {
            EmployeeShiftSkills = new HashSet<EmployeeShiftSkill>();
        }

        public string Name { get; set; }

        public ICollection<EmployeeShiftSkill> EmployeeShiftSkills { get; private set; }
    }
}
