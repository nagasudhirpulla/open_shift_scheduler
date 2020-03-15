using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.Shifts.Queries.GetShiftRoster;

namespace OSS.Web
{
    [Authorize]
    public class RosterModel : PageModel
    {
        private readonly IMediator _mediator;
        public RosterModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public GetShiftRosterQuery Query { get; set; }

        public ShiftRosterDTO Roster { get; set; }
        public async Task OnGet()
        {
            // set start and end dates as this month
            DateTime today = DateTime.Now;
            Query = new GetShiftRosterQuery
            {
                StartDate = new DateTime(today.Year, today.Month, 1)
            };
            Query.EndDate = Query.StartDate.AddMonths(1).AddDays(-1);
            // get roster data
            Roster = await _mediator.Send(Query);
        }

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            // get roster data
            Roster = await _mediator.Send(Query);
        }
    }
}