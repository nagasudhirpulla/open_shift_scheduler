using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Commands.ExecuteLeaveRequest;
public class ExecuteLeaveRequestCommandHandler : IRequestHandler<ExecuteLeaveRequestCommand, LeaveRequest>
{
    private readonly AppIdentityDbContext _context;

    public ExecuteLeaveRequestCommandHandler(AppIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveRequest> Handle(ExecuteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest lr = await _context.LeaveRequests.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

        // get the participation type id of this leave request
        int leaveSpTypeId = lr.LeaveTypeId;
        // get all the shift participations that satisfy the leave request
        var spList = await _context.ShiftParticipations
                        .Where(sp => sp.Shift.ShiftDate >= lr.StartDate
                            && sp.Shift.ShiftDate <= lr.EndDate
                            && sp.EmployeeId == lr.EmployeeId
                            && sp.ShiftParticipationTypeId != leaveSpTypeId)
                        .ToListAsync(cancellationToken: cancellationToken);
        foreach (ShiftParticipation sp in spList)
        {
            sp.ShiftParticipationTypeId = leaveSpTypeId;
            _context.Attach(sp).State = EntityState.Modified;
        }
        lr.IsExecuted = true;
        _context.Attach(lr).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);

        return lr;
    }
}