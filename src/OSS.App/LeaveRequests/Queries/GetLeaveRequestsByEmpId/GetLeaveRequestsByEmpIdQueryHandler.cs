﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.LeaveRequests.Queries.GetLeaveRequestsByEmpId
{
    public class GetLeaveRequestsByEmpIdQueryHandler : IRequestHandler<GetLeaveRequestsByEmpIdQuery, List<LeaveRequest>>
    {
        private readonly AppIdentityDbContext _context;

        public GetLeaveRequestsByEmpIdQueryHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveRequest>> Handle(GetLeaveRequestsByEmpIdQuery request, CancellationToken cancellationToken)
        {
            List<LeaveRequest> res = await _context.LeaveRequests
                                            .Where(r => r.CreatedBy.Id == request.EmployeeId)
                                            .Include(r => r.LeaveRequestComments)
                                            .OrderByDescending(r => r.Created)
                                            .ToListAsync();
            return res;
        }
    }
}
