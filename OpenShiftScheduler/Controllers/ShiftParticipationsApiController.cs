using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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

        // GET: api/ShiftParticipationsApi/FromShift/5
        [HttpGet("FromShift/{shiftId}")]
        public async Task<IActionResult> GetShiftParticipationFromShift([FromRoute] int shiftId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<ShiftParticipation> shiftParticipations = await _context.ShiftParticipations.Where(sp => sp.ShiftId == shiftId).ToListAsync();

            if (shiftParticipations == null)
            {
                return NotFound();
            }

            return Ok(shiftParticipations);
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

        // POST: api/ShiftParticipationsApi/FromGroup
        [HttpPost("FromGroup")]
        public async Task<IActionResult> PostShiftParticipationFromGroup([FromBody] ShiftGroupPartAddParamsViewModel paramsVm)
        {
            //todo complete this
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int shiftGroupId = paramsVm.ShiftGroupId;
            int shiftId = paramsVm.ShiftId;

            // get the employees in the shiftGroup
            ShiftGroup shiftGroup = await _context.ShiftGroups.Include(sg => sg.Employees).FirstOrDefaultAsync(sg => sg.ShiftGroupId == shiftGroupId);
            List<int> empIds = shiftGroup.Employees.Select(empl => empl.EmployeeId).ToList();
            foreach (int empId in empIds)
            {
                try
                {
                    // check if participation exists
                    if ((await _context.ShiftParticipations.Where(sp => (sp.ShiftId == shiftId) && (sp.EmployeeId == empId)).ToListAsync()).Count > 0)
                    {
                        continue;
                    }
                    _context.ShiftParticipations.Add(new ShiftParticipation { EmployeeId = empId, ShiftId = shiftId });
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error occured while adding participation of employee id {empId} to shift id {shiftId} -- {ex.Message}");
                    continue;
                }
            }

            return CreatedAtAction("GetShiftParticipationFromShift", new { shiftId }, await _context.ShiftParticipations.Where(sp => sp.ShiftId == shiftId).ToListAsync());
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

    public class ShiftGroupPartAddParamsViewModel
    {
        public int ShiftId { get; set; }
        public int ShiftGroupId { get; set; }
    }
}