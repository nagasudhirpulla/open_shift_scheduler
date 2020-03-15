using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Shifts.Queries.GetEmployeeCalendarById
{
    public class GetEmployeeCalendarByIdQueryHandler : IRequestHandler<GetEmployeeCalendarByIdQuery, CalendarDTO>
    {
        private readonly AppIdentityDbContext _context;

        public GetEmployeeCalendarByIdQueryHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<CalendarDTO> Handle(GetEmployeeCalendarByIdQuery request, CancellationToken cancellationToken)
        {
            CalendarDTO vm = new CalendarDTO();

            if (request.StartDate > request.EndDate)
            {
                return vm;
            }

            // get the shift participations from db
            List<ShiftParticipation> empShiftParts = await _context.ShiftParticipations
                                                                        .Include(sp => sp.ShiftParticipationType)
                                                                        .Include(sp => sp.Shift)
                                                                        .ThenInclude(sp => sp.ShiftType)
                                                                        .Where(sp => sp.EmployeeId == request.EmployeeId
                                                                                && sp.Shift.ShiftDate >= request.StartDate
                                                                                && sp.Shift.ShiftDate <= request.EndDate)
                                                                        .ToListAsync();

            // create calendar events from the fetched server information
            vm.CalendarEvents = new List<CalendarEventDTO>();
            foreach (ShiftParticipation shiftPart in empShiftParts)
            {
                CalendarEventDTO ce = new CalendarEventDTO();
                ShiftType evntShiftType = shiftPart.Shift.ShiftType;
                ShiftParticipationType evntShiftPartType = shiftPart.ShiftParticipationType;

                // derive the event title based on shift type
                ce.EventTitle = evntShiftType.Name;

                // derive the event date
                ce.ShiftDate = shiftPart.Shift.ShiftDate;

                // derive the event title css style
                ce.TitleBgColor = evntShiftType.ColorString;
                ce.EventTextClasses = new List<string> { "default_evnt_title" };
                if (evntShiftPartType.IsBold == true)
                {
                    ce.EventTextClasses.Add("bold_text");
                }
                if (evntShiftPartType.IsAbsence == true)
                {
                    ce.EventTextClasses.Add("absence_text");
                }
                ce.TooltipText = $"participation = {evntShiftPartType.Name}";

                // derive the event title text color
                ce.TitleColor = "#000000";
                if (evntShiftPartType != null)
                {
                    ce.TitleColor = evntShiftPartType.ColorString;
                }

                // add the calendar event to the view model
                vm.CalendarEvents.Add(ce);
            }

            return vm;
        }
    }
}
