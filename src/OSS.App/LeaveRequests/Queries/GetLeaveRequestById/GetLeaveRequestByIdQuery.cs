using MediatR;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Queries.GetLeaveRequestById;
public class GetLeaveRequestByIdQuery : IRequest<LeaveRequest>
{
    public int Id { get; set; }
}