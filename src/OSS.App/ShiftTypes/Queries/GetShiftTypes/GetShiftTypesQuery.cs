using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace OSS.App.ShiftTypes.Queries.GetShiftTypes
{
    public class GetShiftTypesQuery : IRequest<List<ShiftType>>
    {
        public class GetShiftTypesQueryHandler : IRequestHandler<GetShiftTypesQuery, List<ShiftType>>
        {
            private readonly AppIdentityDbContext _context;

            public GetShiftTypesQueryHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<List<ShiftType>> Handle(GetShiftTypesQuery request, CancellationToken cancellationToken)
            {
                List<ShiftType> res = (await _context.ShiftTypes.ToListAsync()).OrderBy(s => s.ShiftSequence).ToList();
                return res;
            }
        }
    }
}
