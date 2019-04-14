﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OpenShiftScheduler.Data;

namespace OpenShiftScheduler.Migrations
{
    [DbContext(typeof(ShiftScheduleDbContext))]
    [Migration("20190414072659_shiftrole")]
    partial class shiftrole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Dob");

                    b.Property<string>("Email")
                        .HasMaxLength(75);

                    b.Property<int>("GenderId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("OfficeId");

                    b.Property<string>("Phone");

                    b.Property<int>("ShiftGroupId");

                    b.Property<int>("ShiftRoleId");

                    b.HasKey("EmployeeId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("GenderId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ShiftGroupId");

                    b.HasIndex("ShiftRoleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.EmployeeShiftSkill", b =>
                {
                    b.Property<int>("EmployeeShiftSkillId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<int>("ShiftSkillId");

                    b.HasKey("EmployeeShiftSkillId");

                    b.HasAlternateKey("EmployeeId", "ShiftSkillId")
                        .HasName("IX_EmployeeShiftSkill");

                    b.HasIndex("ShiftSkillId");

                    b.ToTable("EmployeeShiftSkills");
                });

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.Gender", b =>
                {
                    b.Property<int>("GenderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("GenderId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.ShiftGroup", b =>
                {
                    b.Property<int>("ShiftGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ShiftGroupId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ShiftGroups");
                });

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.ShiftRole", b =>
                {
                    b.Property<int>("ShiftRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ShiftRoleId");

                    b.HasIndex("RoleName")
                        .IsUnique();

                    b.ToTable("ShiftRoles");
                });

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.ShiftSkill", b =>
                {
                    b.Property<int>("ShiftSkillId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ShiftSkillId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ShiftSkills");
                });

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.ShiftType", b =>
                {
                    b.Property<int>("ShiftTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColorString");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("RoasterSequence");

                    b.Property<int>("ShiftSequence");

                    b.Property<int>("StartOffsetHrs");

                    b.Property<int>("StartOffsetMins");

                    b.HasKey("ShiftTypeId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ShiftTypes");
                });

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.Employee", b =>
                {
                    b.HasOne("OpenShiftScheduler.Models.AppModels.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OpenShiftScheduler.Models.AppModels.ShiftGroup", "ShiftGroup")
                        .WithMany()
                        .HasForeignKey("ShiftGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OpenShiftScheduler.Models.AppModels.ShiftRole", "ShiftRole")
                        .WithMany()
                        .HasForeignKey("ShiftRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.EmployeeShiftSkill", b =>
                {
                    b.HasOne("OpenShiftScheduler.Models.AppModels.Employee", "Employee")
                        .WithMany("EmployeeShiftSkills")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OpenShiftScheduler.Models.AppModels.ShiftSkill", "ShiftSkill")
                        .WithMany("EmployeeShiftSkills")
                        .HasForeignKey("ShiftSkillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
