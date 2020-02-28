using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(OSS.Web.Areas.Identity.IdentityHostingStartup))]
namespace OSS.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}