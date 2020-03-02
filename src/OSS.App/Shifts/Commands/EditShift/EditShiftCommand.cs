using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Shifts.Commands.EditShift
{
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
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Shifts.AnyAsync(e => e.Id == request.Shift.Id))
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
}
