using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.App.Security.Commands.SeedUsers;

public class SeedUsersCommand : IRequest<bool>
{
    public class SeedUsersCommandHandler : IRequestHandler<SeedUsersCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityInit _identityInit;
        private readonly AppIdentityDbContext _context;

        public SeedUsersCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IdentityInit identityInit, AppIdentityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityInit = identityInit;
            _context = context;
        }

        public async Task<bool> Handle(SeedUsersCommand request, CancellationToken cancellationToken)
        {
            // seed roles
            await SeedUserRoles(_roleManager);
            // seed admin user
            await SeedAdminUser(_userManager);
            return true;
        }

        /**
         * This method seeds admin user
         * **/
        public async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            string AdminUserName = _identityInit.AdminUserName;
            string AdminEmail = _identityInit.AdminEmail;
            string AdminPassword = _identityInit.AdminPassword;

            // check if admin user doesn't exist
            if ((await userManager.FindByNameAsync(AdminUserName)) == null)
            {
                // get gender by name "Male"
                Gender gender = await _context.Genders.Where(b => b.Name.ToLower() == "male")
                                                      .FirstOrDefaultAsync();
                ShiftGroup shiftGrp = await _context.ShiftGroups.Where(b => b.Name.ToLower() == "general")
                                                      .FirstOrDefaultAsync();
                // create desired admin user object
                ApplicationUser user = new ApplicationUser
                {
                    UserName = AdminUserName,
                    DisplayName = AdminUserName,
                    Email = AdminEmail,
                    Gender = gender,
                    ShiftGroup = shiftGrp
                };

                // push desired admin user object to DB
                IdentityResult result = await userManager.CreateAsync(user, AdminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SecurityConstants.AdminRoleString);
                }
            }
        }

        /**
         * This method seeds roles
         * **/
        public async Task SeedUserRoles(RoleManager<IdentityRole> roleManager)
        {
            List<string> desiredRoles = new List<string>() { SecurityConstants.GuestRoleString, SecurityConstants.AdminRoleString };
            foreach (string roleName in desiredRoles)
            {
                // check if role doesn't exist
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    // create desired role object
                    IdentityRole role = new IdentityRole
                    {
                        Name = roleName,
                    };
                    // push desired role object to DB
                    IdentityResult roleResult = await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
