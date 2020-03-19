using OSS.App.Security.Queries.GetAppUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.App.Shifts.Queries.GetAllEmployeeStats
{
    public class EmployeeStatsDTO
    {
        public UserDTO Employee { get; set; }
        public Dictionary<string, int> NumShiftsPerType { get; set; } = new Dictionary<string, int>();

        public int numAbsenceShifts { get; set; } = 0;
        public int numPresenceShifts { get; set; } = 0;
    }
}
