using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenShiftScheduler.Data;
using OpenShiftScheduler.Models.AppModels;

namespace OpenShiftScheduler.Controllers
{
    [Authorize]
    public class ShiftRolesController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftRolesController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: ShiftRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShiftRoles.ToListAsync());
        }

        // GET: ShiftRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftRole = await _context.ShiftRoles
                .FirstOrDefaultAsync(m => m.ShiftRoleId == id);
            if (shiftRole == null)
            {
                return NotFound();
            }

            return View(shiftRole);
        }

        // GET: ShiftRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftRoleId,RoleName")] ShiftRole shiftRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shiftRole);
        }

        // GET: ShiftRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftRole = await _context.ShiftRoles.FindAsync(id);
            if (shiftRole == null)
            {
                return NotFound();
            }
            return View(shiftRole);
        }

        // POST: ShiftRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftRoleId,RoleName")] ShiftRole shiftRole)
        {
            if (id != shiftRole.ShiftRoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftRoleExists(shiftRole.ShiftRoleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shiftRole);
        }

        // GET: ShiftRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftRole = await _context.ShiftRoles
                .FirstOrDefaultAsync(m => m.ShiftRoleId == id);
            if (shiftRole == null)
            {
                return NotFound();
            }

            return View(shiftRole);
        }

        // POST: ShiftRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shiftRole = await _context.ShiftRoles.FindAsync(id);
            _context.ShiftRoles.Remove(shiftRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftRoleExists(int id)
        {
            return _context.ShiftRoles.Any(e => e.ShiftRoleId == id);
        }
    }
}
