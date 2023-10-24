using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.Security;

namespace OSS.Web.Pages.Shifts;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class EditBlazorModel : PageModel
{
    public void OnGet()
    {

    }
}
