using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TeamManagementService.Infrastructure.ApplicationContext;
using TeamManagementService.API;
using Testcontainers.MsSql;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace TeamManagement.Application.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncDisposable
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
    .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
    .WithPassword("Nagyon_Nemmindegy98*?.-")
    .Build();

    public HttpClient Client => CreateClient();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RaceContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public override async ValueTask DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();

        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        // Ensure the connection string is available in IConfiguration for other services if needed.
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ConnectionStrings:RaceConnection"] = _dbContainer.GetConnectionString()
            });
        });

        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<RaceContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<RaceContext>(options =>
                options.UseSqlServer(_dbContainer.GetConnectionString()), ServiceLifetime.Scoped);

            // Also, ensure the connection string is available in IConfiguration for other services if needed.
        
        });
    }
}