using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.Shifts.Queries.GetShifts;

public class GetShiftsQuery : IRequest<List<Shift>>
{
    public class GetShiftsQueryHandler : IRequestHandler<GetShiftsQuery, List<Shift>>
    {
        private readonly AppIdentityDbContext _context;

        public GetShiftsQueryHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shift>> Handle(GetShiftsQuery request, CancellationToken cancellationToken)
        {
            List<Shift> res = await _context.Shifts.Include(s => s.ShiftParticipations).ToListAsync(cancellationToken: cancellationToken);
            return res;
        }
    }
}
