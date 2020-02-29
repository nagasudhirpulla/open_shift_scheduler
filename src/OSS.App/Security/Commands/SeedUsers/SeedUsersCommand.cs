using MediatR;
using Microsoft.AspNetCore.Identity;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Security.Commands.SeedUsers
{
    public class SeedUsersCommand : IRequest<bool>
    {
        public class SeedUsersCommandHandler : IRequestHandler<SeedUsersCommand, bool>
        {
            private readonly UserManager<ApplicationUser> UserManager;
            private readonly RoleManager<IdentityRole> RoleManager;
            private readonly IdentityInit IdentityInit;

            public SeedUsersCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IdentityInit identityInit)
            {
                UserManager = userManager;
                RoleManager = roleManager;
                IdentityInit = identityInit;
            }

            public async Task<bool> Handle(SeedUsersCommand request, CancellationToken cancellationToken)
            {
                // seed roles
                await SeedUserRoles(RoleManager);
                // seed admin user
                await SeedAdminUser(UserManager);
                return true;
            }

            /**
             * This method seeds admin user
             * **/
            public async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
            {
                string AdminUserName = IdentityInit.AdminUserName;
                string AdminEmail = IdentityInit.AdminEmail;
                string AdminPassword = IdentityInit.AdminPassword;

                // check if admin user doesn't exist
                if ((await userManager.FindByNameAsync(AdminUserName)) == null)
                {
                    // create desired admin user object
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = AdminUserName,
                        Email = AdminEmail
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
}
