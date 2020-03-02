using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSS.App.Security;
using OSS.App.ShiftGroups.Queries.GetShiftGroups;
using OSS.Domain.Entities;

namespace OSS.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class ShiftGroupsController : ControllerBase
    {
        public IMediator _mediator { get; private set; }

        public ShiftGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/ShiftGroups
        [HttpGet]
        public async Task<IEnumerable<ShiftGroup>> GetEmployees()
        {
            /**
             * User are to be sorted by username
             * Admin user not to be included
             * **/
            return (await _mediator.Send(new GetShiftGroupsQuery()));
        }
    }
}