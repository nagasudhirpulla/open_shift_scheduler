using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSS.App.Security;
using OSS.App.Shifts.Commands.CreateShift;
using OSS.App.Shifts.Commands.DeleteShift;
using OSS.App.Shifts.Commands.EditShift;
using OSS.App.Shifts.Commands.EditShiftComments;
using OSS.App.Shifts.Queries.GetShiftBetweenDates;
using OSS.App.Shifts.Queries.GetShiftById;
using OSS.App.Shifts.Queries.GetShifts;
using OSS.Domain.Entities;

namespace OSS.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class ShiftsController : ControllerBase
    {
        public IMediator _mediator { get; private set; }

        public ShiftsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
        {
            return await _mediator.Send(new GetShiftsQuery());
        }

        // GET: api/Shifts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> GetShiftById([FromRoute]int id)
        {
            Shift s = await _mediator.Send(new GetShiftByIdQuery() { Id = id });
            if (s == null)
            {
                return NotFound();
            }
            return s;
        }

        // GET: api/Shifts/BetweenDates
        [HttpGet("BetweenDates")]
        public async Task<ActionResult<IEnumerable<Shift>>> GetShiftsBetweenDates([FromQuery]string start_date, [FromQuery]string end_date)
        {
            DateTime startDate = DateTime.ParseExact(start_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(end_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return await _mediator.Send(new GetShiftsBetweenDatesQuery() { StartDate = startDate, EndDate = endDate });
        }

        // PUT: api/Shifts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditShift([FromRoute] int id, [FromBody] Shift shift)
        {
            if (id != shift.Id)
            {
                return BadRequest();
            }
            bool res = await _mediator.Send(new EditShiftCommand() { Shift = shift });
            if (res == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Shifts/Comments
        [HttpPut("Comments")]
        public async Task<IActionResult> EditShiftComments(EditShiftCommentsCommand command)
        {
            bool res = await _mediator.Send(command);
            if (res == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Shifts
        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] Shift shift)
        {
            Shift s = await _mediator.Send(new CreateShiftCommand() { Shift = shift });

            return CreatedAtAction("GetShiftById", new { id = shift.Id }, s);
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift([FromRoute] int id)
        {
            Shift shift = await _mediator.Send(new DeleteShiftCommand() { Id = id });
            if (shift == null)
            {
                return NotFound();
            }
            return Ok(shift);
        }
    }
}