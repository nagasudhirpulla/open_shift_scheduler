using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftParticipations.Commands.MoveShiftParticipation
{
    public class MoveShiftParticipationCommandHandler : IRequestHandler<MoveShiftParticipationCommand, Shift>
    {
        private readonly AppIdentityDbContext _context;

        public MoveShiftParticipationCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<Shift> Handle(MoveShiftParticipationCommand request, CancellationToken cancellationToken)
        {
            if (request.Direction != -1 && request.Direction != 1)
            {
                return null;
            }

            //get the id of the shift that contains the participation to move
            ShiftParticipation shiftParticipation = await _context.ShiftParticipations.FindAsync(request.ShiftParticipationId);
            if (shiftParticipation == null)
            {
                return null;
            }

            // get the shift that has the participation to be moved
            Shift shift = await _context.Shifts.Include(s => s.ShiftParticipations).FirstOrDefaultAsync(s => s.Id == shiftParticipation.ShiftId);
            if (shift == null)
            {
                return null;
            }
            // get the interested shift participation
            ShiftParticipation interestedShiftPart = shift.ShiftParticipations.SingleOrDefault(sp => sp.Id == request.ShiftParticipationId);
            ShiftParticipation nextPart = null;

            // find the next shift Participation sequence
            foreach (ShiftParticipation shiftPart in shift.ShiftParticipations)
            {
                if (shiftPart.Id != request.ShiftParticipationId)
                {
                    if (request.Direction == -1 && shiftPart.ParticipationSequence >= interestedShiftPart.ParticipationSequence)
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
                    else if (request.Direction == 1 && shiftPart.ParticipationSequence <= interestedShiftPart.ParticipationSequence)
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
                    if (request.Direction == -1)
                    {
                        // if direction is -1, then we have move the participation down, i.e., add one to sequence
                        interestedShiftPart.ParticipationSequence += 1;
                        _context.Entry(interestedShiftPart).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    else if (request.Direction == 1)
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
                                if (shiftPart.Id != interestedShiftPart.Id)
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
            return await _context.Shifts.Include(s => s.ShiftParticipations).FirstOrDefaultAsync(s => s.Id == shift.Id);
        }
    }
}
