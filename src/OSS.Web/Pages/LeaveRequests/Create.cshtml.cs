using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSS.App.LeaveRequests.Commands.CreateLeaveRequest;
using OSS.App.Security;
using OSS.App.Security.Queries.GetAppUsers;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.LeaveRequests
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateModel(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [BindProperty]
        public LeaveRequest LeaveRequest { get; set; }
        public async Task<IActionResult> OnGet()
        {
            LeaveRequest = new LeaveRequest
            {
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date,
            };
            if (!User.IsInRole(SecurityConstants.AdminRoleString))
            {
                LeaveRequest.EmployeeId = _userManager.GetUserId(User);
            }
            else
            {
                ViewData["EmployeeId"] = new SelectList((await _mediator.Send(new GetAppUsersListQuery())).Users, "UserId", "DisplayName");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // validate request
            CreateLeaveRequestCommand command = new CreateLeaveRequestCommand { LeaveRequest = LeaveRequest, UserId = _userManager.GetUserId(User), IsUserAdmin = User.IsInRole(SecurityConstants.AdminRoleString) };
            var validator = new CreateLeaveRequestCommandValidator();
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, null);
                return Page();
            }

            LeaveRequest lr = await _mediator.Send(command);
            return RedirectToPage("./Index");
        }
    }
}