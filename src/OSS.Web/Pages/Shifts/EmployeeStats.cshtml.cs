using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.Shifts.Queries.GetAllEmployeeStats;

namespace OSS.Web.Pages.Shifts;

[Authorize]
public class EmployeeStatsModel : PageModel
{
    private readonly IMediator _mediator;
    public EmployeeStatsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [BindProperty]
    public GetAllEmployeeStatsQuery Query { get; set; }

    public List<EmployeeStatsDTO> EmployeeStats { get; set; }
    public async Task<IActionResult> OnGet()
    {
        // set start and end dates as this month
        DateTime today = DateTime.Now;
        Query = new GetAllEmployeeStatsQuery
        {
            StartDate = new DateTime(today.Year, today.Month, 1)
        };
        Query.EndDate = Query.StartDate.AddMonths(1).AddDays(-1);
        // get employee stats
        EmployeeStats = await _mediator.Send(Query);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var validator = new GetAllEmployeeStatsQueryValidator();
        var validationResult = validator.Validate(Query);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, null);
            return Page();
        }
        // get roster data
        EmployeeStats = await _mediator.Send(Query);
        return Page();
    }
}
