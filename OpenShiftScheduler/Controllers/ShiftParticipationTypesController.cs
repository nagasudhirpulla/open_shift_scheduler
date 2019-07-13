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
    public class ShiftParticipationTypesController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftParticipationTypesController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: ShiftParticipationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShiftParticipationTypes.ToListAsync());
        }

        // GET: ShiftParticipationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftParticipationType = await _context.ShiftParticipationTypes
                .FirstOrDefaultAsync(m => m.ShiftParticipationTypeId == id);
            if (shiftParticipationType == null)
            {
                return NotFound();
            }

            return View(shiftParticipationType);
        }

        // GET: ShiftParticipationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftParticipationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftParticipationTypeId,Name,IsAbsence,IsBold,ColorString")] ShiftParticipationType shiftParticipationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftParticipationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shiftParticipationType);
        }

        // GET: ShiftParticipationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftParticipationType = await _context.ShiftParticipationTypes.FindAsync(id);
            if (shiftParticipationType == null)
            {
                return NotFound();
            }
            return View(shiftParticipationType);
        }

        // POST: ShiftParticipationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftParticipationTypeId,Name,IsAbsence,IsBold,ColorString")] ShiftParticipationType shiftParticipationType)
        {
            if (id != shiftParticipationType.ShiftParticipationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftParticipationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftParticipationTypeExists(shiftParticipationType.ShiftParticipationTypeId))
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
            return View(shiftParticipationType);
        }

        // GET: ShiftParticipationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftParticipationType = await _context.ShiftParticipationTypes
                .FirstOrDefaultAsync(m => m.ShiftParticipationTypeId == id);
            if (shiftParticipationType == null)
            {
                return NotFound();
            }

            return View(shiftParticipationType);
        }

        // POST: ShiftParticipationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shiftParticipationType = await _context.ShiftParticipationTypes.FindAsync(id);
            _context.ShiftParticipationTypes.Remove(shiftParticipationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftParticipationTypeExists(int id)
        {
            return _context.ShiftParticipationTypes.Any(e => e.ShiftParticipationTypeId == id);
        }
    }
}
