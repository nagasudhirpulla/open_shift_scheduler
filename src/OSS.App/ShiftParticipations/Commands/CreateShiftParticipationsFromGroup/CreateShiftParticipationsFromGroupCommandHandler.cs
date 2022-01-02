using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.App.Security;
using OSS.Domain.Entities;

namespace OSS.App.ShiftParticipations.Commands.CreateShiftParticipationsFromGroup;

public class CreateShiftParticipationsFromGroupCommandHandler : IRequestHandler<CreateShiftParticipationsFromGroupCommand, List<ShiftParticipation>>
{
    private readonly IdentityInit _identityInit;
    private readonly AppIdentityDbContext _context;

    public CreateShiftParticipationsFromGroupCommandHandler(AppIdentityDbContext context, IdentityInit identityInit)
    {
        _context = context;
        _identityInit = identityInit;
    }

    public async Task<List<ShiftParticipation>> Handle(CreateShiftParticipationsFromGroupCommand request, CancellationToken cancellationToken)
    {
        // Get the shift participation type named normal
        ShiftParticipationType shiftParticipationType = await _context.ShiftParticipationTypes.Where(spt => spt.Name.ToLower() == "normal").FirstOrDefaultAsync(cancellationToken: cancellationToken);
        // get the employees in the shiftGroup
        ShiftGroup shiftGroup = await _context.ShiftGroups.Include(sg => sg.Employees).FirstOrDefaultAsync(sg => sg.Id == request.ShiftGroupId, cancellationToken: cancellationToken);
        List<string> empIds = shiftGroup.Employees.Where(e => e.UserName != _identityInit.AdminUserName).Select(empl => empl.Id).ToList();
        foreach (string empId in empIds)
        {
            try
            {
                // check if participation exists
                if ((await _context.ShiftParticipations.Where(sp => (sp.ShiftId == request.ShiftId) && (sp.EmployeeId == empId)).ToListAsync(cancellationToken: cancellationToken)).Count > 0)
                {
                    continue;
                }
                _context.ShiftParticipations.Add(new ShiftParticipation { EmployeeId = empId, ShiftId = request.ShiftId, ShiftParticipationTypeId = shiftParticipationType.Id });
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error occured while adding participation of employee id {empId} to shift id {request.ShiftId} -- {ex.Message}");
                continue;
            }
        }
        return await _context.ShiftParticipations.Where(sp => sp.ShiftId == request.ShiftId).ToListAsync(cancellationToken: cancellationToken);
    }
}
