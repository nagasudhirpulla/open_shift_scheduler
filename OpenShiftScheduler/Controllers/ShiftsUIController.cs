using System;
using System.Collections.Generic;
using System.Globalization;
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
    [Authorize(Roles = "Administrator")]
    public class ShiftsUIController : ControllerBase
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftsUIController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/ShiftsUI/shifts
        [HttpGet("shifts")]
        public async Task<IEnumerable<Shift>> GetShifts([FromQuery]string start_date, [FromQuery]string end_date)
        {
            DateTime startDate = DateTime.ParseExact(start_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(end_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return await _context.Shifts.Where(s => s.ShiftDate >= startDate && s.ShiftDate <= endDate).Include(s => s.ShiftParticipations).ToListAsync();
        }

        // PUT: api/ShiftsUI/PutShiftComments/5
        [HttpPut("PutShiftComments/{id}")]
        public async Task<IActionResult> PutShiftComments([FromRoute] int id, [FromBody] CommentsEditViewModel shiftComments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // get the required shift by id
            Shift shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return BadRequest();
            }

            //edit the comments
            shift.Comments = shiftComments.Comments;

            // save changes
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

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.ShiftId == id);
        }

    }

    public class CommentsEditViewModel
    {
        public string Comments { get; set; }
    }
}