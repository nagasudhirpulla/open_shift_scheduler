using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftParticipationTypes
{
    public class EditModel : PageModel
    {
        private readonly OSS.App.Data.AppIdentityDbContext _context;

        public EditModel(OSS.App.Data.AppIdentityDbContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ShiftParticipationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftParticipationTypeExists(ShiftParticipationType.Id))
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

        private bool ShiftParticipationTypeExists(int id)
        {
            return _context.ShiftParticipationTypes.Any(e => e.Id == id);
        }
    }
}
