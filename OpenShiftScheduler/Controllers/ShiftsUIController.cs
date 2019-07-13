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

        // POST: api/ShiftsUI/MoveShiftParticipation/5
        [HttpPost("MoveShiftParticipation/{shiftPartId}")]
        public async Task<IActionResult> MoveShiftParticipation([FromRoute]int shiftPartId, [FromBody] ShiftPartMoveFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (formModel.Direction != -1 && formModel.Direction != 1)
            {
                return BadRequest();
            }

            //get the id of the shift that contains the participation to move
            ShiftParticipation shiftParticipation = await _context.ShiftParticipations.FindAsync(shiftPartId);
            if (shiftParticipation == null)
            {
                return BadRequest();
            }

            // get the shift that has the participation to be moved
            Shift shift = await _context.Shifts.Include(s => s.ShiftParticipations).FirstOrDefaultAsync(s => s.ShiftId == shiftParticipation.ShiftId);
            if (shift == null)
            {
                return BadRequest();
            }
            // get the interested shift participation
            ShiftParticipation interestedShiftPart = shift.ShiftParticipations.SingleOrDefault(sp => sp.ShiftParticipationId == shiftPartId);
            ShiftParticipation nextPart = null;

            // find the next shift Participation sequence
            foreach (ShiftParticipation shiftPart in shift.ShiftParticipations)
            {
                if (shiftPart.ShiftParticipationId != shiftPartId)
                {
                    if (formModel.Direction == -1 && shiftPart.ParticipationSequence >= interestedShiftPart.ParticipationSequence)
                    {
                        if (nextPart == null)
                        {
                            nextPart = shiftPart;
                        }
                        else
                        {
                            if (shiftPart.ParticipationSequence < nextPart.ParticipationSequence)
                            {
                                nextPart = shiftPart;
                            }
                        }
                    }
                    else if (formModel.Direction == 1 && shiftPart.ParticipationSequence <= interestedShiftPart.ParticipationSequence)
                    {
                        if (nextPart == null)
                        {
                            nextPart = shiftPart;
                        }
                        else
                        {
                            if (shiftPart.ParticipationSequence > nextPart.ParticipationSequence)
                            {
                                nextPart = shiftPart;
                            }
                        }
                    }
                }
            }

            // if there is no next participation, do nothing
            if (nextPart != null)
            {
                if (nextPart.ParticipationSequence != interestedShiftPart.ParticipationSequence)
                {
                    // swap the sequences if possible
                    int tempSeq = nextPart.ParticipationSequence;
                    nextPart.ParticipationSequence = interestedShiftPart.ParticipationSequence;
                    interestedShiftPart.ParticipationSequence = tempSeq;
                    _context.Entry(interestedShiftPart).State = EntityState.Modified;
                    _context.Entry(nextPart).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (formModel.Direction == -1)
                    {
                        // if direction is -1, then we have move the participation down, i.e., add one to sequence
                        interestedShiftPart.ParticipationSequence += 1;
                        _context.Entry(interestedShiftPart).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    else if (formModel.Direction == 1)
                    {
                        if (interestedShiftPart.ParticipationSequence > 0)
                        {
                            // if direction is 1, then we have move the participation up, i.e., subtract one to sequence    
                            interestedShiftPart.ParticipationSequence -= 1;
                            _context.Entry(interestedShiftPart).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // if direction is 1, then we have move the participation up, add one to all other since already seq < zero    
                            foreach (ShiftParticipation shiftPart in shift.ShiftParticipations)
                            {
                                if (shiftPart.ShiftParticipationId != interestedShiftPart.ShiftParticipationId)
                                {
                                    shiftPart.ParticipationSequence += 1;
                                    _context.Entry(shiftPart).State = EntityState.Modified;
                                }
                            }
                            await _context.SaveChangesAsync();
                        }

                    }
                }

            }
            return Ok(await _context.Shifts.Include(s => s.ShiftParticipations).FirstOrDefaultAsync(s => s.ShiftId == shift.ShiftId));
        }

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.ShiftId == id);
        }

    }
}