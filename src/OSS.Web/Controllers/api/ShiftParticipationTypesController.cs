using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSS.App.Security;
using OSS.App.ShiftParticipationTypes.Queries.GetShiftParticipationTypes;
using OSS.Domain.Entities;

namespace OSS.Web.Controllers.api;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class ShiftParticipationTypesController : ControllerBase
{
    public IMediator _mediator { get; private set; }

    public ShiftParticipationTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/ShiftParticipationTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShiftParticipationType>>> GetShiftParticipationTypes()
    {
        return await _mediator.Send(new GetShiftParticipationTypesQuery());
    }
}
