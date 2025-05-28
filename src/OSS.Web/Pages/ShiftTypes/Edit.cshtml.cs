using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftTypes;

public class EditModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public EditModel(OSS.App.Data.AppIdentityDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ShiftType ShiftType { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ShiftType = await _context.ShiftTypes.FirstOrDefaultAsync(m => m.Id == id);

        if (ShiftType == null)
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

        _context.Attach(ShiftType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShiftTypeExists(ShiftType.Id))
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

    private bool ShiftTypeExists(int id)
    {
        return _context.ShiftTypes.Any(e => e.Id == id);
    }
}
