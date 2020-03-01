using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSS.App.Data;
using OSS.Domain.Entities;

namespace OSS.Web.Pages.ShiftCycleItems
{
    public class CreateModel : PageModel
    {
        private readonly OSS.App.Data.AppIdentityDbContext _context;

        public CreateModel(OSS.App.Data.AppIdentityDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public ShiftCycleItem ShiftCycleItem { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ShiftCycleItems.Add(ShiftCycleItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
