using MediatR;

namespace OSS.App.Security.Commands.DeleteAppUser;

public class DeleteAppUserCommand : IRequest<List<string>>
{
    public string Id { get; set; }
}
