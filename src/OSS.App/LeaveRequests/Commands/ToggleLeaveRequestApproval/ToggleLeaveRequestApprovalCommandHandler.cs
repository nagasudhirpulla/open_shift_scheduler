using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Commands.ToggleLeaveRequestApproval;
public class ToggleLeaveRequestApprovalCommandHandler : IRequestHandler<ToggleLeaveRequestApprovalCommand, LeaveRequest>
{
    private readonly AppIdentityDbContext _context;

    public ToggleLeaveRequestApprovalCommandHandler(AppIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveRequest> Handle(ToggleLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest lr = await _context.LeaveRequests.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

        lr.IsApproved = !lr.IsApproved;
        _context.Attach(lr).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);

        return lr;
    }
}