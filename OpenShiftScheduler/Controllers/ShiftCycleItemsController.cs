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
    public class ShiftCycleItemsController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftCycleItemsController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: ShiftCycleItems
        public async Task<IActionResult> Index()
        {
            var shiftScheduleDbContext = _context.ShiftCycleItems.Include(s => s.ShiftType).OrderBy(s => s.ShiftSequence);
            return View(await shiftScheduleDbContext.ToListAsync());
        }

        // GET: ShiftCycleItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftCycleItem = await _context.ShiftCycleItems
                .Include(s => s.ShiftType)
                .FirstOrDefaultAsync(m => m.ShiftCycleItemId == id);
            if (shiftCycleItem == null)
            {
                return NotFound();
            }

            return View(shiftCycleItem);
        }

        // GET: ShiftCycleItems/Create
        public IActionResult Create()
        {
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "ShiftTypeId", "Name");
            return View();
        }

        // POST: ShiftCycleItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftCycleItemId,ShiftSequence,ShiftTypeId")] ShiftCycleItem shiftCycleItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftCycleItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "ShiftTypeId", "Name", shiftCycleItem.ShiftTypeId);
            return View(shiftCycleItem);
        }

        // GET: ShiftCycleItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftCycleItem = await _context.ShiftCycleItems.FindAsync(id);
            if (shiftCycleItem == null)
            {
                return NotFound();
            }
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "ShiftTypeId", "Name", shiftCycleItem.ShiftTypeId);
            return View(shiftCycleItem);
        }

        // POST: ShiftCycleItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftCycleItemId,ShiftSequence,ShiftTypeId")] ShiftCycleItem shiftCycleItem)
        {
            if (id != shiftCycleItem.ShiftCycleItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftCycleItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftCycleItemExists(shiftCycleItem.ShiftCycleItemId))
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
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "ShiftTypeId", "Name", shiftCycleItem.ShiftTypeId);
            return View(shiftCycleItem);
        }

        // GET: ShiftCycleItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftCycleItem = await _context.ShiftCycleItems
                .Include(s => s.ShiftType)
                .FirstOrDefaultAsync(m => m.ShiftCycleItemId == id);
            if (shiftCycleItem == null)
            {
                return NotFound();
            }

            return View(shiftCycleItem);
        }

        // POST: ShiftCycleItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shiftCycleItem = await _context.ShiftCycleItems.FindAsync(id);
            _context.ShiftCycleItems.Remove(shiftCycleItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftCycleItemExists(int id)
        {
            return _context.ShiftCycleItems.Any(e => e.ShiftCycleItemId == id);
        }
    }
}
