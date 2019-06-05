using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenShiftScheduler.Data;
using OpenShiftScheduler.Models.AppModels;

namespace OpenShiftScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShiftGroupsApiController : ControllerBase
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftGroupsApiController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/ShiftGroupsApi
        [HttpGet]
        public IEnumerable<ShiftGroup> GetShiftGroups()
        {
            /**
            * Ordering is done by shift sequence in a day
            * */
            return _context.ShiftGroups.Include(sg => sg.Employees);
        }
    }
}