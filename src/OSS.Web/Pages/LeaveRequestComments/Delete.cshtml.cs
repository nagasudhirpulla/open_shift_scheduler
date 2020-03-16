using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.LeaveRequestComments.Commands.DeleteLeaveRequestComment;
using OSS.App.LeaveRequestComments.Queries.GetLeaveRequestCommentById;
using OSS.App.Security;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.LeaveRequestComments
{
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
        public LeaveRequestComment LeaveRequestComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveRequestComment = await _mediator.Send(new GetLeaveRequestCommentByIdQuery() { Id = id.Value });

            if (LeaveRequestComment == null)
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


            LeaveRequestComment = await _mediator.Send(new GetLeaveRequestCommentByIdQuery() { Id = id.Value });
            if (LeaveRequestComment == null)
            {
                return NotFound();
            }

            LeaveRequestComment lrc = await _mediator.Send(new DeleteLeaveRequestCommentCommand() { Id = id.Value, UserId = userId, IsUserAdmin = User.IsInRole(SecurityConstants.AdminRoleString) });

            return RedirectToPage("/LeaveRequests/Details", new { Id = lrc.LeaveRequestId });
        }
    }
}