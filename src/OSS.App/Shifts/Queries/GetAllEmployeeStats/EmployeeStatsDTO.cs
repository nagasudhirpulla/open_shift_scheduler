using OSS.App.Security.Queries.GetAppUsers;

namespace OSS.App.Shifts.Queries.GetAllEmployeeStats;

public class EmployeeStatsDTO
{
    public UserDTO Employee { get; set; }
    public Dictionary<string, int> NumShiftsPerType { get; set; } = new Dictionary<string, int>();

    public int numAbsenceShifts { get; set; } = 0;
    public int numPresenceShifts { get; set; } = 0;
    public DateTime? LatestParticipation { get; set; }
    public DateTime? LatestNightParticipation { get; set; }
}
