using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftGroups.Commands.SeedShiftGroups;

public class SeedShiftGroupsCommand : IRequest<bool>
{
    public class SeedShiftGroupsCommandHandler : IRequestHandler<SeedShiftGroupsCommand, bool>
    {
        private readonly AppIdentityDbContext _context;

        public SeedShiftGroupsCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SeedShiftGroupsCommand request, CancellationToken cancellationToken)
        {
            List<string> seedShiftGroups = new() { "General" };
            foreach (var shiftGrp in seedShiftGroups)
            {
                bool isShiftGrpPres = await _context.ShiftGroups.AnyAsync(g => g.Name.ToLower().Equals(shiftGrp.ToLower()));
                if (!isShiftGrpPres)
                {
                    _context.ShiftGroups.Add(new ShiftGroup() { Name = shiftGrp });
                    await _context.SaveChangesAsync();
                }
            }
            return true;
        }
    }
}
