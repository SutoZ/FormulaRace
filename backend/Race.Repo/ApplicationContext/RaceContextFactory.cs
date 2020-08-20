using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Race.Repo.ApplicationContext
{
    public class RaceContextFactory : IDesignTimeDbContextFactory<RaceContext>
    {
        public RaceContextFactory()
        {

        }
        public RaceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RaceContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Race;Integrated Security=false;");
            optionsBuilder.EnableSensitiveDataLogging();
            return new RaceContext(optionsBuilder.Options);
        }
    }
}
