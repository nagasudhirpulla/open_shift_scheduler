using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftParticipationTypes;

public class IndexModel : PageModel
{
    private readonly OSS.App.Data.AppIdentityDbContext _context;

    public IndexModel(OSS.App.Data.AppIdentityDbContext context)
    {
        _context = context;
    }

    public IList<ShiftParticipationType> ShiftParticipationType { get; set; }

    public async Task OnGetAsync()
    {
        ShiftParticipationType = await _context.ShiftParticipationTypes.ToListAsync();
    }
}
