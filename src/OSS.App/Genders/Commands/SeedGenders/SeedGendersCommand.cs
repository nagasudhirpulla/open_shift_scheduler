using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.Genders.Commands.SeedGenders;
public class SeedGendersCommand : IRequest<bool>
{
    public class SeedGendersCommandHandler : IRequestHandler<SeedGendersCommand, bool>
    {
        private readonly AppIdentityDbContext _context;

        public SeedGendersCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SeedGendersCommand request, CancellationToken cancellationToken)
        {
            List<string> seedGenders = new List<string>() { "Male", "Female" };
            foreach (var gend in seedGenders)
            {
                bool isGendPres = await _context.Genders.AnyAsync(g => g.Name.ToLower().Equals(gend.ToLower()), cancellationToken: cancellationToken);
                if (!isGendPres)
                {
                    _context.Genders.Add(new Gender() { Name = gend });
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return true;
        }
    }
}