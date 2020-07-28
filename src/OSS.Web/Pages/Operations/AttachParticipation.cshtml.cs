using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSS.App.Security;
using OSS.App.Security.Queries.GetAppUsers;
using OSS.App.ShiftParticipations.Commands.FollowShiftParticipation;
using OSS.App.ShiftParticipationTypes.Queries.GetShiftParticipationTypes;
using OSS.Domain.Entities;

namespace OSS.Web
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class AttachParticipationModel : PageModel
    {
        [BindProperty]
        public FollowShiftParticipationCommand Command { get; set; }

        private readonly IMediator _mediator;
        public AttachParticipationModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["EmployeeId"] = new SelectList((await _mediator.Send(new GetAppUsersListQuery())).Users, "UserId", "DisplayName");

            List<ShiftParticipationType> shiftPartTypes = await _mediator.Send(new GetShiftParticipationTypesQuery());
            ViewData["PartTypeId"] = new SelectList(shiftPartTypes, "Id", "Name");

            List<ShiftParticipationType> shiftPartTypesWithNull = new List<ShiftParticipationType>(shiftPartTypes);
            shiftPartTypesWithNull.Insert(0, new ShiftParticipationType() { Id = -1, Name = "Any" });
            ViewData["PartTypeIdWithNull"] = new SelectList(shiftPartTypesWithNull, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // validate request
            var validator = new FollowShiftParticipationCommandValidator();
            var validationResult = validator.Validate(Command);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, null);
                return Page();
            }

            bool isSuccess = await _mediator.Send(Command);
            return RedirectToPage("/Shifts/Edit");
        }
    }
}