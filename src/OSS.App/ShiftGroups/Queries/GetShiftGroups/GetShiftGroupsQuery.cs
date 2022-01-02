using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.ShiftGroups.Queries.GetShiftGroups;

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
            List<ShiftGroup> res = await _context.ShiftGroups.ToListAsync(cancellationToken: cancellationToken);
            return res;
        }
    }
}
