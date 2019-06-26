using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenShiftScheduler.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Data
{
    public class ShiftScheduleDbContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public DbSet<ShiftType> ShiftTypes { get; set; }
        public DbSet<ShiftRole> ShiftRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ShiftSkill> ShiftSkills { get; set; }
        public DbSet<EmployeeShiftSkill> EmployeeShiftSkills { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ShiftGroup> ShiftGroups { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftParticipation> ShiftParticipations { get; set; }
        public DbSet<ShiftParticipationType> ShiftParticipationTypes { get; set; }

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
            builder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();

            builder.Entity<ShiftGroup>()
            .HasKey(sg => sg.ShiftGroupId);
            builder.Entity<ShiftGroup>()
            .HasIndex(sg => sg.Name)
            .IsUnique();

            builder.Entity<Gender>()
            .HasKey(g => g.GenderId);
            builder.Entity<Gender>()
            .HasIndex(g => g.Name)
            .IsUnique();

            builder.Entity<ShiftSkill>()
            .HasKey(sk => sk.ShiftSkillId);
            builder.Entity<ShiftSkill>()
            .HasIndex(sk => sk.Name)
            .IsUnique();

            builder.Entity<ShiftType>()
            .HasKey(st => st.ShiftTypeId);
            builder.Entity<ShiftType>()
            .HasIndex(st => st.Name)
            .IsUnique();

            builder.Entity<ShiftParticipationType>()
            .HasKey(b => b.ShiftParticipationTypeId);
            builder.Entity<ShiftParticipationType>()
            .HasIndex(b => b.Name)
            .IsUnique();

            builder.Entity<EmployeeShiftSkill>().HasKey(ess => ess.EmployeeShiftSkillId);
            // setting multiple columns as alternate key - https://stackoverflow.com/questions/18889218/unique-key-constraints-for-multiple-columns-in-entity-framework
            builder.Entity<EmployeeShiftSkill>().HasIndex(ess => new { ess.EmployeeId, ess.ShiftSkillId }).IsUnique();

            builder.Entity<Shift>().HasKey(s => s.ShiftId);
            builder.Entity<Shift>().HasIndex(s => new { s.ShiftTypeId, s.ShiftDate }).IsUnique();

            builder.Entity<ShiftParticipation>().HasKey(sp => sp.ShiftParticipationId);
            builder.Entity<ShiftParticipation>().HasIndex(sp => new { sp.ShiftId, sp.EmployeeId }).IsUnique();

            builder.Entity<ShiftParticipation>()
            .HasOne(sp => sp.ShiftParticipationType)
            .WithMany()
            .HasForeignKey(sp => sp.ShiftParticipationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ShiftParticipation>()
            .Property(b => b.ParticipationSequence)
            .IsRequired()
            .HasDefaultValue(0);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }
}
