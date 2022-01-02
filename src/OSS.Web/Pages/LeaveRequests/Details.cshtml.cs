using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.LeaveRequests.Commands.AddCommentToLeaveRequest;
using OSS.App.LeaveRequests.Commands.ToggleLeaveRequestApproval;
using OSS.App.LeaveRequests.Queries.GetLeaveRequestById;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.LeaveRequests;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public LeaveRequest LeaveRequest { get; set; }

    [BindProperty]
    public string Comment { get; set; }

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        LeaveRequest = await _mediator.Send(new GetLeaveRequestByIdQuery() { Id = id.Value });

        if (LeaveRequest == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        LeaveRequest = await _mediator.Send(new GetLeaveRequestByIdQuery() { Id = id.Value });

        if (LeaveRequest == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(Comment))
        {
            ModelState.AddModelError("Comment", "Comments cannot be empty");
            return Page();
        }

        var comment = new LeaveRequestComment() { Comment = Comment, LeaveRequestId = id.Value };
        LeaveRequestComment res = await _mediator.Send(new AddCommentToLeaveRequestCommand() { LeaveRequestComment = comment });
        return RedirectToPage("./Details", new { Id = id });
    }

    public async Task<IActionResult> OnPostToggleApprovalAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        LeaveRequest = await _mediator.Send(new GetLeaveRequestByIdQuery() { Id = id.Value });

        if (LeaveRequest == null)
        {
            return NotFound();
        }

        LeaveRequest lr = await _mediator.Send(new ToggleLeaveRequestApprovalCommand() { Id = id.Value });
        return RedirectToPage("./Details", new { Id = id });
    }
}
