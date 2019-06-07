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
    public class EmployeesController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public EmployeesController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var shiftScheduleDbContext = _context.Employees.Include(e => e.Gender).Include(e => e.ShiftGroup).Include(e => e.ShiftRole);
            return View(await shiftScheduleDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Gender)
                .Include(e => e.ShiftGroup)
                .Include(e => e.ShiftRole)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Genders, "GenderId", "Name");
            ViewData["ShiftGroupId"] = new SelectList(_context.ShiftGroups, "ShiftGroupId", "Name");
            ViewData["ShiftRoleId"] = new SelectList(_context.ShiftRoles, "ShiftRoleId", "RoleName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,OfficeId,GenderId,Name,Phone,Email,Dob,IsActive,ShiftRoleId,ShiftGroupId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Genders, "GenderId", "Name", employee.GenderId);
            ViewData["ShiftGroupId"] = new SelectList(_context.ShiftGroups, "ShiftGroupId", "Name", employee.ShiftGroupId);
            ViewData["ShiftRoleId"] = new SelectList(_context.ShiftRoles, "ShiftRoleId", "RoleName", employee.ShiftRoleId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Genders, "GenderId", "Name", employee.GenderId);
            ViewData["ShiftGroupId"] = new SelectList(_context.ShiftGroups, "ShiftGroupId", "Name", employee.ShiftGroupId);
            ViewData["ShiftRoleId"] = new SelectList(_context.ShiftRoles, "ShiftRoleId", "RoleName", employee.ShiftRoleId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,OfficeId,GenderId,Name,Phone,Email,Dob,IsActive,ShiftRoleId,ShiftGroupId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["GenderId"] = new SelectList(_context.Genders, "GenderId", "Name", employee.GenderId);
            ViewData["ShiftGroupId"] = new SelectList(_context.ShiftGroups, "ShiftGroupId", "Name", employee.ShiftGroupId);
            ViewData["ShiftRoleId"] = new SelectList(_context.ShiftRoles, "ShiftRoleId", "RoleName", employee.ShiftRoleId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Gender)
                .Include(e => e.ShiftGroup)
                .Include(e => e.ShiftRole)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
