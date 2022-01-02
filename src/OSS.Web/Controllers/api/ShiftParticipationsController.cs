using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSS.App.Security;
using OSS.App.ShiftParticipations.Commands.CreateShiftParticipation;
using OSS.App.ShiftParticipations.Commands.CreateShiftParticipationsFromGroup;
using OSS.App.ShiftParticipations.Commands.DeleteShiftParticipation;
using OSS.App.ShiftParticipations.Commands.EditShiftParticipation;
using OSS.App.ShiftParticipations.Commands.MoveShiftParticipation;
using OSS.App.ShiftParticipations.Queries.GetShiftParticipation;
using OSS.App.ShiftParticipations.Queries.GetShiftParticipationsForShift;
using OSS.Domain.Entities;

namespace OSS.Web.Controllers.api;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class ShiftParticipationsController : ControllerBase
{
    public IMediator _mediator { get; private set; }

    public ShiftParticipationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/ShiftParticipations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ShiftParticipation>> GetShiftParticipation([FromRoute] int id)
    {
        ShiftParticipation shiftParticipation = await _mediator.Send(new GetShiftParticipationQuery() { Id = id });

        if (shiftParticipation == null)
        {
            return NotFound();
        }

        return Ok(shiftParticipation);
    }

    // GET: api/ShiftParticipations/ForShift
    [HttpGet("ForShift")]
    public async Task<ActionResult<IEnumerable<ShiftParticipation>>> GetShiftParticipationsForShift([FromRoute] int shiftId)
    {
        List<ShiftParticipation> shiftParticipations = await _mediator.Send(new GetShiftParticipationsForShiftQuery() { ShiftId = shiftId });

        if (shiftParticipations == null)
        {
            return NotFound();
        }

        return shiftParticipations;
    }

    // PUT: api/ShiftParticipations/5
    [HttpPut("{id}")]
    public async Task<IActionResult> EditShiftParticipation([FromRoute] int id, [FromBody] ShiftParticipation shiftParticipation)
    {
        if (id != shiftParticipation.Id)
        {
            return BadRequest();
        }

        bool res = await _mediator.Send(new EditShiftParticipationCommand() { ShiftParticipation = shiftParticipation });

        if (res == false)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/ShiftParticipations
    [HttpPost]
    public async Task<ActionResult<ShiftParticipation>> CreateShiftParticipation([FromBody] ShiftParticipation shiftParticipation)
    {
        ShiftParticipation sp = await _mediator.Send(new CreateShiftParticipationCommand() { ShiftParticipation = shiftParticipation });

        return CreatedAtAction("GetShiftParticipation", new { id = shiftParticipation.Id }, sp);
    }

    // POST: api/ShiftParticipations/FromGroup
    [HttpPost("FromGroup")]
    public async Task<IActionResult> CreateShiftParticipationsFromGroup([FromBody] CreateShiftParticipationsFromGroupCommand command)
    {
        List<ShiftParticipation> spList = await _mediator.Send(command);
        return CreatedAtAction("GetShiftParticipationsForShift", new { shiftId = command.ShiftId }, spList);
    }

    // POST: api/ShiftParticipations/Move
    [HttpPost("Move")]
    public async Task<ActionResult<Shift>> MoveShiftParticipation([FromBody] MoveShiftParticipationCommand command)
    {
        Shift shift = await _mediator.Send(command);
        if (shift == null)
        {
            NotFound();
        }
        return Ok(shift);
    }

    // DELETE: api/ShiftParticipations/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ShiftParticipation>> DeleteShiftParticipation([FromRoute] int id)
    {
        ShiftParticipation sp = await _mediator.Send(new DeleteShiftParticipationCommand() { Id = id });
        if (sp == null)
        {
            return NotFound();
        }
        return sp;
    }
}
