using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OSS.App.Security;
using OSS.App.Security.Commands.DeleteAppUser;
using OSS.App.Security.Queries.GetAppUserById;
using OSS.App.Security.Queries.GetAppUsers;
using OSS.Web.Extensions;

namespace OSS.Web.Pages.Users;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class DeleteModel : PageModel
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public DeleteModel(ILogger<DeleteModel> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    public UserDTO Usr { get; set; }
    [BindProperty]
    public DeleteAppUserCommand UsrDelCmd { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        Usr = await _mediator.Send(new GetAppUserByIdQuery() { Id = id });

        if (Usr == null)
        {
            return NotFound();
        }
        UsrDelCmd = new() { Id = id };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        List<string> errs = await _mediator.Send(UsrDelCmd);

        if (errs.Count == 0)
        {
            _logger.LogInformation("User deleted successfully");
            return RedirectToPage("./Index").WithSuccess("User deletion done");
        }

        // If we got this far, something failed, redisplay form
        AddErrors(errs);
        Usr = await _mediator.Send(new GetAppUserByIdQuery() { Id = UsrDelCmd.Id });
        return Page();
    }

    private void AddErrors(IEnumerable<string> errs)
    {
        foreach (string error in errs)
        {
            ModelState.AddModelError(string.Empty, error);
        }
    }
}
