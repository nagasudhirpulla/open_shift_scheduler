using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSS.App.Security;
using OSS.App.ShiftTypes.Queries.GetShiftTypes;
using OSS.Domain.Entities;

namespace OSS.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class ShiftTypesController : ControllerBase
    {
        public IMediator _mediator { get; private set; }

        public ShiftTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/ShiftTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftType>>> GetShiftTypes()
        {
            return await _mediator.Send(new GetShiftTypesQuery());
        }
    }
}