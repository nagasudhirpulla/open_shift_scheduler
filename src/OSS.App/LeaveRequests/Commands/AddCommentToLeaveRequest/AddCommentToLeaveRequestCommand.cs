using MediatR;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Commands.AddCommentToLeaveRequest;
public class AddCommentToLeaveRequestCommand : IRequest<LeaveRequestComment>
{
    public LeaveRequestComment LeaveRequestComment { get; set; }
}