using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSS.App.Security;
using OSS.App.Shifts.Commands.AutoCreateShifts;

namespace OSS.Web.Pages.Shifts
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class AutoCreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public AutoCreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public AutoCreateShiftsCommand AutoCreateShiftsCommand { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool res = await _mediator.Send(AutoCreateShiftsCommand);

            return RedirectToPage("./Edit");
        }
    }
}