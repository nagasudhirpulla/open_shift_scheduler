using MediatR;
using Microsoft.AspNetCore.Identity;
using OSS.Domain.Entities;

namespace OSS.App.Security.Commands.DeleteAppUser;

public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommand, List<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteAppUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<string>> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
    {
        List<string> errors = new List<string>();
        ApplicationUser user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            errors.Add($"User not found with id {request.Id}");
        }

        IdentityResult result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            foreach (IdentityError err in result.Errors)
            {
                errors.Add(err.Description);
            }
        }

        return errors;
    }
}
