﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Race.Repo.ApplicationContext;

namespace Race.Repo.Migrations
{
    [DbContext(typeof(RaceContext))]
    [Migration("20200820095417_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Race.Model.Models.Pilot", b =>
                {
                    b.Property<int>("Id")
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

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Pilots");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "HAM",
                            Name = "Lewis Hamilton",
                            Nationality = "British",
                            Number = "44",
                            TeamId = 2
                        },
                        new
                        {
                            Id = 2,
                            Code = "OCO",
                            Name = "Esteban Occon",
                            Nationality = "France",
                            Number = "50",
                            TeamId = 4
                        },
                        new
                        {
                            Id = 3,
                            Code = "ROS",
                            Name = "Nico Rosberg",
                            Nationality = "German",
                            Number = "6",
                            TeamId = 1
                        },
                        new
                        {
                            Id = 4,
                            Code = "RAI",
                            Name = "Kimi Raikonnen",
                            Nationality = "Finnish",
                            Number = "14",
                            TeamId = 2
                        },
                        new
                        {
                            Id = 5,
                            Code = "RUS",
                            Name = "Gerogre Russel",
                            Nationality = "English",
                            Number = "19",
                            TeamId = 5
                        },
                        new
                        {
                            Id = 6,
                            Code = "LAT",
                            Name = "Nicholas Latifi",
                            Nationality = "Canadian",
                            Number = "20",
                            TeamId = 5
                        },
                        new
                        {
                            Id = 7,
                            Code = "NOR",
                            Name = "Lando Norris",
                            Nationality = "Italian",
                            Number = "24",
                            TeamId = 3
                        },
                        new
                        {
                            Id = 8,
                            Code = "SAI",
                            Name = "Carlos Sainz",
                            Nationality = "Spanish",
                            Number = "25",
                            TeamId = 3
                        },
                        new
                        {
                            Id = 9,
                            Code = "RIC",
                            Name = "Daniel Ricciardo",
                            Nationality = "Australian",
                            Number = "32",
                            TeamId = 4
                        },
                        new
                        {
                            Id = 10,
                            Code = "GAS",
                            Name = "Pierre Gasly",
                            Nationality = "French",
                            Number = "29",
                            TeamId = 6
                        },
                        new
                        {
                            Id = 11,
                            Code = "KVY",
                            Name = "Daniil Kvyat",
                            Nationality = "Russian",
                            Number = "31",
                            TeamId = 6
                        },
                        new
                        {
                            Id = 12,
                            Code = "VET",
                            Name = "Sebastian Vettel",
                            Nationality = "German",
                            Number = "30",
                            TeamId = 1
                        },
                        new
                        {
                            Id = 13,
                            Code = "SCH",
                            Name = "Michael Schumacher",
                            Nationality = "German",
                            Number = "33",
                            TeamId = 1
                        },
                        new
                        {
                            Id = 24,
                            Code = "BOT",
                            Name = "Walteri Bottas",
                            Nationality = "Finnish",
                            Number = "70",
                            TeamId = 2
                        });
                });

            modelBuilder.Entity("Race.Model.Models.Result", b =>
                {
                    b.Property<int>("ResultId");

                    b.Property<Guid>("PilotId");

                    b.Property<int>("RaceId");

                    b.HasKey("ResultId");

                    b.ToTable("Result");
                });

            modelBuilder.Entity("Race.Model.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChampionShipPoints");

                    b.Property<DateTime>("DateOfFoundation");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OwnerName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChampionShipPoints = 20,
                            DateOfFoundation = new DateTime(1950, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ferrari",
                            OwnerName = "Mattia Binotto"
                        },
                        new
                        {
                            Id = 2,
                            ChampionShipPoints = 30,
                            DateOfFoundation = new DateTime(1970, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Mercedes",
                            OwnerName = "Toto Wolff"
                        },
                        new
                        {
                            Id = 3,
                            ChampionShipPoints = 80,
                            DateOfFoundation = new DateTime(1960, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "McLaren",
                            OwnerName = "Andreas Seidl"
                        },
                        new
                        {
                            Id = 4,
                            ChampionShipPoints = 20,
                            DateOfFoundation = new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Reanult",
                            OwnerName = "Cyril Abiteboul"
                        },
                        new
                        {
                            Id = 5,
                            ChampionShipPoints = 5,
                            DateOfFoundation = new DateTime(1990, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Williams Racing",
                            OwnerName = "Frank Williams"
                        },
                        new
                        {
                            Id = 6,
                            ChampionShipPoints = 3,
                            DateOfFoundation = new DateTime(1990, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Alpha Tauri",
                            OwnerName = "Franz Tost"
                        });
                });

            modelBuilder.Entity("Race.Model.Models.Pilot", b =>
                {
                    b.HasOne("Race.Model.Models.Team", "Team")
                        .WithMany("Pilots")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
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
