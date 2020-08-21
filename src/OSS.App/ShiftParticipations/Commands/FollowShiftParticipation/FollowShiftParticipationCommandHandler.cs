using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftParticipations.Commands.FollowShiftParticipation
{
    public class FollowShiftParticipationCommandHandler : IRequestHandler<FollowShiftParticipationCommand, bool>
    {
        private readonly AppIdentityDbContext _context;

        public FollowShiftParticipationCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(FollowShiftParticipationCommand request, CancellationToken cancellationToken)
        {
            // get all the shift participations to follow
            var spList = await _context.ShiftParticipations
                                .Where(sp => (sp.Shift.ShiftDate >= request.StartDate)
                                             && (sp.Shift.ShiftDate <= request.EndDate)
                                             && (sp.EmployeeId == request.TargetEmployeeId)
                                             && (request.TargetParticipationTypeId == -1 ? true : (sp.ShiftParticipationTypeId == request.TargetParticipationTypeId))
                                             )
                                .ToListAsync();

            // create similar shift participations for the new employee
            foreach (ShiftParticipation sp in spList)
            {
                int newPartTypeId = request.NewParticipationTypeId;
                newPartTypeId = (newPartTypeId == -1) ? sp.ShiftParticipationTypeId : newPartTypeId;

                int newPartSeq = request.NewParticipationSequence;
                newPartSeq += -1;
                newPartSeq = (newPartSeq == -1) ? sp.ParticipationSequence : newPartSeq;

                ShiftParticipation newPart = new ShiftParticipation
                {
                    EmployeeId = request.NewEmployeeId,
                    ShiftId = sp.ShiftId,
                    ShiftParticipationTypeId = newPartTypeId,
                    ParticipationSequence = newPartSeq
                };

                try
                {
                    if (request.NewEmployeeId != request.TargetEmployeeId)
                    {
                        // we are trying to follow an employee participation
                        _context.ShiftParticipations.Add(newPart);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // we are trying to modify an existing participation
                        sp.EmployeeId = newPart.EmployeeId;
                        sp.ShiftId = newPart.ShiftId;
                        sp.ShiftParticipationTypeId = newPart.ShiftParticipationTypeId;
                        sp.ParticipationSequence = newPart.ParticipationSequence;
                        _context.Entry(sp).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return true;
        }
    }
}
