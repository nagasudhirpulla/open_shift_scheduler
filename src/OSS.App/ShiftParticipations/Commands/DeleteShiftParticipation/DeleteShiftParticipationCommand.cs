using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.ShiftParticipations.Commands.DeleteShiftParticipation;

public class DeleteShiftParticipationCommand : IRequest<ShiftParticipation>
{
    public int Id { get; set; }
    public class DeleteShiftParticipationCommandHandler : IRequestHandler<DeleteShiftParticipationCommand, ShiftParticipation>
    {
        private readonly AppIdentityDbContext _context;

        public DeleteShiftParticipationCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ShiftParticipation> Handle(DeleteShiftParticipationCommand request, CancellationToken cancellationToken)
        {
            var shiftParticipation = await _context.ShiftParticipations.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (shiftParticipation == null)
            {
                return null;
            }

            _context.ShiftParticipations.Remove(shiftParticipation);
            await _context.SaveChangesAsync(cancellationToken);

            return shiftParticipation;
        }
    }
}
