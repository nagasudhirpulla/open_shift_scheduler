using MediatR;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.App.LeaveRequests.Commands.AddCommentToLeaveRequest
{
    public class AddCommentToLeaveRequestCommand : IRequest<LeaveRequestComment>
    {
        public LeaveRequestComment LeaveRequestComment { get; set; }
    }
}
