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
    public class ShiftSkillsApiController : ControllerBase
    {
        private readonly ShiftScheduleDbContext _context;

        public ShiftSkillsApiController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/ShiftSkillsApi
        [HttpGet]
        public IEnumerable<ShiftSkill> GetShiftSkills()
        {
            /**
            * Ordering is done by name
            * */
            return _context.ShiftSkills;
        }
    }
}