using Microsoft.EntityFrameworkCore;
using OpenShiftScheduler.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Data
{
    public class ShiftScheduleDbContext: DbContext
    {
        public DbSet<ShiftType> ShiftTypes { get; set; }

        public ShiftScheduleDbContext(DbContextOptions<ShiftScheduleDbContext> options) : base(options)
        {
            // set seeds if required
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
