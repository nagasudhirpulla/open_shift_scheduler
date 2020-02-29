using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.App.Data.Configurations
{
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



}
