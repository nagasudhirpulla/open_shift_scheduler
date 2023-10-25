using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI;

public class ShiftsGridRow
{
    public DateOnly ShiftsDate { get; set; } = new();
    public List<Shift> Shifts { get; set; } = new();

    public static List<ShiftsGridRow> FromShifts(List<Shift> shifts, List<ShiftType> shiftTypes, DateOnly StartDate, DateOnly EndDate)
    {
        static IEnumerable<DateOnly> EachDay(DateOnly from, DateOnly thru)
        {
            for (var day = from; day <= thru; day = day.AddDays(1))
                yield return day;
        }

        List<ShiftsGridRow> shiftsData = new();
        var shiftsGroupedByDate = shifts.ToLookup(shift => DateOnly.FromDateTime(shift.ShiftDate));

        foreach (DateOnly itrDt in EachDay(StartDate, EndDate))
        {
            var dateShifts = new ShiftsGridRow { ShiftsDate = itrDt, Shifts = new() };

            List<Shift> shiftsForDate = shiftsGroupedByDate[itrDt].ToList() ?? new();
            var shiftsForDateGroupedByShiftType = shiftsForDate.ToLookup(s => s.ShiftTypeId);

            foreach (var st in shiftTypes)
            {

                Shift shift = shiftsForDateGroupedByShiftType[st.Id].FirstOrDefault() ?? new()
                {
                    Id = -1,
                    ShiftType = st,
                    ShiftTypeId = st.Id,
                    ShiftDate = itrDt.ToDateTime(new(0))
                };

                dateShifts.Shifts.Add(shift);
            }
            shiftsData.Add(dateShifts);
        }
        return shiftsData;
    }
}

