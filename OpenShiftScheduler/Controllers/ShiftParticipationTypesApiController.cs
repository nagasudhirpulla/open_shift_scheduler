using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Administrator")]
    public class ShiftParticipationTypesApiController : ControllerBase
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftParticipationTypesApiController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/ShiftParticipationTypesApi
        [HttpGet]
        public IEnumerable<ShiftParticipationType> GetShiftTypes()
        {
            return _context.ShiftParticipationTypes.OrderBy(spt => spt.ShiftParticipationTypeId);
        }
    }
}