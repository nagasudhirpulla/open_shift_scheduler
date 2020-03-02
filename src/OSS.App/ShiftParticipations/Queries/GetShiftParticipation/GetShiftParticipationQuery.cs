using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftParticipations.Queries.GetShiftParticipation
{
    public class GetShiftParticipationQuery : IRequest<ShiftParticipation>
    {
        public int Id { get; set; }
        public class GetShiftRolesQueryHandler : IRequestHandler<GetShiftParticipationQuery, ShiftParticipation>
        {
            private readonly AppIdentityDbContext _context;

            public GetShiftRolesQueryHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<ShiftParticipation> Handle(GetShiftParticipationQuery request, CancellationToken cancellationToken)
            {
                var shiftParticipation = await _context.ShiftParticipations.FindAsync(request.Id);
                return shiftParticipation;
            }
        }
    }
}
