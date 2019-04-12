﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ShiftGroup> ShiftGroups { get; set; }

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
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }
}
