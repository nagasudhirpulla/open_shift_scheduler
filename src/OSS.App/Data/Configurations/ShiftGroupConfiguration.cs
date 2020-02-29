using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.App.Data.Configurations
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
