using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.App.Security;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftGroups;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class DeleteModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public DeleteModel(OSS.App.Data.AppIdentityDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ShiftGroup ShiftGroup { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ShiftGroup = await _context.ShiftGroups.FirstOrDefaultAsync(m => m.Id == id);

        if (ShiftGroup == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ShiftGroup = await _context.ShiftGroups.FindAsync(id);

        if (ShiftGroup != null)
        {
            _context.ShiftGroups.Remove(ShiftGroup);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
