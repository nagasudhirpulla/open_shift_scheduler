using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.Infra.Persistence.Configurations
{
    public class ShiftRoleConfiguration : IEntityTypeConfiguration<ShiftRole>
    {
        public void Configure(EntityTypeBuilder<ShiftRole> builder)
        {
            builder
            .HasIndex(b => b.RoleName)
            .IsUnique();
        }
    }

    public class ShiftGroupConfiguration : IEntityTypeConfiguration<ShiftGroup>
    {
        public void Configure(EntityTypeBuilder<ShiftGroup> builder)
        {
            builder
            .HasIndex(sg => sg.Name)
            .IsUnique();
        }
    }
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder
            .HasIndex(g => g.Name)
            .IsUnique();
        }
    }
    public class ShiftSkillConfiguration : IEntityTypeConfiguration<ShiftSkill>
    {
        public void Configure(EntityTypeBuilder<ShiftSkill> builder)
        {
            builder
            .HasIndex(ss => ss.Name)
            .IsUnique();
        }
    }
    public class ShiftTypeConfiguration : IEntityTypeConfiguration<ShiftType>
    {
        public void Configure(EntityTypeBuilder<ShiftType> builder)
        {
            builder
            .HasIndex(st => st.Name)
            .IsUnique();
        }
    }

    public class ShiftParticipationTypeConfiguration : IEntityTypeConfiguration<ShiftParticipationType>
    {
        public void Configure(EntityTypeBuilder<ShiftParticipationType> builder)
        {
            builder
            .HasIndex(spt => spt.Name)
            .IsUnique();

            builder
            .Property(b => b.ColorString)
            .IsRequired()
            .HasDefaultValue("#FFFFFF");

            builder
            .Property(b => b.IsBold)
            .IsRequired()
            .HasDefaultValue(false);
        }
    }

    public class EmployeeShiftSkillConfiguration : IEntityTypeConfiguration<EmployeeShiftSkill>
    {
        public void Configure(EntityTypeBuilder<EmployeeShiftSkill> builder)
        {
            // many to many relationship in ef-core - https://www.entityframeworktutorial.net/efcore/configure-many-to-many-relationship-in-ef-core.aspx
            builder.HasKey(b => new { b.EmployeeId, b.ShiftSkillId });

            builder
                .HasOne(rcc => rcc.Employee)
                .WithMany(s => s.EmployeeShiftSkills)
                .HasForeignKey(rcc => rcc.EmployeeId);


            builder
                .HasOne(rcc => rcc.ShiftSkill)
                .WithMany()
                .HasForeignKey(rcc => rcc.ShiftSkillId);
        }
    }

    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder
                .HasIndex(s => new { s.ShiftTypeId, s.ShiftDate })
                .IsUnique();
        }
    }
    public class ShiftParticipationConfiguration : IEntityTypeConfiguration<ShiftParticipation>
    {
        public void Configure(EntityTypeBuilder<ShiftParticipation> builder)
        {
            builder
                .HasIndex(sp => new { sp.ShiftId, sp.EmployeeId })
                .IsUnique();

            builder
            .HasOne(sp => sp.ShiftParticipationType)
            .WithMany()
            .HasForeignKey(sp => sp.ShiftParticipationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .Property(b => b.ParticipationSequence)
            .IsRequired()
            .HasDefaultValue(0);
        }
    }

    public class ShiftCycleItemConfiguration : IEntityTypeConfiguration<ShiftCycleItem>
    {
        public void Configure(EntityTypeBuilder<ShiftCycleItem> builder)
        {
            builder
            .HasIndex(sci => sci.ShiftSequence)
            .IsUnique();

            builder
            .HasOne(sci => sci.ShiftType)
            .WithMany()
            .HasForeignKey(sci => sci.ShiftTypeId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }



}
