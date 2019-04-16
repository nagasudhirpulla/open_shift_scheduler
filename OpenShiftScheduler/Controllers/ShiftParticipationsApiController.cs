using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenShiftScheduler.Data;
using OpenShiftScheduler.Models.AppModels;

namespace OpenShiftScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftParticipationsApiController : ControllerBase
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftParticipationsApiController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/ShiftParticipationsApi
        [HttpGet]
        public IEnumerable<ShiftParticipation> GetShiftParticipations()
        {
            return _context.ShiftParticipations.Include(sp => sp.Employee);
        }

        // GET: api/ShiftParticipationsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShiftParticipation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shiftParticipation = await _context.ShiftParticipations.FindAsync(id);

            if (shiftParticipation == null)
            {
                return NotFound();
            }

            return Ok(shiftParticipation);
        }

        // PUT: api/ShiftParticipationsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftParticipation([FromRoute] int id, [FromBody] ShiftParticipation shiftParticipation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shiftParticipation.ShiftParticipationId)
            {
                return BadRequest();
            }

            _context.Entry(shiftParticipation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftParticipationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShiftParticipationsApi
        [HttpPost]
        public async Task<IActionResult> PostShiftParticipation([FromBody] ShiftParticipation shiftParticipation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShiftParticipations.Add(shiftParticipation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShiftParticipation", new { id = shiftParticipation.ShiftParticipationId }, shiftParticipation);
        }

        // DELETE: api/ShiftParticipationsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftParticipation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shiftParticipation = await _context.ShiftParticipations.FindAsync(id);
            if (shiftParticipation == null)
            {
                return NotFound();
            }

            _context.ShiftParticipations.Remove(shiftParticipation);
            await _context.SaveChangesAsync();

            return Ok(shiftParticipation);
        }

        private bool ShiftParticipationExists(int id)
        {
            return _context.ShiftParticipations.Any(e => e.ShiftParticipationId == id);
        }
    }
}