using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenShiftScheduler.Data;
using OpenShiftScheduler.Models.AppModels;

namespace OpenShiftScheduler.Controllers
{
    [EnableCors("anyorigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsApiController : ControllerBase
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftsApiController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/ShiftsApi
        [HttpGet]
        public IEnumerable<Shift> GetShifts()
        {
            return _context.Shifts.Include(s=>s.ShiftParticipations);
        }

        // GET: api/ShiftsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShift([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shift = await _context.Shifts.FindAsync(id);

            if (shift == null)
            {
                return NotFound();
            }

            return Ok(shift);
        }

        // PUT: api/ShiftsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShift([FromRoute] int id, [FromBody] Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shift.ShiftId)
            {
                return BadRequest();
            }

            _context.Entry(shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftExists(id))
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

        // POST: api/ShiftsApi
        [HttpPost]
        public async Task<IActionResult> PostShift([FromBody] Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShift", new { id = shift.ShiftId }, shift);
        }

        // DELETE: api/ShiftsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return Ok(shift);
        }

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.ShiftId == id);
        }
    }
}