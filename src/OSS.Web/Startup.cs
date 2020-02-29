using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OSS.Infra;
using OSS.App;
using FluentValidation.AspNetCore;
using OSS.App.Data;
using MediatR;
using OSS.App.Security.Commands.SeedUsers;
using System.Threading.Tasks;
using OSS.App.Genders.Commands.SeedGenders;
using OSS.App.ShiftRoles.Commands.SeedShiftRoles;
using OSS.App.ShiftGroups.Commands.SeedShiftGroups;

namespace OSS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration, Environment);
            services.AddApplication();
            services
                .AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppIdentityDbContext>())
                .AddRazorRuntimeCompilation();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMediator mediator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            SeedData(mediator).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        public async Task SeedData(IMediator mediator)
        {
            bool gendersSeeded = await mediator.Send(new SeedGendersCommand());
            bool shiftRolesSeeded = await mediator.Send(new SeedShiftRolesCommand());
            bool shiftGroupsSeeded = await mediator.Send(new SeedShiftGroupsCommand());
            bool usersSeeded = await mediator.Send(new SeedUsersCommand());
        }
    }
}
