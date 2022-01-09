using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OSS.App.Genders.Queries.GetGenders;
using OSS.App.Security;
using OSS.App.Security.Commands.EditAppUser;
using OSS.App.Security.Queries.GetRawRawAppUserById;
using OSS.App.ShiftGroups.Queries.GetShiftGroups;
using OSS.App.ShiftRoles.Queries.GetShiftRoles;
using OSS.Domain.Entities;
using OSS.Web.Extensions;

namespace OSS.Web.Pages.Users;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class EditModel : PageModel
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public EditModel(ILogger<EditModel> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    [BindProperty]
    public EditAppUserCommand Usr { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        ApplicationUser user = await _mediator.Send(new GetRawAppUserByIdQuery() { Id = id });
        if (user == null)
        {
            return NotFound();
        }

        Usr = _mapper.Map<EditAppUserCommand>(user);

        await InitSelectListsAsync();

        // If we got this far, something failed, redisplay form
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        List<string> errors = await _mediator.Send(Usr);
        AddErrors(errors);

        // check if we have any errors and redirect if successful
        if (errors.Count == 0)
        {
            _logger.LogInformation("User edit operation successful");
            return RedirectToPage("./Index").WithSuccess("User Editing done");
        }

        await InitSelectListsAsync();
        // If we got this far, something failed, redisplay form
        return Page();
    }

    private async Task InitSelectListsAsync()
    {
        // ViewData["UserRole"] = new SelectList(SecurityConstants.GetRoles());
        ViewData["GenderId"] = new SelectList(await _mediator.Send(new GetGendersQuery()), "Id", "Name");
        ViewData["ShiftGroupId"] = new SelectList(await _mediator.Send(new GetShiftGroupsQuery()), "Id", "Name");
        ViewData["ShiftRoleId"] = new SelectList(await _mediator.Send(new GetShiftRolesQuery()), "Id", "RoleName");
    }

    private void AddErrors(IEnumerable<string> errs)
    {
        foreach (string error in errs)
        {
            ModelState.AddModelError(string.Empty, error);
        }
    }
}
