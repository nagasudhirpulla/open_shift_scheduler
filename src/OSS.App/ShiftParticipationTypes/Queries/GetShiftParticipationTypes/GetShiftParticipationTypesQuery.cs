using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftParticipationTypes.Queries.GetShiftParticipationTypes
{
    public class GetShiftParticipationTypesQuery : IRequest<List<ShiftParticipationType>>
    {
        public class GetShiftParticipationTypesQueryHandler : IRequestHandler<GetShiftParticipationTypesQuery, List<ShiftParticipationType>>
        {
            private readonly AppIdentityDbContext _context;

            public GetShiftParticipationTypesQueryHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<List<ShiftParticipationType>> Handle(GetShiftParticipationTypesQuery request, CancellationToken cancellationToken)
            {
                List<ShiftParticipationType> res = await _context.ShiftParticipationTypes.ToListAsync();
                return res;
            }

        }
    }
}
