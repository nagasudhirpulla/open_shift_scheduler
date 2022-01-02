using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.Shifts.Queries.GetShiftBetweenDates;

public class GetShiftsBetweenDatesQuery : IRequest<List<Shift>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public class GetShiftsBetweenDatesQueryHandler : IRequestHandler<GetShiftsBetweenDatesQuery, List<Shift>>
    {
        private readonly AppIdentityDbContext _context;

        public GetShiftsBetweenDatesQueryHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shift>> Handle(GetShiftsBetweenDatesQuery request, CancellationToken cancellationToken)
        {
            List<Shift> res = await _context.Shifts.Include(s => s.ShiftParticipations).ToListAsync(cancellationToken: cancellationToken);
            return await _context.Shifts.Where(s => s.ShiftDate >= request.StartDate && s.ShiftDate <= request.EndDate).Include(s => s.ShiftType).Include(s => s.ShiftParticipations).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
