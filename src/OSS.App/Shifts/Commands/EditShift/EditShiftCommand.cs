using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.Shifts.Commands.EditShift;

public class EditShiftCommand : IRequest<bool>
{
    public Shift Shift { get; set; }
    public class EditShiftCommandHandler : IRequestHandler<EditShiftCommand, bool>
    {
        private readonly AppIdentityDbContext _context;

        public EditShiftCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EditShiftCommand request, CancellationToken cancellationToken)
        {
            _context.Entry(request.Shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Shifts.AnyAsync(e => e.Id == request.Shift.Id, cancellationToken: cancellationToken))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
    }
}
