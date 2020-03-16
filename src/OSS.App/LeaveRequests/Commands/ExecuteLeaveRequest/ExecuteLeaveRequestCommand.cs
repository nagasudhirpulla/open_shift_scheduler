using MediatR;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.App.LeaveRequests.Commands.ExecuteLeaveRequest
{
    public class ExecuteLeaveRequestCommand : IRequest<LeaveRequest>
    {
        public int Id { get; set; }
    }
}
