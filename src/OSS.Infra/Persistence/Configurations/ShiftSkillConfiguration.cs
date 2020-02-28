using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.Infra.Persistence.Configurations
{
    public class ShiftSkillConfiguration : IEntityTypeConfiguration<ShiftSkill>
    {
        public void Configure(EntityTypeBuilder<ShiftSkill> builder)
        {
            builder
            .HasIndex(ss => ss.Name)
            .IsUnique();
        }
    }



}
