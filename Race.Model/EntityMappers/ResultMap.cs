using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Race.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Race.Model.EntityMappers
{
    public class ResultMap
    {
        public ResultMap(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(x => x.ResultId);
            builder.Property(x => x.RaceId).IsRequired();
            builder.HasOne(x => x.Pilot)
                .WithMany(x => x.Results)
                .HasForeignKey(x => x.ResultId);
        }
    }
}
