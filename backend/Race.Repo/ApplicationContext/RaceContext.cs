using Microsoft.EntityFrameworkCore;
using Race.Model.Models;
using Race.Model.Seed.Pilots;
using Race.Repo.EntityConfig;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Repo.ApplicationContext
{
    public class RaceContext : DbContext, IRaceContext
    {
        public RaceContext(DbContextOptions<RaceContext> context) : base(context)
        {
        }

        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Result> Results { get; set; }
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
            modelBuilder.Entity<Team>().HasData(new TeamsSeed().Entities);
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
