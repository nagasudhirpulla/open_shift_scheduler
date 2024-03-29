﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.ShiftRoles.Commands.SeedShiftRoles;

public class SeedShiftRolesCommand : IRequest<bool>
{
    public class SeedShiftRolesCommandHandler : IRequestHandler<SeedShiftRolesCommand, bool>
    {
        private readonly AppIdentityDbContext _context;

        public SeedShiftRolesCommandHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SeedShiftRolesCommand request, CancellationToken cancellationToken)
        {
            List<string> seedShiftRoles = new List<string>() { "Shift-Incharge", "Outage-Coordination", "Scheduling-Coordination" };
            foreach (var shiftRole in seedShiftRoles)
            {
                bool IsShiftRolePres = await _context.ShiftRoles.AnyAsync(g => g.RoleName.Equals(shiftRole), cancellationToken: cancellationToken);
                if (!IsShiftRolePres)
                {
                    _context.ShiftRoles.Add(new ShiftRole() { RoleName = shiftRole });
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return true;
        }
    }
}
