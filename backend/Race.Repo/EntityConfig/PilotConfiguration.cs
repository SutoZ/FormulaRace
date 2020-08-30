using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Race.Model.Models;

namespace Race.Repo.EntityConfig
{
    public class PilotConfiguration : IEntityTypeConfiguration<Pilot>
    {
        public void Configure(EntityTypeBuilder<Pilot> builder)
        {
            builder.ToTable("Pilots");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Nationality).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Code).IsRequired();

            builder.HasOne(x => x.Team)
                .WithMany(x => x.Pilots)
                .HasForeignKey(x => x.TeamId);
        }
    }
}
