using MediatR;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Commands.ExecuteLeaveRequest;
public class ExecuteLeaveRequestCommand : IRequest<LeaveRequest>
{
    public int Id { get; set; }
}