using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftRoles.Queries.GetShiftRoles
{
    public class GetShiftRolesQuery : IRequest<List<ShiftRole>>
    {
        public class GetShiftRolesQueryHandler : IRequestHandler<GetShiftRolesQuery, List<ShiftRole>>
        {
            private readonly AppIdentityDbContext _context;

            public GetShiftRolesQueryHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<List<ShiftRole>> Handle(GetShiftRolesQuery request, CancellationToken cancellationToken)
            {
                List<ShiftRole> res = await _context.ShiftRoles.ToListAsync();
                return res;
            }
        }
    }
}
