using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.App.Security;
using OSS.App.Security.Queries.GetAppUsers;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Shifts.Queries.GetAllEmployeeNightStats
{
    public class GetAllEmployeeNightStatsQueryHandler : IRequestHandler<GetAllEmployeeNightStatsQuery, List<EmployeeNightStatsDTO>>
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetAllEmployeeNightStatsQueryHandler(UserManager<ApplicationUser> userManager, AppIdentityDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeNightStatsDTO>> Handle(GetAllEmployeeNightStatsQuery request, CancellationToken cancellationToken)
        {
            List<EmployeeNightStatsDTO> vm = new List<EmployeeNightStatsDTO>();

            // get all the shift participations
            List<ShiftParticipation> shiftParts = await _context.ShiftParticipations.Where(sp => sp.Shift.ShiftDate >= request.StartDate.Date && sp.Shift.ShiftDate <= request.EndDate.Date)
                                                      .Include(sp => sp.ShiftParticipationType)
                                                      .Include(sp => sp.Shift)
                                                      .ThenInclude(s => s.ShiftType)
                                                      .ToListAsync();

            ShiftType nShiftType = (await _context.ShiftTypes.Where(s => s.Name.ToLower().Contains("night shift")).ToListAsync()).ElementAt(0);
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
                    EmployeeNightStatsDTO stats = new EmployeeNightStatsDTO
                    {
                        Employee = uDTO
                    };

                    int numEmpShiftsOfThisType = shiftParts.Count(sp => sp.EmployeeId == emp.Id && sp.Shift.ShiftTypeId == nShiftType.Id);
                    stats.NumNightShiftsAllotted = numEmpShiftsOfThisType;

                    List<ShiftParticipation> attendedNightShiftParts = shiftParts.Where(sp => sp.EmployeeId == emp.Id && sp.Shift.ShiftTypeId == nShiftType.Id && !sp.ShiftParticipationType.IsAbsence).ToList();
                    string nightShiftDates = String.Join(", ", attendedNightShiftParts.Select(p => p.Shift.ShiftDate.Day).OrderBy(x => x).ToArray());
                    stats.NightShiftDates = nightShiftDates;
                    stats.NumNightShiftsAttended = attendedNightShiftParts.Count;

                    vm.Add(stats);
                }
            }
            return vm;
        }
    }
}
