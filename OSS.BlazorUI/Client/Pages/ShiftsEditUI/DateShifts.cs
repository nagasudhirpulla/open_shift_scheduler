using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI;

public class DateShifts
{
    public DateOnly ShiftsDate { get; set; } = new();
    public Dictionary<ShiftType, Shift> Shifts { get; set; } = new();
}
