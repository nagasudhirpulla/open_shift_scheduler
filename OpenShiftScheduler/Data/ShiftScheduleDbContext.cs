using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenShiftScheduler.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Data
{
    public class ShiftScheduleDbContext: DbContext
    {
        public IConfiguration Configuration { get; }

        public DbSet<ShiftType> ShiftTypes { get; set; }
        public DbSet<ShiftRole> ShiftRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ShiftScheduleDbContext(DbContextOptions<ShiftScheduleDbContext> options, IConfiguration configuration) : base(options)
        {
            // set seeds if required
            Configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // defining unique constraints with and without index - https://stackoverflow.com/questions/49526370/is-there-a-data-annotation-for-unique-constraint-in-ef-core-code-first
            // base.OnModelCreating(builder);
            builder.Entity<ShiftRole>()
            .HasKey(b => b.ShiftRoleId);
            builder.Entity<ShiftRole>()
            .HasIndex(b => b.RoleName)
            .IsUnique();

            builder.Entity<Employee>()
            .HasKey(e => e.EmployeeId);
            builder.Entity<Employee>()
            .HasIndex(e => e.Name)
            .IsUnique();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }
}
