using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.Infra.Persistence.Configurations
{
    public class ShiftGroupConfiguration : IEntityTypeConfiguration<ShiftGroup>
    {
        public void Configure(EntityTypeBuilder<ShiftGroup> builder)
        {
            builder
            .HasIndex(sg => sg.Name)
            .IsUnique();
        }
    }



}
