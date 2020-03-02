using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Shifts.Commands.DeleteShift
{
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
                Shift shift = await _context.Shifts.FindAsync(request.Id);
                if (shift == null)
                {
                    return null;
                }

                _context.Shifts.Remove(shift);
                await _context.SaveChangesAsync();

                return shift;
            }
        }
    }
}
