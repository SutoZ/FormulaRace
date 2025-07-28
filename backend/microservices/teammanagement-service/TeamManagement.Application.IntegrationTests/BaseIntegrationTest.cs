using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using TeamManagementService.Infrastructure.ApplicationContext;

namespace TeamManagement.Application.IntegrationTests;

public abstract class BaseIntegrationTest
{
    private static CustomWebApplicationFactory _factory = null;
    protected IServiceScope _scope = null;
    private static Respawner _respawner = null;
    protected static HttpClient _client = null;
    protected RaceContext _dbContext = null;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _factory = new CustomWebApplicationFactory();
        await _factory.InitializeAsync();
        _client = _factory.Client;

        // Create a single Respawner instance with an explicitly opened connection.
        var connectionString = _factory.Services.GetRequiredService<IConfiguration>().GetConnectionString("RaceConnection");

        using var conn = new SqlConnection(connectionString!);
        await conn.OpenAsync();

        _respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            SchemasToInclude = new[] { "dbo" }
        });
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.DisposeAsync();
    }

    [SetUp]
    public async Task SetUp()
    {
        _scope = _factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<RaceContext>();

        // Reset the database before each test.
        var connectionString = _factory.Services.GetRequiredService<IConfiguration>().GetConnectionString("RaceConnection");
        await _respawner.ResetAsync(connectionString!);
    }

    [TearDown]
    public void TearDown()
    {
        _scope?.Dispose();
    }
}
