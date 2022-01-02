using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.App.Data.Configurations;
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