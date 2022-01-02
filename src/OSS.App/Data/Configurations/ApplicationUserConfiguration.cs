using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.App.Data.Configurations;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
        .HasOne(p => p.ShiftGroup)
        .WithMany(sg => sg.Employees)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);
    }
}