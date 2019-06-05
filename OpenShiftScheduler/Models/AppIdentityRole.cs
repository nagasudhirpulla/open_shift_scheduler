using Microsoft.AspNetCore.Identity;

namespace OpenShiftScheduler.Models
{
    public class AppIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
