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
    public class ShiftsController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftsController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Shifts
        public async Task<IActionResult> Index()
        {
            var shiftScheduleDbContext = _context.Shifts.Include(s => s.ShiftType);
            return View(await shiftScheduleDbContext.ToListAsync());
        }

        // GET: Shifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .Include(s => s.ShiftType)
                .FirstOrDefaultAsync(m => m.ShiftId == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Shifts/Create
        public IActionResult Create()
        {
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "ShiftTypeId", "Name");
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftId,ShiftTypeId,ShiftDate")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "ShiftTypeId", "Name", shift.ShiftTypeId);
            return View(shift);
        }

        // GET: Shifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "ShiftTypeId", "Name", shift.ShiftTypeId);
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftId,ShiftTypeId,ShiftDate")] Shift shift)
        {
            if (id != shift.ShiftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.ShiftId))
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
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypes, "ShiftTypeId", "Name", shift.ShiftTypeId);
            return View(shift);
        }

        // GET: Shifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .Include(s => s.ShiftType)
                .FirstOrDefaultAsync(m => m.ShiftId == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.ShiftId == id);
        }

        // GET: Shifts/AutoCreate
        public IActionResult AutoCreate()
        {
            return View();
        }

        // POST: Shifts/AutoCreate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AutoCreate([Bind("StartDate,EndDate")] AutoInitializeParamsViewModel autoInitParams)
        {
            if (ModelState.IsValid)
            {
                DateTime startDate = autoInitParams.StartDate;
                DateTime endDate = autoInitParams.EndDate;

                // create shift types in the roaster sequence for automated shift 
                List<ShiftType> shiftTypes = await _context.ShiftTypes.OrderBy(st => st.RoasterSequence).ToListAsync();
                List<int> orderedShiftTypeIds = shiftTypes.Select(st => st.ShiftTypeId).ToList();

                // defining function as variable - https://stackoverflow.com/questions/12127020/c-sharp-variable-new-function
                Func<int, int, Tuple<int, int>> GetNextShiftTypeInfo = (currentShiftTypeId, numTimesAlreadyOccured) =>
                {
                    if (numTimesAlreadyOccured == 1)
                    {
                        return new Tuple<int, int>(currentShiftTypeId, 2);
                    }
                    int nextShiftTypeId = -1;
                    int updatedAlreadyOccured = -1;
                    if (orderedShiftTypeIds.Any(sti => sti == currentShiftTypeId))
                    {
                        int currentShiftTypeIdIndex = orderedShiftTypeIds.FindIndex(sti => sti == currentShiftTypeId);
                        int nextShiftTypeIdIndex = currentShiftTypeIdIndex + 1;
                        if (nextShiftTypeIdIndex >= orderedShiftTypeIds.Count)
                        {
                            nextShiftTypeIdIndex = 0;
                        }
                        nextShiftTypeId = orderedShiftTypeIds[nextShiftTypeIdIndex];
                        updatedAlreadyOccured = 1;
                    }
                    return new Tuple<int, int>(nextShiftTypeId, updatedAlreadyOccured);
                };

                // get the shifts on the start Date
                List<Shift> startDayShifts = await _context.Shifts.Include(s => s.ShiftType).Include(s => s.ShiftParticipations).Where(s => s.ShiftDate == startDate).OrderBy(s => s.ShiftType.RoasterSequence).ToListAsync();

                // iterate through each shift in the start date
                foreach (Shift startDayShift in startDayShifts)
                {
                    List<ShiftParticipation> startDayShiftParticipations = startDayShift.ShiftParticipations.ToList();
                    int numTimesAlreadyOccured = 1;
                    int currentShiftTypeId = startDayShift.ShiftTypeId;

                    // iterate through each shift date for automation
                    for (DateTime shiftDate = startDate.AddDays(1); shiftDate.Date <= endDate.Date; shiftDate = shiftDate.AddDays(1))
                    {

                        // get the ShiftType information to use for creating the new shift
                        Tuple<int, int> nextShiftTypeInfo = GetNextShiftTypeInfo(currentShiftTypeId, numTimesAlreadyOccured);
                        currentShiftTypeId = nextShiftTypeInfo.Item1;
                        numTimesAlreadyOccured = nextShiftTypeInfo.Item2;

                        // delete all the shifts of the particular shift type on that day if present
                        List<Shift> existingShifts = await _context.Shifts.Where(s => s.ShiftDate == shiftDate && s.ShiftTypeId == currentShiftTypeId).ToListAsync();
                        foreach (Shift existingShift in existingShifts)
                        {
                            _context.Shifts.Remove(existingShift);
                        }

                        // create new shift with the shift participations
                        Shift newShift = new Shift { ShiftTypeId = currentShiftTypeId, ShiftDate = shiftDate };
                        _context.Add(newShift);
                        await _context.SaveChangesAsync();

                        // create the shift participations for the new shift
                        foreach (ShiftParticipation shiftPart in startDayShift.ShiftParticipations)
                        {
                            ShiftParticipation newShiftPart = new ShiftParticipation { ShiftId = newShift.ShiftId, EmployeeId = shiftPart.EmployeeId };
                            _context.ShiftParticipations.Add(newShiftPart);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Shifts/Display
        public IActionResult Display()
        {
            ShiftsPrintViewModel vm = new ShiftsPrintViewModel { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) };
            return View(vm);
        }

        // Post: Shifts/Display
        [HttpPost]
        public async Task<IActionResult> Display(ShiftsPrintViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // check if start date > end date
                if (vm.StartDate > vm.EndDate)
                {
                    return View(vm);
                }

                //fetch the shift types
                List<ShiftType> shiftTypes = await _context.ShiftTypes.OrderBy(st => st.RoasterSequence).ToListAsync();
                List<int> shiftTypeIds = shiftTypes.Select(st => st.ShiftTypeId).ToList();

                // set the view model shift Types
                vm.ShiftTypes = new List<string>();
                foreach (ShiftType shiftType in shiftTypes)
                {
                    vm.ShiftTypes.Add(shiftType.Name);
                }

                // initiaize the shift participations in view model
                vm.ShiftParticipations = new Dictionary<DateTime, List<List<string>>>();
                for (DateTime dt = vm.StartDate; dt <= vm.EndDate; dt = dt.AddDays(1))
                {
                    vm.ShiftParticipations.Add(dt, new List<List<string>>());
                    foreach (var sType in shiftTypes)
                    {
                        vm.ShiftParticipations[dt].Add(new List<string>());
                    }
                }

                // get the shift participations from db
                List<ShiftParticipation> shiftParticipations = await _context.ShiftParticipations.Include(sp => sp.Shift).Include(sp => sp.Employee).Where(sp => sp.Shift.ShiftDate >= vm.StartDate && sp.Shift.ShiftDate <= vm.EndDate).ToListAsync();

                // assign the fetched shift participations to the vm
                foreach (ShiftParticipation shiftPart in shiftParticipations)
                {
                    DateTime shiftDate = shiftPart.Shift.ShiftDate;
                    int shiftTypeIndex = -1;
                    try
                    {
                        shiftTypeIndex = shiftTypeIds.FindIndex(sTId => sTId == shiftPart.Shift.ShiftTypeId);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    vm.ShiftParticipations[shiftPart.Shift.ShiftDate][shiftTypeIndex].Add(shiftPart.Employee.Name);
                }
            }
            return View(vm);
        }

    }

    public class AutoInitializeParamsViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
