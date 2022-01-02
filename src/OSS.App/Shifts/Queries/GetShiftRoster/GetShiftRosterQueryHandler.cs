using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.Shifts.Queries.GetShiftRoster;

public class GetShiftRosterQueryHandler : IRequestHandler<GetShiftRosterQuery, ShiftRosterDTO>
{
    private readonly AppIdentityDbContext _context;

    public GetShiftRosterQueryHandler(AppIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<ShiftRosterDTO> Handle(GetShiftRosterQuery request, CancellationToken cancellationToken)
    {
        // TODO refine code
        ShiftRosterDTO vm = new ShiftRosterDTO();

        if (request.StartDate > request.EndDate)
        {
            return vm;
        }

        //fetch the shift types
        List<ShiftType> shiftTypes = await _context.ShiftTypes.OrderBy(st => st.ShiftSequence).ToListAsync(cancellationToken: cancellationToken);
        List<int> shiftTypeIds = shiftTypes.Select(st => st.Id).ToList();

        //fetch the shift Participation types
        List<ShiftParticipationType> shiftPartTypes = await _context.ShiftParticipationTypes.ToListAsync(cancellationToken: cancellationToken);
        List<int> shiftPartTypeIds = shiftPartTypes.Select(spt => spt.Id).ToList();

        // set the view model shift Types
        vm.ShiftTypes = new List<string>();
        foreach (ShiftType shiftType in shiftTypes)
        {
            vm.ShiftTypes.Add(shiftType.Name);
        }

        // initiaize the shift participations in view model
        vm.ShiftParticipations = new Dictionary<DateTime, List<List<Tuple<string, ShiftParticipationType>>>>();
        for (DateTime dt = request.StartDate; dt <= request.EndDate; dt = dt.AddDays(1))
        {
            vm.ShiftParticipations.Add(dt, new List<List<Tuple<string, ShiftParticipationType>>>());
            foreach (var sType in shiftTypes)
            {
                vm.ShiftParticipations[dt].Add(new List<Tuple<string, ShiftParticipationType>>());
            }
        }

        // get the shift participations from db
        List<ShiftParticipation> shiftParticipations = await _context.ShiftParticipations.Include(sp => sp.Shift).Include(sp => sp.Employee).Where(sp => sp.Shift.ShiftDate >= request.StartDate && sp.Shift.ShiftDate <= request.EndDate).ToListAsync(cancellationToken: cancellationToken);

        // assign the fetched shift participations to the vm
        foreach (ShiftParticipation shiftPart in shiftParticipations.OrderBy(sp => sp.ParticipationSequence))
        {
            DateTime shiftDate = shiftPart.Shift.ShiftDate;
            ShiftParticipationType participationType = null;
            int shiftTypeIndex = -1;
            int shiftPartTypeIndex = -1;
            try
            {
                shiftTypeIndex = shiftTypeIds.FindIndex(sTId => sTId == shiftPart.Shift.ShiftTypeId);
                shiftPartTypeIndex = shiftPartTypeIds.FindIndex(sPTId => sPTId == shiftPart.ShiftParticipationTypeId);
            }
            catch (Exception)
            {
                continue;
            }
            if (shiftPartTypeIndex != -1)
            {
                participationType = shiftPartTypes[shiftPartTypeIndex];
            }
            vm.ShiftParticipations[shiftPart.Shift.ShiftDate.Date][shiftTypeIndex].Add(new Tuple<string, ShiftParticipationType>(shiftPart.Employee.DisplayName, participationType));
        }

        // get all the shift objects for comments
        List<Shift> shifts = await _context.Shifts.Where(s => s.ShiftDate >= request.StartDate && s.ShiftDate <= request.EndDate).OrderBy(s => s.ShiftDate).ToListAsync(cancellationToken: cancellationToken);

        // assign the fetched shift comments to the vm
        vm.ShiftComments = new List<Tuple<DateTime, string, string>>();
        foreach (Shift shift in shifts)
        {
            if (shift.Comments != null && shift.Comments != "")
            {
                DateTime shiftDate = shift.ShiftDate;
                int shiftTypeIndex = -1;
                try
                {
                    shiftTypeIndex = shiftTypeIds.FindIndex(sTId => sTId == shift.ShiftTypeId);
                }
                catch (Exception)
                {
                    continue;
                }
                vm.ShiftComments.Add(new Tuple<DateTime, string, string>(shift.ShiftDate, shiftTypes[shiftTypeIndex].Name, shift.Comments));
            }
        }

        return vm;
    }
}
