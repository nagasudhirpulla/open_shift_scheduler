using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftCycleItems;

public class DeleteModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public DeleteModel(OSS.App.Data.AppIdentityDbContext context)
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
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ShiftCycleItem = await _context.ShiftCycleItems.FindAsync(id);

        if (ShiftCycleItem != null)
        {
            _context.ShiftCycleItems.Remove(ShiftCycleItem);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
