﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Race.Model.Models;

namespace Race.Repo.EntityConfig;

public class PilotConfiguration : IEntityTypeConfiguration<Pilot>
{
    private const int NameMaxLength = 100;
    private const int NationalityMaxLength = 50;
    private const int CodeMaxLength = 10;

    public void Configure(EntityTypeBuilder<Pilot> builder)
    {
        builder.ToTable("Pilots");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(x => x.Nationality)
            .IsRequired()
            .HasMaxLength(NationalityMaxLength);

        builder.Property(x => x.Number).IsRequired();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(CodeMaxLength);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Code).IsUnique();

        builder.HasOne(x => x.Team)
            .WithMany(x => x.Pilots)
            .HasForeignKey(x => x.TeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}