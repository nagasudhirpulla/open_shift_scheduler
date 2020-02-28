﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSS.Domain.Entities;

namespace OSS.Infra.Persistence.Configurations
{
    public class ShiftParticipationTypeConfiguration : IEntityTypeConfiguration<ShiftParticipationType>
    {
        public void Configure(EntityTypeBuilder<ShiftParticipationType> builder)
        {
            builder
            .HasIndex(spt => spt.Name)
            .IsUnique();

            builder
            .Property(b => b.ColorString)
            .IsRequired()
            .HasDefaultValue("#FFFFFF");

            builder
            .Property(b => b.IsBold)
            .IsRequired()
            .HasDefaultValue(false);
        }
    }



}
