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
    public class ShiftSkillsController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftSkillsController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: ShiftSkills
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShiftSkills.ToListAsync());
        }

        // GET: ShiftSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftSkill = await _context.ShiftSkills
                .FirstOrDefaultAsync(m => m.ShiftSkillId == id);
            if (shiftSkill == null)
            {
                return NotFound();
            }

            return View(shiftSkill);
        }

        // GET: ShiftSkills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftSkills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftSkillId,Name")] ShiftSkill shiftSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shiftSkill);
        }

        // GET: ShiftSkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftSkill = await _context.ShiftSkills.FindAsync(id);
            if (shiftSkill == null)
            {
                return NotFound();
            }
            return View(shiftSkill);
        }

        // POST: ShiftSkills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftSkillId,Name")] ShiftSkill shiftSkill)
        {
            if (id != shiftSkill.ShiftSkillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftSkillExists(shiftSkill.ShiftSkillId))
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
            return View(shiftSkill);
        }

        // GET: ShiftSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftSkill = await _context.ShiftSkills
                .FirstOrDefaultAsync(m => m.ShiftSkillId == id);
            if (shiftSkill == null)
            {
                return NotFound();
            }

            return View(shiftSkill);
        }

        // POST: ShiftSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shiftSkill = await _context.ShiftSkills.FindAsync(id);
            _context.ShiftSkills.Remove(shiftSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftSkillExists(int id)
        {
            return _context.ShiftSkills.Any(e => e.ShiftSkillId == id);
        }
    }
}
