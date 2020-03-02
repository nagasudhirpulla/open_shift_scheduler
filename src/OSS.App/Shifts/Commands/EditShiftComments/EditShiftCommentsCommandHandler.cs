using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Shifts.Commands.EditShiftComments
{
    public class EditShiftCommentsCommandHandler : IRequestHandler<EditShiftCommentsCommand, bool>
    {
        private readonly AppIdentityDbContext _context;

        public EditShiftCommentsCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EditShiftCommentsCommand request, CancellationToken cancellationToken)
        {
            // get the required shift by id
            Shift shift = await _context.Shifts.FindAsync(request.ShiftId);
            if (shift == null)
            {
                return false;
            }

            // edit the comments
            shift.Comments = request.Comments;

            // save changes
            _context.Entry(shift).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Shifts.AnyAsync(e => e.Id == request.ShiftId))
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
