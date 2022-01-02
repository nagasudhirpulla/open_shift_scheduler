using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.Shifts.Queries.GetShiftById;

public class GetShiftByIdQuery : IRequest<Shift>
{
    public int Id { get; set; }
    public class GetShiftByIdQueryHandler : IRequestHandler<GetShiftByIdQuery, Shift>
    {
        private readonly AppIdentityDbContext _context;

        public GetShiftByIdQueryHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<Shift> Handle(GetShiftByIdQuery request, CancellationToken cancellationToken)
        {
            var shift = await _context.Shifts.Include(s => s.ShiftType).FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken: cancellationToken);
            return shift;
        }
    }
}
