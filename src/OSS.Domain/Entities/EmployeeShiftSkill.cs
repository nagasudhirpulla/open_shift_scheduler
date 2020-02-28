namespace OSS.Domain.Entities
{
    public class EmployeeShiftSkill : BaseEntity
    {
        public ApplicationUser Employee { get; set; }
        public string EmployeeId { get; set; }

        public ShiftSkill ShiftSkill { get; set; }
        public int ShiftSkillId { get; set; }
    }
}
