using MediatR;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Commands.ToggleLeaveRequestApproval;
public class ToggleLeaveRequestApprovalCommand : IRequest<LeaveRequest>
{
    public int Id { get; set; }
}