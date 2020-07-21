﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Race.Repo.ApplicationContext;

namespace Race.Repo.Migrations
{
    [DbContext(typeof(RaceContext))]
    partial class RaceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Race.Model.Models.Pilot", b =>
                {
                    b.Property<int>("PilotId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Nationality")
                        .IsRequired();

                    b.Property<string>("Number")
                        .IsRequired();

                    b.HasKey("PilotId");

                    b.ToTable("Pilots");
                });

            modelBuilder.Entity("Race.Model.Models.Result", b =>
                {
                    b.Property<int>("ResultId");

                    b.Property<int>("PilotId");

                    b.Property<int>("RaceId");

                    b.HasKey("ResultId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("Race.Model.Models.Result", b =>
                {
                    b.HasOne("Race.Model.Models.Pilot", "Pilot")
                        .WithMany("Results")
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
