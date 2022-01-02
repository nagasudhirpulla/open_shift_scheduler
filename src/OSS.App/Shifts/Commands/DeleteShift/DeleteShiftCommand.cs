using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.Shifts.Commands.DeleteShift;

public class DeleteShiftCommand : IRequest<Shift>
{
    public int Id { get; set; }
    public class DeleteShiftCommandHandler : IRequestHandler<DeleteShiftCommand, Shift>
    {
        private readonly AppIdentityDbContext _context;

        public DeleteShiftCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<Shift> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
        {
            Shift shift = await _context.Shifts.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (shift == null)
            {
                return null;
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync(cancellationToken);

            return shift;
        }
    }
}
