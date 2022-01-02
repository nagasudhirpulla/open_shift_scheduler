using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.ShiftParticipations.Queries.GetShiftParticipationsForShift;

public class GetShiftParticipationsForShiftQuery : IRequest<List<ShiftParticipation>>
{
    public int ShiftId { get; set; }
    public class GetShiftRolesQueryHandler : IRequestHandler<GetShiftParticipationsForShiftQuery, List<ShiftParticipation>>
    {
        private readonly AppIdentityDbContext _context;

        public GetShiftRolesQueryHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShiftParticipation>> Handle(GetShiftParticipationsForShiftQuery request, CancellationToken cancellationToken)
        {
            List<ShiftParticipation> res = await _context.ShiftParticipations.Where(sp => sp.ShiftId == request.ShiftId).ToListAsync(cancellationToken: cancellationToken);
            return res;
        }
    }
}
