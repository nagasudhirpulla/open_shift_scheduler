using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.LeaveRequests.Commands.ToggleLeaveRequestApproval
{
    public class ToggleLeaveRequestApprovalCommandHandler : IRequestHandler<ToggleLeaveRequestApprovalCommand, LeaveRequest>
    {
        private readonly AppIdentityDbContext _context;

        public ToggleLeaveRequestApprovalCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<LeaveRequest> Handle(ToggleLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            LeaveRequest lr = await _context.LeaveRequests.FindAsync(request.Id);

            lr.IsApproved = !lr.IsApproved;
            _context.Attach(lr).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return lr;
        }
    }
}
