using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftCycleItems;

public class EditModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public EditModel(OSS.App.Data.AppIdentityDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ShiftCycleItem ShiftCycleItem { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ShiftCycleItem = await _context.ShiftCycleItems
            .Include(s => s.ShiftType).FirstOrDefaultAsync(m => m.Id == id);

        if (ShiftCycleItem == null)
        {
            return NotFound();
        }
        ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "Id", "Name");
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

        _context.Attach(ShiftCycleItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShiftCycleItemExists(ShiftCycleItem.Id))
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

    private bool ShiftCycleItemExists(int id)
    {
        return _context.ShiftCycleItems.Any(e => e.Id == id);
    }
}
