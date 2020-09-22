using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Race.Model.Models;

namespace Race.Repo.EntityConfig
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.DateOfFoundation).IsRequired();
            builder.Property(x => x.OwnerName).IsRequired();
            builder.Property(x => x.ChampionShipPoints).IsRequired();

            builder
                .HasMany(x => x.Pilots)
                .WithOne(x => x.Team)
                .HasForeignKey(x => x.TeamId);
        }
    }
}
