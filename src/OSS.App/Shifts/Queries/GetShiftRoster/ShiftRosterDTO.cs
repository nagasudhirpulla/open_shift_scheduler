using OSS.Domain.Entities;

namespace OSS.App.Shifts.Queries.GetShiftRoster;

public class ShiftRosterDTO
{
    public List<string> ShiftTypes { get; set; }

    // the shift participations for a date will in the same order as the shift types array
    public Dictionary<DateTime, List<List<Tuple<string, ShiftParticipationType>>>> ShiftParticipations { get; set; } = new Dictionary<DateTime, List<List<Tuple<string, ShiftParticipationType>>>>();

    // Shift Comments
    public List<Tuple<DateTime, string, string>> ShiftComments { get; set; } = new List<Tuple<DateTime, string, string>>();
}
