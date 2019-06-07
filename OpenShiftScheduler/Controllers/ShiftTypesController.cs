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
    [Authorize(Roles = "Administrator")]
    public class ShiftTypesController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftTypesController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: ShiftTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShiftTypes.OrderBy(x => x.ShiftSequence).ToListAsync());
        }

        // GET: ShiftTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftType = await _context.ShiftTypes
                .SingleOrDefaultAsync(m => m.ShiftTypeId == id);
            if (shiftType == null)
            {
                return NotFound();
            }

            return View(shiftType);
        }

        // GET: ShiftTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftTypeId,Name,StartOffsetHrs,StartOffsetMins,RoasterSequence,ShiftSequence,ColorString")] ShiftType shiftType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shiftType);
        }

        // GET: ShiftTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftType = await _context.ShiftTypes.SingleOrDefaultAsync(m => m.ShiftTypeId == id);
            if (shiftType == null)
            {
                return NotFound();
            }
            return View(shiftType);
        }

        // POST: ShiftTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftTypeId,Name,StartOffsetHrs,StartOffsetMins,RoasterSequence,ShiftSequence,ColorString")] ShiftType shiftType)
        {
            if (id != shiftType.ShiftTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftTypeExists(shiftType.ShiftTypeId))
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
            return View(shiftType);
        }

        // GET: ShiftTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftType = await _context.ShiftTypes
                .SingleOrDefaultAsync(m => m.ShiftTypeId == id);
            if (shiftType == null)
            {
                return NotFound();
            }

            return View(shiftType);
        }

        // POST: ShiftTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shiftType = await _context.ShiftTypes.SingleOrDefaultAsync(m => m.ShiftTypeId == id);
            _context.ShiftTypes.Remove(shiftType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftTypeExists(int id)
        {
            return _context.ShiftTypes.Any(e => e.ShiftTypeId == id);
        }
    }
}
