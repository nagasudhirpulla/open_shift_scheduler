using System.Collections.Generic;
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

namespace OSS.Web;

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
        Command = new FollowShiftParticipationCommand();

        List<ShiftParticipationType> shiftPartTypes = await _mediator.Send(new GetShiftParticipationTypesQuery());
        ViewData["PartTypeIdWithNull"] = new SelectList(GetShiftPartTypesWithAny(shiftPartTypes), "Id", "Name");
        ViewData["PartTypeId"] = new SelectList(GetShiftPartTypesWithSame(shiftPartTypes), "Id", "Name");
        ViewData["EmployeeId"] = new SelectList((await _mediator.Send(new GetAppUsersListQuery())).Users, "UserId", "DisplayName");

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
            List<ShiftParticipationType> shiftPartTypes = await _mediator.Send(new GetShiftParticipationTypesQuery());
            ViewData["PartTypeIdWithNull"] = new SelectList(GetShiftPartTypesWithAny(shiftPartTypes), "Id", "Name");
            ViewData["PartTypeId"] = new SelectList(GetShiftPartTypesWithSame(shiftPartTypes), "Id", "Name");
            ViewData["EmployeeId"] = new SelectList((await _mediator.Send(new GetAppUsersListQuery())).Users, "UserId", "DisplayName");
            return Page();
        }

        bool isSuccess = await _mediator.Send(Command);
        return RedirectToPage("/Shifts/Edit");
    }

    public List<ShiftParticipationType> GetShiftPartTypesWithSame(List<ShiftParticipationType> shiftPartTypes)
    {
        List<ShiftParticipationType> newShiftPartTypes = new List<ShiftParticipationType>(shiftPartTypes);
        newShiftPartTypes.Insert(0, new ShiftParticipationType() { Id = -1, Name = "Same" });
        return newShiftPartTypes;
    }

    public List<ShiftParticipationType> GetShiftPartTypesWithAny(List<ShiftParticipationType> shiftPartTypes)
    {
        List<ShiftParticipationType> newShiftPartTypes = new List<ShiftParticipationType>(shiftPartTypes);
        newShiftPartTypes.Insert(0, new ShiftParticipationType() { Id = -1, Name = "Any" });
        return newShiftPartTypes;
    }
}
