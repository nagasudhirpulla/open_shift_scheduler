using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Commands.DeleteLeaveRequest;
public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, LeaveRequest>
{
    private readonly AppIdentityDbContext _context;

    public DeleteLeaveRequestCommandHandler(AppIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveRequest> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveReq = await _context.LeaveRequests.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

        if (leaveReq == null)
        {
            throw new Exception("Leave Request not found for deletion...");
        }
        else if (!request.IsUserAdmin && leaveReq.EmployeeId != request.UserId)
        {
            throw new Exception("non-admin user who did not create the leave request is trying to delete a request...");
        }
        else if (!request.IsUserAdmin && leaveReq.IsApproved == true)
        {
            throw new Exception("Non-admin is trying to delete a leave Request already executed, hence cannot be deleted");
        }

        _context.LeaveRequests.Remove(leaveReq);
        await _context.SaveChangesAsync(cancellationToken);

        return leaveReq;
    }
}