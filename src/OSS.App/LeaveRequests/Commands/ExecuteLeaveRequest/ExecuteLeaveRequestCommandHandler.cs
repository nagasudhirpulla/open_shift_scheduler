using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.LeaveRequests.Commands.ExecuteLeaveRequest
{
    public class ExecuteLeaveRequestCommandHandler : IRequestHandler<ExecuteLeaveRequestCommand, LeaveRequest>
    {
        private readonly AppIdentityDbContext _context;

        public ExecuteLeaveRequestCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<LeaveRequest> Handle(ExecuteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            LeaveRequest lr = await _context.LeaveRequests.FindAsync(request.Id);

            // get the participation type id of "Leave"
            int leaveSpTypeId = (await _context.ShiftParticipationTypes.Where(spt => spt.Name.ToLower().Equals("leave")).FirstOrDefaultAsync()).Id;
            // get all the shift participations that satisfy the leave request
            var spList = await _context.ShiftParticipations
                            .Where(sp => sp.Shift.ShiftDate >= lr.StartDate
                                && sp.Shift.ShiftDate <= lr.EndDate
                                && sp.EmployeeId == lr.EmployeeId
                                && sp.ShiftParticipationTypeId != leaveSpTypeId)
                            .ToListAsync();
            foreach (ShiftParticipation sp in spList)
            {
                sp.ShiftParticipationTypeId = leaveSpTypeId;
                _context.Attach(sp).State = EntityState.Modified;
            }
            lr.IsExecuted = true;
            _context.Attach(lr).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return lr;
        }
    }
}
