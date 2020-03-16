using MediatR;
using OSS.Domain.Entities;
using System;
using System.Text;

namespace OSS.App.LeaveRequests.Commands.DeleteLeaveRequest
{
    public class DeleteLeaveRequestCommand : IRequest<LeaveRequest>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsUserAdmin { get; set; }
    }
}
