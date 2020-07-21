using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Race.Repo.ApplicationContext
{
    public class RaceContextDesignFactory : IDesignTimeDbContextFactory<RaceContext>
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Race;Integrated Security=false;";

        public IConfiguration Configuration { get; }
        public RaceContextDesignFactory()
        {

        }
        public RaceContextDesignFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public RaceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RaceContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RaceContext(optionsBuilder.Options);
        }
    }
}
