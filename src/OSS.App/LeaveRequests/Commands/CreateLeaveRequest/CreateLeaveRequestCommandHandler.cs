using MediatR;
using OSS.App.Data;
using OSS.App.Security;
using OSS.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.LeaveRequests.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, LeaveRequest>
    {
        private readonly AppIdentityDbContext _context;

        public CreateLeaveRequestCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<LeaveRequest> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            LeaveRequest lr = request.LeaveRequest;
            if (!request.IsUserAdmin && request.UserId != request.LeaveRequest.EmployeeId)
            {
                throw new Exception("A non-admin user is trying to create a request on others behalf...");
            }
            _context.LeaveRequests.Add(lr);
            await _context.SaveChangesAsync();
            return lr;
        }
    }
}
