using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.LeaveRequests.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsQuery : IRequest<List<LeaveRequest>>
    {
        public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequest>>
        {
            private readonly AppIdentityDbContext _context;

            public GetLeaveRequestsQueryHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<List<LeaveRequest>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
            {
                List<LeaveRequest> res = await _context.LeaveRequests
                                                .Include(r => r.LeaveRequestComments)
                                                .Include(r => r.Employee)
                                                .Include(r => r.LeaveType)
                                                .OrderByDescending(r => r.Created)
                                                .ToListAsync();
                return res;
            }
        }
    }    
}
