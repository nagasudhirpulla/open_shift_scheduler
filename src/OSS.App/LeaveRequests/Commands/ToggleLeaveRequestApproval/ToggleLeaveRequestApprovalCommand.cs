using MediatR;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSS.App.LeaveRequests.Commands.ToggleLeaveRequestApproval
{
    public class ToggleLeaveRequestApprovalCommand : IRequest<LeaveRequest>
    {
        public int Id { get; set; }
    }
}
