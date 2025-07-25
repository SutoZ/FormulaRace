﻿using AutoMapper;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;
using TeamManagementService.API.Extensions;
using TeamManagementService.API.Monitoring;
using TeamManagementService.Application.Mappings;
using TeamManagementService.Application.Middleware;
using TeamManagementService.Application.Validators;
using TeamManagementService.Infrastructure.ApplicationContext;
using TeamManagementService.Infrastructure.Interceptors;

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


var dbProvider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "SqlServer";
var conn = builder.Configuration.GetConnectionString("RaceConnection");

ArgumentException.ThrowIfNullOrEmpty(conn);

switch (dbProvider.ToLowerInvariant())
{
    case "sqlserver":
        builder.Services.AddDbContext<RaceContext>((serviceProvider, options) =>
            options.UseSqlServer(conn, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
                sqlOptions.MigrationsAssembly("TeamManagementService.Infrastructure");
            })
                   .AddInterceptors(new SoftDeleteInterceptor(serviceProvider.GetRequiredService<ILogger<SoftDeleteInterceptor>>()))
                   .AddInterceptors(new AuditableInterceptor(serviceProvider.GetRequiredService<ILogger<AuditableInterceptor>>()))
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                   .EnableDetailedErrors()
                   .EnableSensitiveDataLogging());

        builder.Services.AddHealthChecks().AddSqlServer(conn!);
        break;
    case "postgres":
        builder.Services.AddDbContext<RaceContext>((serviceProvider, options) =>
            options.UseNpgsql(conn, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure();
                npgsqlOptions.MigrationsAssembly("TeamManagementService.Infrastructure");
            })
                   .AddInterceptors(new SoftDeleteInterceptor(serviceProvider.GetRequiredService<ILogger<SoftDeleteInterceptor>>()))
                   .AddInterceptors(new AuditableInterceptor(serviceProvider.GetRequiredService<ILogger<AuditableInterceptor>>()))
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                   .EnableDetailedErrors()
                   .EnableSensitiveDataLogging());

        builder.Services.AddHealthChecks().AddNpgSql(conn!);
        break;
    default:
        throw new NotSupportedException($"Database provider '{dbProvider}' is not supported.");
}


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

//builder.Services.AddDatabaseContext(builder.Configuration);

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var db = services.GetRequiredService<RaceContext>();

    try
    {
        logger.LogInformation("Attempting to apply database migrations...");

        var stopWatch = System.Diagnostics.Stopwatch.StartNew();
        var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
        var migrationsCount = pendingMigrations.Count();

        if (pendingMigrations.Any())
        {
            logger.LogInformation("Pending migrations found: {Count}. Applying migrations...{Migrations}", migrationsCount, pendingMigrations);

            var retryPolicy = Policy
                .Handle<SqlException>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        logger.LogWarning(exception, "Error connecting to database. Retrying in {timeSpan}. Attempt {retryCount}", timeSpan, retryCount);
                    });

            await retryPolicy.ExecuteAsync(async () =>
            {
                await db.Database.MigrateAsync();
                logger.LogInformation("Database migrations applied successfully.");
            });
        }
        else
        {
            logger.LogInformation("Database schema is up-to-date. No migrations to apply.");
        }

        stopWatch.Stop();
        logger.LogInformation("Database migration completed in {ElapsedMilliseconds} ms.", stopWatch.ElapsedMilliseconds);

        DatabaseMetrics.RecordMigration(stopWatch.ElapsedMilliseconds, migrationsCount);

        logger.LogInformation("Attempting to seed data...");
        await SeedData.SeedAllAsync(db, logger);
        logger.LogInformation("Data seeding completed successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database migration or seeding.");
    }
    finally
    {
        await Log.CloseAndFlushAsync();
    }
}

await app.RunAsync();
