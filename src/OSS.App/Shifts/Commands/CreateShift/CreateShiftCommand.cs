using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Shifts.Commands.CreateShift
{
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
                await _context.SaveChangesAsync();
                return s;
            }
        }
    }
}
