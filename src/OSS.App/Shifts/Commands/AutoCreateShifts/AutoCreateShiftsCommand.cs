using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.App.Shifts.Commands.CreateShift;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Shifts.Commands.AutoCreateShifts
{
    public class AutoCreateShiftsCommand : IRequest<bool>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public class AutoCreateShiftsCommandHandler : IRequestHandler<AutoCreateShiftsCommand, bool>
        {
            private readonly AppIdentityDbContext _context;

            public AutoCreateShiftsCommandHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(AutoCreateShiftsCommand request, CancellationToken cancellationToken)
            {
                DateTime startDate = request.StartDate;
                DateTime endDate = request.EndDate;

                // create shift type Ids in the roaster sequence for automated shift 
                List<int> orderedShiftTypeIds = await _context.ShiftCycleItems.OrderBy(sci => sci.ShiftSequence).Select(sci => sci.ShiftTypeId).ToListAsync();

                // List<ShiftType> shiftTypes = await _context.ShiftTypes.OrderBy(st => st.RoasterSequence).ToListAsync();
                // List<int> orderedShiftTypeIds_old = shiftTypes.Select(st => st.ShiftTypeId).ToList();

                //Func<int, int, Tuple<int, int>> GetNextShiftTypeInfo = (currentShiftTypeId, numTimesAlreadyOccured) =>
                //{
                //    if (numTimesAlreadyOccured == 1)
                //    {
                //        return new Tuple<int, int>(currentShiftTypeId, 2);
                //    }
                //    int nextShiftTypeId = -1;
                //    int updatedAlreadyOccured = -1;
                //    if (orderedShiftTypeIds.Any(sti => sti == currentShiftTypeId))
                //    {
                //        int currentShiftTypeIdIndex = orderedShiftTypeIds.FindIndex(sti => sti == currentShiftTypeId);
                //        int nextShiftTypeIdIndex = currentShiftTypeIdIndex + 1;
                //        if (nextShiftTypeIdIndex >= orderedShiftTypeIds.Count)
                //        {
                //            nextShiftTypeIdIndex = 0;
                //        }
                //        nextShiftTypeId = orderedShiftTypeIds[nextShiftTypeIdIndex];
                //        updatedAlreadyOccured = 1;
                //    }
                //    return new Tuple<int, int>(nextShiftTypeId, updatedAlreadyOccured);
                //};

                // using local function
                int GetNextShiftTypeIndex(int currentShiftTypeIndex)
                {
                    int nextShiftTypeIndex = -1;
                    if (currentShiftTypeIndex < 0)
                    {
                        return -1;
                    }
                    if (currentShiftTypeIndex == orderedShiftTypeIds.Count - 1)
                    {
                        nextShiftTypeIndex = 0;
                    }
                    else
                    {
                        nextShiftTypeIndex = currentShiftTypeIndex + 1;
                    }
                    return nextShiftTypeIndex;
                }

                // get the shifts on the start Date
                List<Shift> startDayShifts = await _context.Shifts.Include(s => s.ShiftType).Include(s => s.ShiftParticipations).Where(s => s.ShiftDate == startDate).ToListAsync();

                // iterate through each shift in the start date
                foreach (Shift startDayShift in startDayShifts)
                {
                    List<ShiftParticipation> startDayShiftParticipations = startDayShift.ShiftParticipations.ToList();
                    int currentShiftTypeId = startDayShift.ShiftTypeId;
                    int currentShiftTypeIndex = orderedShiftTypeIds.IndexOf(currentShiftTypeId);

                    // iterate through each shift date for automation
                    for (DateTime shiftDate = startDate.AddDays(1); (shiftDate.Date <= endDate.Date) && (currentShiftTypeIndex != -1); shiftDate = shiftDate.AddDays(1))
                    {
                        // get the next ShiftType in the shift cycle sequence creating the new shift
                        currentShiftTypeIndex = GetNextShiftTypeIndex(currentShiftTypeIndex);
                        currentShiftTypeId = orderedShiftTypeIds[currentShiftTypeIndex];

                        // delete all the shifts of the particular shift type on that day if present
                        List<Shift> existingShifts = await _context.Shifts.Where(s => s.ShiftDate == shiftDate && s.ShiftTypeId == currentShiftTypeId).ToListAsync();
                        foreach (Shift existingShift in existingShifts)
                        {
                            _context.Shifts.Remove(existingShift);
                        }

                        // create new shift with the shift participations
                        Shift newShift = new Shift { ShiftTypeId = currentShiftTypeId, ShiftDate = shiftDate };
                        _context.Add(newShift);
                        await _context.SaveChangesAsync();

                        // create the shift participations for the new shift
                        foreach (ShiftParticipation shiftPart in startDayShift.ShiftParticipations)
                        {
                            ShiftParticipation newShiftPart = new ShiftParticipation { ShiftId = newShift.Id, EmployeeId = shiftPart.EmployeeId, ParticipationSequence = shiftPart.ParticipationSequence, ShiftParticipationTypeId = shiftPart.ShiftParticipationTypeId };
                            _context.ShiftParticipations.Add(newShiftPart);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return true;
            }
        }
    }
}
