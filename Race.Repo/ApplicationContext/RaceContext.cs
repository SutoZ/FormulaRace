using Microsoft.EntityFrameworkCore;
using Race.Model.EntityMappers;
using Race.Model.Models;

namespace Race.Repo.ApplicationContext
{
    public class RaceContext : DbContext
    {
        public RaceContext(DbContextOptions<RaceContext> context) : base(context)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new EntitytypeConfiguration<>)        TODO
            modelBuilder.Entity<Pilot>().ToTable("Pilots");
            modelBuilder.Entity<Pilot>().Property(x => x.PilotId).ValueGeneratedNever();

            new PilotMap(modelBuilder.Entity<Pilot>());
            new ResultMap(modelBuilder.Entity<Result>());
        }

        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Result> Results { get; set; }
    }
}
