using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.LeaveRequests.Commands.DeleteLeaveRequest;
using OSS.App.LeaveRequests.Queries.GetLeaveRequestById;
using OSS.App.Security;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.LeaveRequests;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteModel(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [BindProperty]
    public LeaveRequest LeaveRequest { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
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
        string userId = _userManager.GetUserId(User);


        LeaveRequest = await _mediator.Send(new GetLeaveRequestByIdQuery() { Id = id.Value });
        if (LeaveRequest == null)
        {
            return NotFound();
        }

        LeaveRequest lr = await _mediator.Send(new DeleteLeaveRequestCommand() { Id = id.Value, UserId = userId, IsUserAdmin = User.IsInRole(SecurityConstants.AdminRoleString) });

        return RedirectToPage("./Index");
    }
}
