using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftParticipationTypes.Commands.SeedShiftParticipationTypes
{
    public class SeedShiftParticipationTypesCommand : IRequest<bool>
    {
        public class SeedShiftParticipationTypesCommandHandler : IRequestHandler<SeedShiftParticipationTypesCommand, bool>
        {
            private readonly AppIdentityDbContext _context;

            public SeedShiftParticipationTypesCommandHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(SeedShiftParticipationTypesCommand request, CancellationToken cancellationToken)
            {
                List<string> seedPartTypes = new List<string>() { "Normal", "Leave" };
                foreach (var partType in seedPartTypes)
                {
                    bool isPartTypePres = await _context.ShiftParticipationTypes.AnyAsync(spt => spt.Name.ToLower().Equals(partType.ToLower()));
                    if (!isPartTypePres)
                    {
                        _context.ShiftParticipationTypes.Add(new ShiftParticipationType() { Name = partType });
                        await _context.SaveChangesAsync();
                    }
                }
                return true;
            }
        }
    }
}
