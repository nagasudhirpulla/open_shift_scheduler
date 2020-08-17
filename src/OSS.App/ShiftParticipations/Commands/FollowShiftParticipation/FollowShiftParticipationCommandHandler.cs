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
                ShiftParticipation newPart = new ShiftParticipation
                {
                    EmployeeId = request.NewEmployeeId,
                    ShiftId = sp.ShiftId,
                    ShiftParticipationTypeId = request.NewParticipationTypeId
                };
                try
                {
                    _context.ShiftParticipations.Add(newPart);
                    await _context.SaveChangesAsync();
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
