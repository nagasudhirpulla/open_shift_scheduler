using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.ShiftParticipations.Commands.CreateShiftParticipationsFromGroup
{
    public class CreateShiftParticipationsFromGroupCommandHandler : IRequestHandler<CreateShiftParticipationsFromGroupCommand, List<ShiftParticipation>>
    {
        private readonly AppIdentityDbContext _context;

        public CreateShiftParticipationsFromGroupCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShiftParticipation>> Handle(CreateShiftParticipationsFromGroupCommand request, CancellationToken cancellationToken)
        {
            // get the employees in the shiftGroup
            ShiftGroup shiftGroup = await _context.ShiftGroups.Include(sg => sg.Employees).FirstOrDefaultAsync(sg => sg.Id == request.ShiftGroupId);
            List<string> empIds = shiftGroup.Employees.Select(empl => empl.Id).ToList();
            foreach (string empId in empIds)
            {
                try
                {
                    // check if participation exists
                    if ((await _context.ShiftParticipations.Where(sp => (sp.ShiftId == request.ShiftId) && (sp.EmployeeId == empId)).ToListAsync()).Count > 0)
                    {
                        continue;
                    }
                    _context.ShiftParticipations.Add(new ShiftParticipation { EmployeeId = empId, ShiftId = request.ShiftId });
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error occured while adding participation of employee id {empId} to shift id {request.ShiftId} -- {ex.Message}");
                    continue;
                }
            }
            return await _context.ShiftParticipations.Where(sp => sp.ShiftId == request.ShiftId).ToListAsync();
        }
    }
}
