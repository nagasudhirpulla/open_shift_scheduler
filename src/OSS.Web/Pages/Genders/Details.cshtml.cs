using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.App.Security;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.Genders;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class DetailsModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public DetailsModel(OSS.App.Data.AppIdentityDbContext context)
    {
        _context = context;
    }

    public Gender Gender { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Gender = await _context.Genders.FirstOrDefaultAsync(m => m.Id == id);

        if (Gender == null)
        {
            return NotFound();
        }
        return Page();
    }
}
