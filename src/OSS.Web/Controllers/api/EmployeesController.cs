using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSS.App.Security;
using OSS.App.Security.Queries.GetAppUsers;

namespace OSS.Web.Controllers.api;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class EmployeesController : ControllerBase
{
    public IMediator _mediator { get; private set; }

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Employees
    [HttpGet]
    public async Task<IEnumerable<UserDTO>> GetEmployees()
    {
        /**
         * User are to be sorted by username
         * Admin user not to be included
         * **/
        return (await _mediator.Send(new GetAppUsersListQuery())).Users;
    }
}
