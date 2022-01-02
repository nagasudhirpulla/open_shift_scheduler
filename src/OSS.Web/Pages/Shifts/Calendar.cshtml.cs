using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.Security;
using OSS.App.Shifts.Queries.GetEmployeeCalendarById;
using OSS.Domain.Entities;

namespace OSS.Web;

[Authorize(Roles = SecurityConstants.GuestRoleString)]
public class CalendarModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public CalendarModel(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [BindProperty]
    public GetEmployeeCalendarByIdQuery Query { get; set; }
    public CalendarDTO Calendar { get; set; }

    public async Task OnGet()
    {
        // set start and end dates as this month
        DateTime today = DateTime.Now;
        Query = new GetEmployeeCalendarByIdQuery
        {
            StartDate = new DateTime(today.Year, today.Month, 1)
        };
        Query.EndDate = Query.StartDate.AddMonths(1).AddDays(-1);
        Query.EmployeeId = _userManager.GetUserId(User);
        // get employee data
        Calendar = await _mediator.Send(Query);
    }

    public async Task OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return;
        }
        Calendar = await _mediator.Send(Query);
    }
}
