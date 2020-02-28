using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.Infra.Persistence.Configurations
{
    public class ShiftTypeConfiguration : IEntityTypeConfiguration<ShiftType>
    {
        public void Configure(EntityTypeBuilder<ShiftType> builder)
        {
            builder
            .HasIndex(st => st.Name)
            .IsUnique();
        }
    }



}
