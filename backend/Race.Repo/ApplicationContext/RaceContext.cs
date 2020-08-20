using Microsoft.EntityFrameworkCore;
using Race.Model.Models;
using Race.Model.Seed.Pilots;
using Race.Repo.EntityConfig;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Repo.ApplicationContext
{
    public class RaceContext : DbContext //, IRaceContext
    {
        public RaceContext(DbContextOptions<RaceContext> context) : base(context)
        {
        }

        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PilotConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());

            SeedInitialDatas(modelBuilder);
        }

        private void SeedInitialDatas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pilot>().HasData(new PilotSeed().Entities);
        /*    modelBuilder.Entity<Pilot>().OwnsOne(ent => ent.Team).HasData(new
            {
                Id = 2,
                ChampionShipPoints = 30,
                Name = "Mercedes",
                DateOfFoundation = new DateTime(1970, 04, 04),
                OwnerName = "Toto Wolff",
                Pilots = new List<Pilot>
                {
                     new Pilot
                {
                  Id = 1,
                  Code = "HAM",
                  Number = "44",
                  Name = "Lewis Hamilton",
                  TeamId = 2,
                  Nationality = "British"
                },
                     new Pilot
                {
                  Id = 20,
                  Code = "BOT",
                  Number = "70",
                  Name = "Walteri Bottas",
                  TeamId = 2,
                  Nationality = "Finnish"
                }
                }
            });
           */
            modelBuilder.Entity<Team>().HasData(new TeamSeed().Entities);
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
