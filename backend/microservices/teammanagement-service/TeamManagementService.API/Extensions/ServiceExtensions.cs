using Asp.Versioning;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Race.Shared.Utilities.Paging;
using System.Text;
using TeamManagementService.Application.CQRS.Pilots.Commands;
using TeamManagementService.Application.Interfaces.Repositories;
using TeamManagementService.Application.Interfaces.Services;
using TeamManagementService.Application.Middleware;
using TeamManagementService.Application.Services;
using TeamManagementService.Domain.Models;
using TeamManagementService.Infrastructure.ApplicationContext;
using TeamManagementService.Infrastructure.Interceptors;
using TeamManagementService.Infrastructure.Repositories;

namespace TeamManagementService.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        // Register custom services here
        services.AddScoped<IPilotRepository, PilotRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IPilotService, PilotService>();
        services.AddScoped<SoftDeleteInterceptor>();
        services.AddScoped<AuditableInterceptor>();
        services.AddScoped(typeof(IPagedList<>), typeof(PagedList<>));

        services.AddHttpContextAccessor();
        services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }

    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        // Register Identity services
        services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
        }).AddRoles<IdentityRole>()
          .AddEntityFrameworkStores<RaceContext>();

        return services;
    }

    public static IServiceCollection AddSingletonServices(this IServiceCollection services)
    {
        // Register custom services here
        services.AddSingleton<IExceptionHandler, ExceptionHandler>();

        return services;
    }

    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        // Register MediatR services
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); // Register current assembly
            cfg.RegisterServicesFromAssembly(typeof(CreatePilotCommand).Assembly); // Register CreatePilotCommand assembly
        });

        return services;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext with SQL Server
        services.AddDbContext<RaceContext>((serviceProvider, options) =>
        {
            string conn = new StringBuilder(configuration.GetConnectionString("RaceConnection")).ToString();

            ArgumentException.ThrowIfNullOrEmpty(conn, nameof(conn));

            options.UseSqlServer(conn)
                   .AddInterceptors(new SoftDeleteInterceptor(serviceProvider.GetRequiredService<ILogger<SoftDeleteInterceptor>>()))
                   .AddInterceptors(new AuditableInterceptor(serviceProvider.GetRequiredService<ILogger<AuditableInterceptor>>()))
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                   .EnableDetailedErrors()
                   .EnableSensitiveDataLogging();
        });

        return services;
    }

    public static IServiceCollection AddWebApiVersioning(this IServiceCollection services)
    {
        // Register API versioning
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new UrlSegmentApiVersionReader()
            );
        });

        return services;
    }
}
