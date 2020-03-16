using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.LeaveRequests.Queries.GetLeaveRequestById;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.LeaveRequests
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
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
    }
}