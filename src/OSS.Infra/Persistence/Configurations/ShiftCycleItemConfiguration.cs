using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.Infra.Persistence.Configurations
{
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
