using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OSS.App.Genders.Queries.GetGenders;
using OSS.App.Security;
using OSS.App.Security.Commands.CreateAppUser;
using OSS.App.ShiftGroups.Queries.GetShiftGroups;
using OSS.App.ShiftRoles.Queries.GetShiftRoles;
using OSS.Web.Extensions;

namespace OSS.Web.Pages.Users;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class CreateModel : PageModel
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    [BindProperty]
    public CreateAppUserCommand NewUser { get; set; }

    public CreateModel(ILogger<CreateModel> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    public async Task OnGetAsync()
    {
        await InitSelectListsAsync();
        NewUser = new();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        NewUser.BaseUrl = new Uri(new Uri(Request.Scheme + "://" + Request.Host), "roster/Identity/Account/ConfirmEmail").ToString();
        IdentityResult result = await _mediator.Send(NewUser);
        if (result.Succeeded)
        {
            _logger.LogInformation("Created new account for {username}", NewUser.Username);
            return RedirectToPage("./Index").WithSuccess($"Created new user {NewUser.Username}");
        }
        AddErrors(result);

        await InitSelectListsAsync();

        // If we got this far, something failed, redisplay form
        return Page();
    }

    private async Task InitSelectListsAsync()
    {
        ViewData["GenderId"] = new SelectList(await _mediator.Send(new GetGendersQuery()), "Id", "Name");
        ViewData["ShiftGroupId"] = new SelectList(await _mediator.Send(new GetShiftGroupsQuery()), "Id", "Name");
        ViewData["ShiftRoleId"] = new SelectList(await _mediator.Send(new GetShiftRolesQuery()), "Id", "RoleName");
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
