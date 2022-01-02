namespace OSS.Domain.Entities;
public class ShiftGroup : BaseEntity
{
    public ShiftGroup()
    {
        Employees = new HashSet<ApplicationUser>();
    }

    public string Name { get; set; }
    public ICollection<ApplicationUser> Employees { get; private set; }
}