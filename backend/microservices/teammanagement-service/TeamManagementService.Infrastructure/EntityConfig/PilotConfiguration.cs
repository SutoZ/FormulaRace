using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Infrastructure.EntityConfig;

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

        builder.HasQueryFilter(x => x.Active);

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.Active).IsRequired();

        builder.Property(x => x.Nationality)
            .IsRequired()
            .HasMaxLength(NationalityMaxLength);

        builder.Property(x => x.Number).IsRequired();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(CodeMaxLength);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Code).IsUnique().HasDatabaseName("IX_Pilot_Code");

        builder.HasOne(x => x.Team)
            .WithMany(x => x.Pilots)
            .HasForeignKey(x => x.TeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}