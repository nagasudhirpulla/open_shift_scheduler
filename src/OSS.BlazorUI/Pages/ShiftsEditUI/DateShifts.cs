using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI;

public class DateShifts
{
    public DateOnly ShiftsDate { get; set; } = new();
    public Dictionary<int, Shift> ShiftsByType { get; set; } = new();

    public static List<DateShifts> FromShifts(List<Shift> shifts)
    {
        var groupedShifts = shifts.GroupBy(shift => shift.ShiftDate)
            .Select(s => new DateShifts
            {
                ShiftsDate = DateOnly.FromDateTime(s.Key),
                ShiftsByType = s.ToList().GroupBy(shift => shift.ShiftTypeId).ToDictionary(g => g.Key, g => g.First())
            })
            .OrderBy(gs => gs.ShiftsDate)
            .ToList();
        return groupedShifts;
    }
}

