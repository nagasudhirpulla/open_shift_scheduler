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
    [Authorize]
    public class EmployeesApiController : ControllerBase
    {
        private readonly ShiftScheduleDbContext _context;

        public EmployeesApiController(ShiftScheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeesApi
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            /**
            * Ordering is done by name
            * */
            return _context.Employees.Include(s => s.EmployeeShiftSkills).OrderBy(e => e.Name);
        }
    }
}