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
    public class EmployeeShiftSkillsController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public EmployeeShiftSkillsController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeShiftSkills
        public async Task<IActionResult> Index()
        {
            var shiftScheduleDbContext = _context.EmployeeShiftSkills.Include(e => e.Employee).Include(e => e.ShiftSkill);
            return View(await shiftScheduleDbContext.ToListAsync());
        }

        // GET: EmployeeShiftSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeShiftSkill = await _context.EmployeeShiftSkills
                .Include(e => e.Employee)
                .Include(e => e.ShiftSkill)
                .FirstOrDefaultAsync(m => m.EmployeeShiftSkillId == id);
            if (employeeShiftSkill == null)
            {
                return NotFound();
            }

            return View(employeeShiftSkill);
        }

        // GET: EmployeeShiftSkills/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            ViewData["ShiftSkillId"] = new SelectList(_context.ShiftSkills, "ShiftSkillId", "Name");
            return View();
        }

        // POST: EmployeeShiftSkills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeShiftSkillId,EmployeeId,ShiftSkillId")] EmployeeShiftSkill employeeShiftSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeShiftSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", employeeShiftSkill.EmployeeId);
            ViewData["ShiftSkillId"] = new SelectList(_context.ShiftSkills, "ShiftSkillId", "Name", employeeShiftSkill.ShiftSkillId);
            return View(employeeShiftSkill);
        }

        // GET: EmployeeShiftSkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeShiftSkill = await _context.EmployeeShiftSkills.FindAsync(id);
            if (employeeShiftSkill == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", employeeShiftSkill.EmployeeId);
            ViewData["ShiftSkillId"] = new SelectList(_context.ShiftSkills, "ShiftSkillId", "Name", employeeShiftSkill.ShiftSkillId);
            return View(employeeShiftSkill);
        }

        // POST: EmployeeShiftSkills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeShiftSkillId,EmployeeId,ShiftSkillId")] EmployeeShiftSkill employeeShiftSkill)
        {
            if (id != employeeShiftSkill.EmployeeShiftSkillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // employeeShiftSkill.Employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeShiftSkill.EmployeeId);
                    // employeeShiftSkill.ShiftSkill = await _context.ShiftSkills.FirstOrDefaultAsync(sk => sk.ShiftSkillId == employeeShiftSkill.ShiftSkillId);
                    _context.Update(employeeShiftSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeShiftSkillExists(employeeShiftSkill.EmployeeShiftSkillId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", employeeShiftSkill.EmployeeId);
            ViewData["ShiftSkillId"] = new SelectList(_context.ShiftSkills, "ShiftSkillId", "Name", employeeShiftSkill.ShiftSkillId);
            return View(employeeShiftSkill);
        }

        // GET: EmployeeShiftSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeShiftSkill = await _context.EmployeeShiftSkills
                .Include(e => e.Employee)
                .Include(e => e.ShiftSkill)
                .FirstOrDefaultAsync(m => m.EmployeeShiftSkillId == id);
            if (employeeShiftSkill == null)
            {
                return NotFound();
            }

            return View(employeeShiftSkill);
        }

        // POST: EmployeeShiftSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeShiftSkill = await _context.EmployeeShiftSkills.FindAsync(id);
            _context.EmployeeShiftSkills.Remove(employeeShiftSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeShiftSkillExists(int id)
        {
            return _context.EmployeeShiftSkills.Any(e => e.EmployeeShiftSkillId == id);
        }
    }
}
