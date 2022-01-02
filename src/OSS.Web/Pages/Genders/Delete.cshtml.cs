using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.App.Security;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.Genders;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class DeleteModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public DeleteModel(OSS.App.Data.AppIdentityDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Gender = await _context.Genders.FindAsync(id);

        if (Gender != null)
        {
            _context.Genders.Remove(Gender);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
