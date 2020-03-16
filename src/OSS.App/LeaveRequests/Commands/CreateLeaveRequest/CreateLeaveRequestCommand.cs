using MediatR;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.App.LeaveRequests.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommand : IRequest<LeaveRequest>
    {
        public LeaveRequest LeaveRequest { get; set; }
        public string UserId { get; set; }
        public bool IsUserAdmin { get; set; }
    }
}
