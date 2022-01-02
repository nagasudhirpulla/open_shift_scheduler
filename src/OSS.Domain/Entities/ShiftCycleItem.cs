namespace OSS.Domain.Entities;
public class ShiftCycleItem : BaseEntity
{
    // make sure shift sequence is unique
    // shift type linking is cascade delete, set in db context
    public int ShiftSequence { get; set; }

    public ShiftType ShiftType { get; set; }
    public int ShiftTypeId { get; set; }
}