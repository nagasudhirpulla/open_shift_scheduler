using System.Collections.Generic;

namespace OSS.App.Shifts.Queries.GetEmployeeCalendarById
{
    public class CalendarDTO
    {
        public List<CalendarEventDTO> CalendarEvents { get; set; } = new List<CalendarEventDTO>();
    }
}
