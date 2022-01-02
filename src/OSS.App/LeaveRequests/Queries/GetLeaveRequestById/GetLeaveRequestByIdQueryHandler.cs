using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequests.Queries.GetLeaveRequestById;
public class GetLeaveRequestByIdQueryHandler : IRequestHandler<GetLeaveRequestByIdQuery, LeaveRequest>
{
    private readonly AppIdentityDbContext _context;

    public GetLeaveRequestByIdQueryHandler(AppIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveRequest> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
    {
        LeaveRequest res = await _context.LeaveRequests
                                        .Where(r => r.Id == request.Id)
                                        .Include(r => r.Employee)
                                        .Include(r => r.LeaveType)
                                        .Include(r => r.LeaveRequestComments)
                                        .ThenInclude(c => c.CreatedBy)
                                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return res;
    }
}