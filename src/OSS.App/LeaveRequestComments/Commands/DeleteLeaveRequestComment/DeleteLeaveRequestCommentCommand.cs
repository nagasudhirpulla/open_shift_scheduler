using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequestComments.Commands.DeleteLeaveRequestComment;
public class DeleteLeaveRequestCommentCommand : IRequest<LeaveRequestComment>
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public bool IsUserAdmin { get; set; }
}

public class DeleteLeaveRequestCommentCommandHandler : IRequestHandler<DeleteLeaveRequestCommentCommand, LeaveRequestComment>
{
    private readonly AppIdentityDbContext _context;

    public DeleteLeaveRequestCommentCommandHandler(AppIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveRequestComment> Handle(DeleteLeaveRequestCommentCommand request, CancellationToken cancellationToken)
    {
        LeaveRequestComment leaveReqComm = await _context.LeaveRequestComments.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

        if (leaveReqComm == null)
        {
            throw new Exception("Leave Request Comment not found for deletion...");
        }
        else if (!request.IsUserAdmin && leaveReqComm.CreatedById != request.UserId)
        {
            throw new Exception("non-admin user who did not create the leave request comment is trying to delete it...");
        }

        _context.LeaveRequestComments.Remove(leaveReqComm);
        await _context.SaveChangesAsync(cancellationToken);

        return leaveReqComm;
    }
}