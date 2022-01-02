using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.App.Data.Configurations;
public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder
        .HasIndex(g => g.Name)
        .IsUnique();
    }
}