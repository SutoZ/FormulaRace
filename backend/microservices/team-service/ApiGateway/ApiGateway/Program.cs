using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Load the ocelot.json configuration file
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot services to the dependency injection container
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Add the Ocelot middleware to the pipeline
await app.UseOcelot();

app.Run();