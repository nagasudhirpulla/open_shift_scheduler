using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.Shifts.Commands.CreateShift;

public class CreateShiftCommand : IRequest<Shift>
{
    public Shift Shift { get; set; }
    public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand, Shift>
    {
        private readonly AppIdentityDbContext _context;

        public CreateShiftCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<Shift> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            Shift s = request.Shift;
            _context.Shifts.Add(s);
            await _context.SaveChangesAsync(cancellationToken);
            return s;
        }
    }
}
