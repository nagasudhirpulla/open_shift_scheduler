using MediatR;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Queries.GetLeaveRequestsByEmpId;
public class GetLeaveRequestsByEmpIdQuery : IRequest<List<LeaveRequest>>
{
    public string EmployeeId { get; set; }
}