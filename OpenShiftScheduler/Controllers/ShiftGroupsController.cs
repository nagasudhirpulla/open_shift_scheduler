using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenShiftScheduler.Data;
using OpenShiftScheduler.Models.AppModels;

namespace OpenShiftScheduler.Controllers
{
    public class ShiftGroupsController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftGroupsController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: ShiftGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShiftGroups.ToListAsync());
        }

        // GET: ShiftGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftGroup = await _context.ShiftGroups
                .FirstOrDefaultAsync(m => m.ShiftGroupId == id);
            if (shiftGroup == null)
            {
                return NotFound();
            }

            return View(shiftGroup);
        }

        // GET: ShiftGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftGroupId,Name")] ShiftGroup shiftGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shiftGroup);
        }

        // GET: ShiftGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftGroup = await _context.ShiftGroups.FindAsync(id);
            if (shiftGroup == null)
            {
                return NotFound();
            }
            return View(shiftGroup);
        }

        // POST: ShiftGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftGroupId,Name")] ShiftGroup shiftGroup)
        {
            if (id != shiftGroup.ShiftGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftGroupExists(shiftGroup.ShiftGroupId))
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
            return View(shiftGroup);
        }

        // GET: ShiftGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftGroup = await _context.ShiftGroups
                .FirstOrDefaultAsync(m => m.ShiftGroupId == id);
            if (shiftGroup == null)
            {
                return NotFound();
            }

            return View(shiftGroup);
        }

        // POST: ShiftGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shiftGroup = await _context.ShiftGroups.FindAsync(id);
            _context.ShiftGroups.Remove(shiftGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftGroupExists(int id)
        {
            return _context.ShiftGroups.Any(e => e.ShiftGroupId == id);
        }
    }
}
