using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.LeaveRequests.Commands.ExecuteLeaveRequest;
using OSS.App.LeaveRequests.Queries.GetLeaveRequestById;
using OSS.App.Security;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.LeaveRequests
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class ExecuteModel : PageModel
    {
        private readonly IMediator _mediator;

        public ExecuteModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public LeaveRequest LeaveRequest { get; set; }

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

            LeaveRequest lr = await _mediator.Send(new ExecuteLeaveRequestCommand() { Id = id.Value });
            return RedirectToPage("./Details", new { Id = id });
        }
    }
}