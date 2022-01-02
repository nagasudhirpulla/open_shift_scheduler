using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftTypes;

public class CreateModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public CreateModel(OSS.App.Data.AppIdentityDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public ShiftType ShiftType { get; set; }

    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.ShiftTypes.Add(ShiftType);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
