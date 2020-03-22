using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.App.Security;
using OSS.App.Security.Queries.GetAppUsers;
using OSS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Shifts.Queries.GetAllEmployeeStats
{
    public class GetAllEmployeeStatsQueryHandler : IRequestHandler<GetAllEmployeeStatsQuery, List<EmployeeStatsDTO>>
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetAllEmployeeStatsQueryHandler(UserManager<ApplicationUser> userManager, AppIdentityDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeStatsDTO>> Handle(GetAllEmployeeStatsQuery request, CancellationToken cancellationToken)
        {
            List<EmployeeStatsDTO> vm = new List<EmployeeStatsDTO>();

            // get all the shift participations
            List<ShiftParticipation> shiftParts = await _context.ShiftParticipations.Where(sp => sp.Shift.ShiftDate >= request.StartDate.Date && sp.Shift.ShiftDate <= request.EndDate.Date)
                                                      .Include(sp => sp.ShiftParticipationType)
                                                      .Include(sp => sp.Shift)
                                                      .ThenInclude(sp => sp.ShiftType)
                                                      .ToListAsync();

            List<ShiftType> shiftTypes = await _context.ShiftTypes.ToListAsync();
            List<ShiftParticipationType> shiftPartTypes = await _context.ShiftParticipationTypes.ToListAsync();
            List<ApplicationUser> employees = await _userManager.Users
                                                        .Include(e => e.ShiftGroup)
                                                        .Include(e => e.ShiftRole)
                                                        .Include(e => e.Gender)
                                                        .ToListAsync();

            // compute stats for each employee
            foreach (ApplicationUser emp in employees)
            {
                string userRole = "";
                IList<string> existingRoles = await _userManager.GetRolesAsync(emp);
                if (existingRoles.Count > 0)
                {
                    userRole = existingRoles.ElementAt(0);
                }
                if (userRole != SecurityConstants.AdminRoleString)
                {
                    UserDTO uDTO = _mapper.Map<UserDTO>(emp);
                    uDTO.UserRole = userRole;
                    EmployeeStatsDTO stats = new EmployeeStatsDTO
                    {
                        Employee = uDTO
                    };

                    // count as per shift type
                    foreach (ShiftType sType in shiftTypes)
                    {
                        int numEmpShiftsOfThisType = shiftParts.Count(sp => sp.EmployeeId == emp.Id && sp.Shift.ShiftTypeId == sType.Id && !sp.ShiftParticipationType.IsAbsence);
                        stats.NumShiftsPerType.Add(sType.Name, numEmpShiftsOfThisType);
                    }

                    // count as per participation type
                    foreach (var sPartType in shiftPartTypes)
                    {
                        int numEmpShiftsOfThisType = shiftParts.Count(sp => sp.EmployeeId == emp.Id && sp.ShiftParticipationTypeId == sPartType.Id);
                        stats.NumShiftsPerType.Add(sPartType.Name, numEmpShiftsOfThisType);
                        if (sPartType.IsAbsence)
                        {
                            stats.numAbsenceShifts += numEmpShiftsOfThisType;
                        }
                        else
                        {
                            stats.numPresenceShifts += numEmpShiftsOfThisType;
                        }
                    }
                    vm.Add(stats);
                }
            }
            return vm;
        }
    }
}
