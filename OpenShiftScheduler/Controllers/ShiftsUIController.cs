using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenShiftScheduler.Data;
using OpenShiftScheduler.Models.AppModels;

namespace OpenShiftScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsUIController : ControllerBase
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftsUIController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/ShiftsUI/shifts
        [HttpGet("shifts")]
        public async Task<IEnumerable<Shift>> GetShifts([FromQuery]string start_date, [FromQuery]string end_date)
        {
            DateTime startDate = DateTime.ParseExact(start_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(end_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return await _context.Shifts.Where(s => s.ShiftDate >= startDate && s.ShiftDate <= endDate).Include(s => s.ShiftParticipations).ToListAsync();
        }

    }
}