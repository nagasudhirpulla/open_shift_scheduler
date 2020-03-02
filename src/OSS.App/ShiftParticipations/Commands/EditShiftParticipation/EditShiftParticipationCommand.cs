using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftParticipations.Commands.EditShiftParticipation
{
    public class EditShiftParticipationCommand : IRequest<bool>
    {
        public ShiftParticipation ShiftParticipation { get; set; }
        public class EditShiftParticipationCommandHandler : IRequestHandler<EditShiftParticipationCommand, bool>
        {
            private readonly AppIdentityDbContext _context;

            public EditShiftParticipationCommandHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(EditShiftParticipationCommand request, CancellationToken cancellationToken)
            {
                _context.Entry(request.ShiftParticipation).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.ShiftParticipations.AnyAsync(e => e.Id == request.ShiftParticipation.Id))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }
        }
    }
}
