namespace OSS.Domain.Entities;
public class LeaveRequestComment : AuditableEntity
{
    public string Comment { get; set; }

    public LeaveRequest LeaveRequest { get; set; }
    public int LeaveRequestId { get; set; }
}