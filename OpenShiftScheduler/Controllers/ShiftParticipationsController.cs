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
    public class ShiftParticipationsController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftParticipationsController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: ShiftParticipations
        public async Task<IActionResult> Index()
        {
            var shiftScheduleDbContext = _context.ShiftParticipations.Include(s => s.Employee).Include(s => s.Shift);
            return View(await shiftScheduleDbContext.ToListAsync());
        }

        // GET: ShiftParticipations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftParticipation = await _context.ShiftParticipations
                .Include(s => s.Employee)
                .Include(s => s.Shift)
                .FirstOrDefaultAsync(m => m.ShiftParticipationId == id);
            if (shiftParticipation == null)
            {
                return NotFound();
            }

            return View(shiftParticipation);
        }

        // GET: ShiftParticipations/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            // https://stackoverflow.com/questions/17951524/mvc-and-entity-framework-select-list
            ViewData["ShiftId"] = _context.Shifts.Select(s => new SelectListItem
            {
                Text = $"{s.ShiftDate.ToString("dd-MMM-yyyy")},{s.ShiftType.Name}",
                Value = s.ShiftId.ToString()
            }).ToList();
            //ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftId");
            return View();
        }

        // POST: ShiftParticipations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftParticipationId,EmployeeId,ShiftId")] ShiftParticipation shiftParticipation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftParticipation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", shiftParticipation.EmployeeId);
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftId", shiftParticipation.ShiftId);
            return View(shiftParticipation);
        }

        // GET: ShiftParticipations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftParticipation = await _context.ShiftParticipations.FindAsync(id);
            if (shiftParticipation == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", shiftParticipation.EmployeeId);
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftId", shiftParticipation.ShiftId);
            return View(shiftParticipation);
        }

        // POST: ShiftParticipations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftParticipationId,EmployeeId,ShiftId")] ShiftParticipation shiftParticipation)
        {
            if (id != shiftParticipation.ShiftParticipationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftParticipation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftParticipationExists(shiftParticipation.ShiftParticipationId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", shiftParticipation.EmployeeId);
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftId", shiftParticipation.ShiftId);
            return View(shiftParticipation);
        }

        // GET: ShiftParticipations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftParticipation = await _context.ShiftParticipations
                .Include(s => s.Employee)
                .Include(s => s.Shift)
                .FirstOrDefaultAsync(m => m.ShiftParticipationId == id);
            if (shiftParticipation == null)
            {
                return NotFound();
            }

            return View(shiftParticipation);
        }

        // POST: ShiftParticipations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shiftParticipation = await _context.ShiftParticipations.FindAsync(id);
            _context.ShiftParticipations.Remove(shiftParticipation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftParticipationExists(int id)
        {
            return _context.ShiftParticipations.Any(e => e.ShiftParticipationId == id);
        }
    }
}
