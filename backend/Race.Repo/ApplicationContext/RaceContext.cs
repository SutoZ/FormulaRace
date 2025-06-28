using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Race.Model.Models;
using Race.Model.Seed.Pilots;
using Race.Model.Seed.Teams;
using Race.Repo.EntityConfig;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Repo.ApplicationContext;

public class RaceContext(DbContextOptions<RaceContext> context) : IdentityDbContext<ApplicationUser>(context)
{
    public virtual DbSet<Pilot> Pilots { get; set; }
    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PilotConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());

        SeedInitialDatas(modelBuilder);
    }

    private static void SeedInitialDatas(ModelBuilder modelBuilder)
    {
        //use reflection to get all seed classes in the assembly
        modelBuilder.Entity<Pilot>().HasData(new PilotSeed().Entities);
        modelBuilder.Entity<Team>().HasData(new TeamSeed().Entities);
    }
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("RaceConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}