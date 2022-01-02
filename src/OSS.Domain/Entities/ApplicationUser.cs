using Microsoft.AspNetCore.Identity;

namespace OSS.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public ApplicationUser() : base()
    {
        EmployeeShiftSkills = new HashSet<EmployeeShiftSkill>();
        ShiftParticipations = new HashSet<ShiftParticipation>();
    }

    public string DisplayName { get; set; }

    public string OfficeId { get; set; }

    public string Designation { get; set; }

    public Gender Gender { get; set; }
    public int GenderId { get; set; }

    public bool IsActive { get; set; } = true;

    public ShiftRole ShiftRole { get; set; }
    public int? ShiftRoleId { get; set; }

    public ShiftGroup ShiftGroup { get; set; }
    public int ShiftGroupId { get; set; }

    public ICollection<EmployeeShiftSkill> EmployeeShiftSkills { get; private set; }

    public ICollection<ShiftParticipation> ShiftParticipations { get; private set; }
}