using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.LeaveRequests.Queries.GetLeaveRequests;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.LeaveRequests;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IMediator _mediator;
    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IList<LeaveRequest> LeaveRequests { get; set; }

    public async Task OnGet()
    {
        LeaveRequests = await _mediator.Send(new GetLeaveRequestsQuery());
    }
}
