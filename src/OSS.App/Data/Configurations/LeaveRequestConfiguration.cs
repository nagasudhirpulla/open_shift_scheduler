using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.App.Data.Configurations;
public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder
        .HasIndex(lr => new { lr.EmployeeId, lr.StartDate, lr.EndDate })
        .IsUnique();

        builder
            .HasMany(lr => lr.LeaveRequestComments)
            .WithOne(lrc => lrc.LeaveRequest)
            .OnDelete(DeleteBehavior.Cascade);
    }
}