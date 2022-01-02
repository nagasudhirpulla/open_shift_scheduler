using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.App.Data.Configurations;
public class ShiftRoleConfiguration : IEntityTypeConfiguration<ShiftRole>
{
    public void Configure(EntityTypeBuilder<ShiftRole> builder)
    {
        builder
        .HasIndex(b => b.RoleName)
        .IsUnique();
    }
}
