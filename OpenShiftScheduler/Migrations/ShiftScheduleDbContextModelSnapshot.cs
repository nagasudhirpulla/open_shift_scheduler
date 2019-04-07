﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OpenShiftScheduler.Data;

namespace OpenShiftScheduler.Migrations
{
    [DbContext(typeof(ShiftScheduleDbContext))]
    partial class ShiftScheduleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("OpenShiftScheduler.Models.AppModels.ShiftType", b =>
                {
                    b.Property<int>("ShiftTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColorString");

                    b.Property<string>("Name");

                    b.Property<int>("RoasterSequence");

                    b.Property<int>("ShiftSequence");

                    b.Property<int>("StartOffsetHrs");

                    b.Property<int>("StartOffsetMins");

                    b.HasKey("ShiftTypeId");

                    b.ToTable("ShiftTypes");
                });
#pragma warning restore 612, 618
        }
    }
}