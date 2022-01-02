using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Commands.AddCommentToLeaveRequest;
public class AddCommentToLeaveRequestCommandHandler : IRequestHandler<AddCommentToLeaveRequestCommand, LeaveRequestComment>
{
    private readonly AppIdentityDbContext _context;

    public AddCommentToLeaveRequestCommandHandler(AppIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveRequestComment> Handle(AddCommentToLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequestComment lrc = request.LeaveRequestComment;
        _context.LeaveRequestComments.Add(lrc);
        await _context.SaveChangesAsync(cancellationToken);
        return lrc;
    }
}