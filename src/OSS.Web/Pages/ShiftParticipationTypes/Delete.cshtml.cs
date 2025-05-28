using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftParticipationTypes;

public class DeleteModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public DeleteModel(OSS.App.Data.AppIdentityDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ShiftParticipationType ShiftParticipationType { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ShiftParticipationType = await _context.ShiftParticipationTypes.FirstOrDefaultAsync(m => m.Id == id);

        if (ShiftParticipationType == null)
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

        ShiftParticipationType = await _context.ShiftParticipationTypes.FindAsync(id);

        if (ShiftParticipationType != null)
        {
            _context.ShiftParticipationTypes.Remove(ShiftParticipationType);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
