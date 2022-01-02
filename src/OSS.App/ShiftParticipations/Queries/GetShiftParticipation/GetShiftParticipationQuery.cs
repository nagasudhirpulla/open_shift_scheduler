using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.ShiftParticipations.Queries.GetShiftParticipation;

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
            var shiftParticipation = await _context.ShiftParticipations.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            return shiftParticipation;
        }
    }
}
