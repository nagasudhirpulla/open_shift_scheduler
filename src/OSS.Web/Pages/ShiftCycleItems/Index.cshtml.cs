using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftCycleItems
{
    public class IndexModel : PageModel
    {
        private readonly OSS.App.Data.AppIdentityDbContext _context;

        public IndexModel(OSS.App.Data.AppIdentityDbContext context)
        {
            _context = context;
        }

        public IList<ShiftCycleItem> ShiftCycleItem { get; set; }

        public async Task OnGetAsync()
        {
            ShiftCycleItem = await _context.ShiftCycleItems
                .Include(s => s.ShiftType).OrderBy(sc => sc.ShiftSequence).ToListAsync();
        }
    }
}
