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
    [Authorize(Roles = "Administrator, GuestUser")]
    public class ShiftsController : Controller
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftsController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Shifts
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public IActionResult AutoCreate()
        {
            return View();
        }

        // POST: Shifts/AutoCreate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AutoCreate([Bind("StartDate,EndDate")] AutoInitializeParamsViewModel autoInitParams)
        {
            if (ModelState.IsValid)
            {
                DateTime startDate = autoInitParams.StartDate;
                DateTime endDate = autoInitParams.EndDate;

                // create shift type Ids in the roaster sequence for automated shift 
                List<int> orderedShiftTypeIds = await _context.ShiftCycleItems.OrderBy(sci => sci.ShiftSequence).Select(sci => sci.ShiftTypeId).ToListAsync();

                // List<ShiftType> shiftTypes = await _context.ShiftTypes.OrderBy(st => st.RoasterSequence).ToListAsync();
                // List<int> orderedShiftTypeIds_old = shiftTypes.Select(st => st.ShiftTypeId).ToList();

                //Func<int, int, Tuple<int, int>> GetNextShiftTypeInfo = (currentShiftTypeId, numTimesAlreadyOccured) =>
                //{
                //    if (numTimesAlreadyOccured == 1)
                //    {
                //        return new Tuple<int, int>(currentShiftTypeId, 2);
                //    }
                //    int nextShiftTypeId = -1;
                //    int updatedAlreadyOccured = -1;
                //    if (orderedShiftTypeIds.Any(sti => sti == currentShiftTypeId))
                //    {
                //        int currentShiftTypeIdIndex = orderedShiftTypeIds.FindIndex(sti => sti == currentShiftTypeId);
                //        int nextShiftTypeIdIndex = currentShiftTypeIdIndex + 1;
                //        if (nextShiftTypeIdIndex >= orderedShiftTypeIds.Count)
                //        {
                //            nextShiftTypeIdIndex = 0;
                //        }
                //        nextShiftTypeId = orderedShiftTypeIds[nextShiftTypeIdIndex];
                //        updatedAlreadyOccured = 1;
                //    }
                //    return new Tuple<int, int>(nextShiftTypeId, updatedAlreadyOccured);
                //};

                // defining function as variable - https://stackoverflow.com/questions/12127020/c-sharp-variable-new-function
                Func<int, int> GetNextShiftTypeIndex = (currentShiftTypeIndex) =>
                {
                    int nextShiftTypeIndex = -1;
                    if (currentShiftTypeIndex < 0)
                    {
                        return -1;
                    }
                    if (currentShiftTypeIndex == orderedShiftTypeIds.Count - 1)
                    {
                        nextShiftTypeIndex = 0;
                    }
                    else
                    {
                        nextShiftTypeIndex = currentShiftTypeIndex + 1;
                    }
                    return nextShiftTypeIndex;
                };

                // get the shifts on the start Date
                List<Shift> startDayShifts = await _context.Shifts.Include(s => s.ShiftType).Include(s => s.ShiftParticipations).Where(s => s.ShiftDate == startDate).OrderBy(s => s.ShiftType.RoasterSequence).ToListAsync();

                // iterate through each shift in the start date
                foreach (Shift startDayShift in startDayShifts)
                {
                    List<ShiftParticipation> startDayShiftParticipations = startDayShift.ShiftParticipations.ToList();
                    int currentShiftTypeId = startDayShift.ShiftTypeId;
                    int currentShiftTypeIndex = orderedShiftTypeIds.IndexOf(currentShiftTypeId);

                    // iterate through each shift date for automation
                    for (DateTime shiftDate = startDate.AddDays(1); (shiftDate.Date <= endDate.Date) && (currentShiftTypeIndex != -1); shiftDate = shiftDate.AddDays(1))
                    {
                        // get the next ShiftType in the shift cycle sequence creating the new shift
                        currentShiftTypeIndex = GetNextShiftTypeIndex(currentShiftTypeIndex);
                        currentShiftTypeId = orderedShiftTypeIds[currentShiftTypeIndex];

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
                            ShiftParticipation newShiftPart = new ShiftParticipation { ShiftId = newShift.ShiftId, EmployeeId = shiftPart.EmployeeId, ParticipationSequence = shiftPart.ParticipationSequence, ShiftParticipationTypeId = shiftPart.ShiftParticipationTypeId };
                            _context.ShiftParticipations.Add(newShiftPart);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Shifts/Roaster
        [Authorize(Roles = "Administrator, GuestUser")]
        public IActionResult Roaster()
        {
            DateTime dt = DateTime.Now;
            DateTime monthStart = new DateTime(dt.Year, dt.Month, 1);
            DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);
            ShiftsPrintViewModel vm = new ShiftsPrintViewModel { StartDate = monthStart, EndDate = monthEnd };
            return View(vm);
        }

        // Post: Shifts/Roaster
        [HttpPost]
        [Authorize(Roles = "Administrator, GuestUser")]
        public async Task<IActionResult> Roaster(ShiftsPrintViewModel vm)
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

                //fetch the shift Participation types
                List<ShiftParticipationType> shiftPartTypes = await _context.ShiftParticipationTypes.ToListAsync();
                List<int> shiftPartTypeIds = shiftPartTypes.Select(spt => spt.ShiftParticipationTypeId).ToList();

                // set the view model shift Types
                vm.ShiftTypes = new List<string>();
                foreach (ShiftType shiftType in shiftTypes)
                {
                    vm.ShiftTypes.Add(shiftType.Name);
                }

                // initiaize the shift participations in view model
                vm.ShiftParticipations = new Dictionary<DateTime, List<List<Tuple<string, ShiftParticipationType>>>>();
                for (DateTime dt = vm.StartDate; dt <= vm.EndDate; dt = dt.AddDays(1))
                {
                    vm.ShiftParticipations.Add(dt, new List<List<Tuple<string, ShiftParticipationType>>>());
                    foreach (var sType in shiftTypes)
                    {
                        vm.ShiftParticipations[dt].Add(new List<Tuple<string, ShiftParticipationType>>());
                    }
                }

                // get the shift participations from db
                List<ShiftParticipation> shiftParticipations = await _context.ShiftParticipations.Include(sp => sp.Shift).Include(sp => sp.Employee).Where(sp => sp.Shift.ShiftDate >= vm.StartDate && sp.Shift.ShiftDate <= vm.EndDate).ToListAsync();

                // assign the fetched shift participations to the vm
                foreach (ShiftParticipation shiftPart in shiftParticipations.OrderBy(sp => sp.ParticipationSequence))
                {
                    DateTime shiftDate = shiftPart.Shift.ShiftDate;
                    ShiftParticipationType participationType = null;
                    int shiftTypeIndex = -1;
                    int shiftPartTypeIndex = -1;
                    try
                    {
                        shiftTypeIndex = shiftTypeIds.FindIndex(sTId => sTId == shiftPart.Shift.ShiftTypeId);
                        shiftPartTypeIndex = shiftPartTypeIds.FindIndex(sPTId => sPTId == shiftPart.ShiftParticipationTypeId);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    if (shiftPartTypeIndex != -1)
                    {
                        participationType = shiftPartTypes[shiftPartTypeIndex];
                    }
                    vm.ShiftParticipations[shiftPart.Shift.ShiftDate][shiftTypeIndex].Add(new Tuple<string, ShiftParticipationType>(shiftPart.Employee.Name, participationType));
                }

                // get all the shift objects for comments
                List<Shift> shifts = await _context.Shifts.Where(s => s.ShiftDate >= vm.StartDate && s.ShiftDate <= vm.EndDate).OrderBy(s => s.ShiftDate).ToListAsync();

                // assign the fetched shift comments to the vm
                vm.ShiftComments = new List<Tuple<DateTime, string, string>>();
                foreach (Shift shift in shifts)
                {
                    if (shift.Comments != null && shift.Comments != "")
                    {
                        DateTime shiftDate = shift.ShiftDate;
                        int shiftTypeIndex = -1;
                        try
                        {
                            shiftTypeIndex = shiftTypeIds.FindIndex(sTId => sTId == shift.ShiftTypeId);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        vm.ShiftComments.Add(new Tuple<DateTime, string, string>(shift.ShiftDate, shiftTypes[shiftTypeIndex].Name, shift.Comments));
                    }
                }
            }
            return View(vm);
        }

        // GET: Shifts/EmployeesCalendar
        public IActionResult EmployeesCalendar()
        {
            DateTime dt = DateTime.Now;
            DateTime monthStart = new DateTime(dt.Year, dt.Month, 1);
            DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);
            EmployeesCalendarPrintViewModel vm = new EmployeesCalendarPrintViewModel { StartDate = monthStart, EndDate = monthEnd };
            return View(vm);
        }

        // Post: Shifts/EmployeesCalendar
        [HttpPost]
        public async Task<IActionResult> EmployeesCalendar(EmployeesCalendarPrintViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // check if start date > end date
                if (vm.StartDate > vm.EndDate)
                {
                    return View(vm);
                }

                // fetch employees
                List<Employee> employees = await _context.Employees.OrderBy(e => e.Name).ToListAsync();
                List<string> employeeNames = employees.Select(e => e.Name).ToList();

                //fetch the shift types
                List<ShiftType> shiftTypes = await _context.ShiftTypes.OrderBy(st => st.RoasterSequence).ToListAsync();
                List<int> shiftTypeIds = shiftTypes.Select(st => st.ShiftTypeId).ToList();

                // initiaize the EmployeeShifts in view model
                vm.EmployeeShifts = new Dictionary<String, List<string>>();
                vm.EmployeeShiftSummaries = new Dictionary<string, Dictionary<string, int>>();
                foreach (string employeeName in employeeNames)
                {
                    vm.EmployeeShifts.Add(employeeName, new List<string>());
                    for (DateTime dt = vm.StartDate; dt <= vm.EndDate; dt = dt.AddDays(1))
                    {
                        vm.EmployeeShifts[employeeName].Add("");
                    }

                    // initialize the shift counts summary for each employee
                    vm.EmployeeShiftSummaries.Add(employeeName, new Dictionary<string, int>());
                    foreach (ShiftType shiftType in shiftTypes)
                    {
                        vm.EmployeeShiftSummaries[employeeName].Add(shiftType.Name, 0);
                    }
                }

                //get the shiftParticipationTypes from db
                List<ShiftParticipationType> shiftParticipationTypes = await _context.ShiftParticipationTypes.ToListAsync();
                List<int> nonAbsenceShiftPartTypes = shiftParticipationTypes.Where(spt => spt.IsAbsence == true).Select(spt => spt.ShiftParticipationTypeId).ToList();
                // get the required non absence shift participations from db
                List<ShiftParticipation> shiftParticipations = await _context.ShiftParticipations.Include(sp => sp.Shift).Include(sp => sp.Employee).Where(sp => (sp.ShiftParticipationTypeId == null || nonAbsenceShiftPartTypes.Any(nasptId => nasptId == sp.ShiftParticipationTypeId)) && sp.Shift.ShiftDate >= vm.StartDate && sp.Shift.ShiftDate <= vm.EndDate).ToListAsync();

                // assign the fetched shift participations to the vm
                foreach (ShiftParticipation shiftPart in shiftParticipations)
                {
                    // find the shiftDate
                    DateTime shiftDate = shiftPart.Shift.ShiftDate;

                    // find the shiftType Name
                    int shiftTypeIndex = -1;
                    try
                    {
                        shiftTypeIndex = shiftTypeIds.FindIndex(sTId => sTId == shiftPart.Shift.ShiftTypeId);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    string shiftTypeName = shiftTypes[shiftTypeIndex].Name;

                    // find the employee Name
                    string employeeName = shiftPart.Employee.Name;

                    // check if employee name exists
                    int empIndex = -1;
                    try
                    {
                        empIndex = employeeNames.FindIndex(eName => eName == shiftPart.Employee.Name);
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    // find the date list index by the shiftParticipation Date
                    int dateIndex = (int)Math.Floor((shiftDate - vm.StartDate).TotalDays);

                    // set the employeeShift value
                    vm.EmployeeShifts[employeeName][dateIndex] = shiftTypeName;

                    // increment the number of shifts in summary
                    vm.EmployeeShiftSummaries[employeeName][shiftTypeName] += 1;
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
