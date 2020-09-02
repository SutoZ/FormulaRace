using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Race.Repo.ApplicationContext;
using Race.Web;
using System;

namespace Race.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var serviceProvirder = app.ApplicationServices;
            var db = serviceProvirder.GetRequiredService<RaceContext>();

            var logger = serviceProvirder.GetRequiredService<ILogger<RaceWebApplicationFactory>>();

            db.Database.OpenConnection();
            db.Database.EnsureCreated();
            try
            {
                AddTestData(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

            base.Configure(app, env);
        }

        public virtual void AddTestData(RaceContext db)
        {
            TestDataSeed.SeedData(db);
        }

        protected override void AddDbContext(IServiceCollection services, bool isDevelopment = false)
        {
            services.AddDbContext<RaceContext>(options =>
            {
                if (isDevelopment)
                {
                    options.UseInMemoryDatabase(Configuration.GetConnectionString("RaceConnection"));
                    options.EnableSensitiveDataLogging();
                }
            });
        }
    }
}
