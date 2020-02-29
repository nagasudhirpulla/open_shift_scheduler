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

namespace OSS.Web.Pages.Genders
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class IndexModel : PageModel
    {
        private readonly OSS.App.Data.AppIdentityDbContext _context;

        public IndexModel(OSS.App.Data.AppIdentityDbContext context)
        {
            _context = context;
        }

        public IList<Gender> Gender { get;set; }

        public async Task OnGetAsync()
        {
            Gender = await _context.Genders.ToListAsync();
        }
    }
}
