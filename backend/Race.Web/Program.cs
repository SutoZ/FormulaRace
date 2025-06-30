using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Race.Model.Models;
using Race.Repo.ApplicationContext;
using Race.Repo.Interfaces;
using Race.Repo.Repositories;
using Race.Service.Interfaces;
using Race.Service.Services;
using Race.Web.Mappings;
using Race.Web.Middleware;
using Serilog;
using System;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .CreateLogger();

Log.Information("Loading configuration...");

try
{
    var builder = WebApplication.CreateBuilder(args);
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Host.UseSerilog();
    builder.WebHost.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://+:8080");

    // Add services to the container.
    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
    });


    builder.Services.AddHealthChecks();

    builder.Services.AddControllersWithViews();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(setup =>
    {
        setup.AddPolicy(name: "AllowCredentials", policy =>
        {
            policy
                .WithOrigins(builder.Configuration.GetSection("Site:ClientUrl").Value)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

    builder.Services.AddSingleton(Log.Logger);

    builder.Services.AddScoped<IPilotRepository, PilotRepository>();
    builder.Services.AddScoped<ITeamRepository, TeamRepository>();
    builder.Services.AddScoped<ITeamService, TeamService>();
    builder.Services.AddScoped<IPilotService, PilotService>();

    builder.Services.AddDbContext<RaceContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("RaceConnection"));
        options.EnableSensitiveDataLogging();
    });

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

    builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedEmail = true;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<RaceContext>();

    builder.Services.AddAuthorization();

    builder.Services.AddSpaStaticFiles(spa => spa.RootPath = "frontend");

    builder.Services.AddSession();

    builder.Services.AddExceptionHandler<DBUpdateExceptionHandler>();
    builder.Services.AddSingleton<IExceptionHandler, ExceptionHandler>();
    builder.Services.AddExceptionHandler<ExceptionHandler>();

    builder.Services.AddApiVersioning(options =>
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
        app.UseSwaggerUI(setup =>
        {
            setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Race API v1");
            setup.RoutePrefix = string.Empty;
        });
    }
    else
    {
        app.UseExceptionHandler();
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
    app.UseCors("AllowCredentials");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    await app.RunAsync();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly!");
}
finally
{
    await Log.CloseAndFlushAsync();
}
