using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace OSS.App.Shifts.Queries.GetEmployeeCalendarById
{
    public class GetEmployeeCalendarByIdQuery : IRequest<CalendarDTO>
    {
        public string EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
