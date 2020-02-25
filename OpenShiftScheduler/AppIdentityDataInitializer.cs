using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OpenShiftScheduler.Models;

namespace OpenShiftScheduler
{
    public static class AppIdentityDataInitializer
    {
        public static readonly string GuestUserRoleString = "GuestUser";
        public static readonly string AdministratorRoleString = "Administrator";

        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<AppIdentityRole> roleManager, IConfiguration Configuration)
        {
            UserInitVariables initVariables = new UserInitVariables();
            initVariables.InitializeFromConfig(Configuration);
            SeedUserRoles(roleManager);
            SeedGuestAdminUsers(userManager, initVariables);
        }

        public static void SeedGuestAdminUsers(UserManager<ApplicationUser> userManager, UserInitVariables initVariables)
        {
            string GuestUserName = initVariables.GuestUserName;
            string GuestEmail = initVariables.GuestEmail;
            string GuestPassword = initVariables.GuestPassword;
            string AdminUserName = initVariables.AdminUserName;
            string AdminEmail = initVariables.AdminEmail;
            string AdminPassword = initVariables.AdminPassword;
            if (userManager.FindByNameAsync(GuestUserName).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = GuestUserName,
                    Email = GuestEmail
                };

                IdentityResult result = userManager.CreateAsync(user, GuestPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, GuestUserRoleString).Wait();
                }
            }


            if (userManager.FindByNameAsync(AdminUserName).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = AdminUserName,
                    Email = AdminEmail
                };

                IdentityResult result = userManager.CreateAsync(user, AdminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, AdministratorRoleString).Wait();
                }
            }
        }

        public static void SeedUserRoles(RoleManager<AppIdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(GuestUserRoleString).Result)
            {
                AppIdentityRole role = new AppIdentityRole
                {
                    Name = GuestUserRoleString,
                    Description = "Can only do viewing of limited data"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync(AdministratorRoleString).Result)
            {
                AppIdentityRole role = new AppIdentityRole
                {
                    Name = AdministratorRoleString,
                    Description = "Can Perform all the operations."
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
