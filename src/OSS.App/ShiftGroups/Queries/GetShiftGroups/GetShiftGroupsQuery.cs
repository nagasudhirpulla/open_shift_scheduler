using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftGroups.Queries.GetShiftGroups
{
    public class GetShiftGroupsQuery : IRequest<List<ShiftGroup>>
    {
        public class GetShiftGroupsQueryHandler : IRequestHandler<GetShiftGroupsQuery, List<ShiftGroup>>
        {
            private readonly AppIdentityDbContext _context;

            public GetShiftGroupsQueryHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<List<ShiftGroup>> Handle(GetShiftGroupsQuery request, CancellationToken cancellationToken)
            {
                List<ShiftGroup> res = await _context.ShiftGroups.ToListAsync();
                return res;
            }
        }
    }
}
