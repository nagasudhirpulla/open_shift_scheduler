using MediatR;
using OSS.Domain.Entities;
using System.Collections.Generic;

namespace OSS.App.LeaveRequests.Queries.GetLeaveRequestsByEmpId
{
    public class GetLeaveRequestsByEmpIdQuery : IRequest<List<LeaveRequest>>
    {
        public string EmployeeId { get; set; }
    }
}
