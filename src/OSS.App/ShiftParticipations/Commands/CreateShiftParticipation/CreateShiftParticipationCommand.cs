using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftParticipations.Commands.CreateShiftParticipation
{
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
                await _context.SaveChangesAsync();
                return sp;
            }
        }
    }
}
