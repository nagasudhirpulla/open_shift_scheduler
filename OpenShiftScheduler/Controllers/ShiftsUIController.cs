using System;
using System.Collections.Generic;
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
        public IEnumerable<Shift> GetShifts()
        {
            return _context.Shifts.Include(s => s.ShiftParticipations);
        }
    }
}