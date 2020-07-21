using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Race.Model.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Race.Model.EntityMappers
{
    public class PilotMap
    {
        public PilotMap(EntityTypeBuilder<Pilot> builder)
        {
            builder.HasKey(x => x.PilotId);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Nationality).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Code).IsRequired();
            builder.HasMany(x => x.Results).WithOne(x => x.Pilot).HasForeignKey(x => x.ResultId);
        }
    }
}
