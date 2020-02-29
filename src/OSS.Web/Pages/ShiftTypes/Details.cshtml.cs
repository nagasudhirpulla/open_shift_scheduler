using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftTypes
{
    public class DetailsModel : PageModel
    {
        private readonly OSS.App.Data.AppIdentityDbContext _context;

        public DetailsModel(OSS.App.Data.AppIdentityDbContext context)
        {
            _context = context;
        }

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
    }
}
