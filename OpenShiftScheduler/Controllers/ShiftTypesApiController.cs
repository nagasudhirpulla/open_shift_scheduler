using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenShiftScheduler.Data;
using OpenShiftScheduler.Models.AppModels;

namespace OpenShiftScheduler.Controllers
{
    [EnableCors("anyorigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftTypesApiController : ControllerBase
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftTypesApiController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/ShiftTypesApi
        [HttpGet]
        public IEnumerable<ShiftType> GetShiftTypes()
        {
            /**
            * Ordering is done by shift sequence in a day
            * */
            return _context.ShiftTypes.OrderBy(st => st.ShiftSequence);
        }
    }
}