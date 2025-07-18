using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TeamManagementService.Infrastructure.ApplicationContext;

// used for design-time database context creation for migrations DO NOT DELETE!!!
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RaceContext>
{
    public RaceContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<RaceContext>();
        var connectionString = configuration.GetConnectionString("RaceConnection");

        builder.UseSqlServer(connectionString);

        return new RaceContext(builder.Options);
    }
}