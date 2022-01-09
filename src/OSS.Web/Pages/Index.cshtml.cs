using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OSS.App.Security;

namespace OSS.Web.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            if (User.IsInRole(SecurityConstants.AdminRoleString))
            {
                return RedirectToPage("/Shifts/Edit");
            }
            else
            {
                return RedirectToPage("/Shifts/Calendar");
            }
        }
        return Page();
    }
}
