using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OSS.App.Security;

namespace OSS.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
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
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
