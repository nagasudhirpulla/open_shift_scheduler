using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.Security;
using OSS.App.Security.Queries.GetAppUsers;

namespace OSS.Web.Pages.Users;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    public UserListVM UsersData { get; set; }
    public async Task OnGetAsync()
    {
        UsersData = await _mediator.Send(new GetAppUsersListQuery());
    }
}
