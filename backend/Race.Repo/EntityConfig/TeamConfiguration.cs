﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Race.Model.Models;

namespace Race.Repo.EntityConfig;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    private const int NameMaxLength = 100;
    private const int OwnerNameMaxLength = 100;

    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(x => x.DateOfFoundation).IsRequired();

        builder.Property(x => x.OwnerName)
            .IsRequired()
            .HasMaxLength(OwnerNameMaxLength);

        builder.Property(x => x.ChampionShipPoints).IsRequired();

        builder.HasIndex(x => x.Name);

        builder
            .HasMany(x => x.Pilots)
            .WithOne(x => x.Team)
            .HasForeignKey(x => x.TeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}