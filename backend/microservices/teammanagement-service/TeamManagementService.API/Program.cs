using AutoMapper;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;
using TeamManagementService.API.Extensions;
using TeamManagementService.Application.Mappings;
using TeamManagementService.Application.Middleware;
using TeamManagementService.Application.Validators;
using TeamManagementService.Infrastructure.ApplicationContext;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .CreateLogger();

Log.Information("Loading configuration...");

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json")
    .Build())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddSingleton(Log.Logger);

Log.Information("Configuration loaded successfully.");

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

ArgumentException.ThrowIfNullOrEmpty(builder.Configuration.GetConnectionString("RaceConnection"));

var conn = builder.Configuration.GetConnectionString("RaceConnection");

builder.Services.AddHealthChecks()
    .AddSqlServer(conn!);

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(setup =>
{
    setup.AddPolicy(name: "AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddValidatorsFromAssembly(typeof(PilotDeleteValidator).Assembly);

builder.Services.AddScopedServices();
builder.Services.AddSingletonServices();

builder.Services.AddDatabaseContext(builder.Configuration);

builder.Services.AddMediatRServices();

builder.Services.AddIdentityService();

builder.Services.AddAuthorization();

builder.Services.AddSpaStaticFiles(spa => spa.RootPath = "frontend");

builder.Services.AddSession();

builder.Services.AddExceptionHandler<DBUpdateExceptionHandler>();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddWebApiVersioning();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(opt => { });
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthz");
app.UseSession();

app.UseRouting();
app.UseCors("AllowFrontend");

app.MapGet("/", () => "API is running!");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Apply migrations and seed data with a retry policy

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<RaceContext>();
await db.Database.MigrateAsync();
//await db.SeedAllAsync();


//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var logger = services.GetRequiredService<ILogger<Program>>();
//    var db = services.GetRequiredService<RaceContext>();

//    try
//    {
//        logger.LogInformation("Attempting to apply database migrations...");

//        var retryPolicy = Policy
//            .Handle<SqlException>()
//            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
//                (exception, timeSpan, retryCount, context) =>
//                {
//                    logger.LogWarning(exception, "Error connecting to database. Retrying in {timeSpan}. Attempt {retryCount}", timeSpan, retryCount);
//                });

//        await retryPolicy.ExecuteAsync(async () =>
//        {
//            await db.Database.MigrateAsync();
//            logger.LogInformation("Database migrations applied successfully.");
//        });

//        logger.LogInformation("Attempting to seed data...");
//        await db.SeedAllAsync();
//        logger.LogInformation("Data seeding completed successfully.");
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "An error occurred during database migration or seeding.");
//    }
//    finally
//    {
//        await Log.CloseAndFlushAsync();
//    }
//}

await app.RunAsync();
