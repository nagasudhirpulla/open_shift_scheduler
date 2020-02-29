using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.App.Security;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftRoles
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class DeleteModel : PageModel
    {
        private readonly OSS.App.Data.AppIdentityDbContext _context;

        public DeleteModel(OSS.App.Data.AppIdentityDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShiftRole ShiftRole { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShiftRole = await _context.ShiftRoles.FirstOrDefaultAsync(m => m.Id == id);

            if (ShiftRole == null)
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

            ShiftRole = await _context.ShiftRoles.FindAsync(id);

            if (ShiftRole != null)
            {
                _context.ShiftRoles.Remove(ShiftRole);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
