using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using OSS.Domain.Entities;
using OSS.Infra.Email;
using OSS.Infra.Identity.TokenProviders;
using OSS.App.Security;
using OSS.App.Data;
using DNTCaptcha.Core;

namespace OSS.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        if (environment.IsEnvironment("Testing"))
        {
            // Add Identity Infra
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "OpenShiftScheduler"));
        }
        else
        {
            // Add Identity Persistence Infra 
            services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseNpgsql(
                      configuration.GetConnectionString("DefaultConnection"),
                      b => b.MigrationsAssembly("OSS.Web"))
                );
        }

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
            options.Password.RequiredUniqueChars = 2;
            options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
        })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation");

        // set email confirmation token lifespan to 3 days
        services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromDays(3));

        services.ConfigureApplicationCookie(options =>
        {
                // configure login path for return urls
                // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
                options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });

        // Add Email service
        services.AddTransient<IEmailSender, EmailSender>();

        // add super admin user details from config as a singleton service
        IdentityInit identityInit = new IdentityInit();
        configuration.Bind("IdentityInit", identityInit);
        services.AddSingleton(identityInit);

        // add email settings from config as a singleton service
        EmailConfiguration emailConfig = new EmailConfiguration();
        configuration.Bind("EmailSettings", emailConfig);
        services.AddSingleton(emailConfig);

        services.AddDNTCaptcha(options =>
        {
            // options.UseSessionStorageProvider(); // -> It doesn't rely on the server or client's times. Also it's the safest one.
            // options.UseMemoryCacheStorageProvider(); // -> It relies on the server's times. It's safer than the CookieStorageProvider.
            options.UseCookieStorageProvider() // -> It relies on the server and client's times. It's ideal for scalability, because it doesn't save anything in the server's memory.
            .ShowThousandsSeparators(false)
            .WithEncryptionKey(Guid.NewGuid().ToString());
            // options.UseDistributedCacheStorageProvider(); // --> It's ideal for scalability using `services.AddStackExchangeRedisCache()` for instance.
            // options.UseDistributedSerializationProvider();
        });

        return services;
    }
}
