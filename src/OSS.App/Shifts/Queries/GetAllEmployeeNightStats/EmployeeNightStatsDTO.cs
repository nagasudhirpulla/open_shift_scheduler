using OSS.App.Security.Queries.GetAppUsers;

namespace OSS.App.Shifts.Queries.GetAllEmployeeNightStats;

public class EmployeeNightStatsDTO
{
    public UserDTO Employee { get; set; }
    public int NumNightShiftsAllotted { get; set; } = 0;
    public int NumNightShiftsAttended { get; set; } = 0;
    public string NightShiftDates { get; set; }
}
