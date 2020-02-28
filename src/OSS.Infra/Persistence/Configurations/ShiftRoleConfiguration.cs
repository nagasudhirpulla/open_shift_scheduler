using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.Infra.Persistence.Configurations
{
    public class ShiftRoleConfiguration : IEntityTypeConfiguration<ShiftRole>
    {
        public void Configure(EntityTypeBuilder<ShiftRole> builder)
        {
            builder
            .HasIndex(b => b.RoleName)
            .IsUnique();
        }
    }
}
