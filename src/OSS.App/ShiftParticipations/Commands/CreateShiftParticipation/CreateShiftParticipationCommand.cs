using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.ShiftParticipations.Commands.CreateShiftParticipation;

public class CreateShiftParticipationCommand : IRequest<ShiftParticipation>
{
    public ShiftParticipation ShiftParticipation { get; set; }
    public class CreateShiftParticipationCommandHandler : IRequestHandler<CreateShiftParticipationCommand, ShiftParticipation>
    {
        private readonly AppIdentityDbContext _context;

        public CreateShiftParticipationCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ShiftParticipation> Handle(CreateShiftParticipationCommand request, CancellationToken cancellationToken)
        {
            ShiftParticipation sp = request.ShiftParticipation;
            _context.ShiftParticipations.Add(sp);
            await _context.SaveChangesAsync(cancellationToken);
            return sp;
        }
    }
}
