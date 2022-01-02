using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.App.Security;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.Genders;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class EditModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public EditModel(OSS.App.Data.AppIdentityDbContext context)
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

    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Gender).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GenderExists(Gender.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool GenderExists(int id)
    {
        return _context.Genders.Any(e => e.Id == id);
    }
}
