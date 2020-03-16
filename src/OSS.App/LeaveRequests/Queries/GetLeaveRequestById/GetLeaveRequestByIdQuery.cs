using MediatR;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.App.LeaveRequests.Queries.GetLeaveRequestById
{
    public class GetLeaveRequestByIdQuery : IRequest<LeaveRequest>
    {
        public int Id { get; set; }
    }
}
