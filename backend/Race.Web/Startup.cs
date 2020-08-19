using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Race.Repo.ApplicationContext;
using Race.Repo.Interfaces;
using Race.Repo.Repositories;
using Race.Service.Interfaces;
using Race.Service.Services;

namespace Race.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(setup =>
            {
                setup.AddPolicy(name: "AllowCredentials", builder =>
                {
                    builder
                    .WithOrigins(Configuration.GetSection("Site").GetSection("ClientUrl").Value)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            services.AddScoped(typeof(IPilotRepository), typeof(PilotRepository));
            services.AddScoped(typeof(ITeamRepository), typeof(TeamRepository));

            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IPilotService, PilotService>();

            services.AddDbContext<RaceContext>(options => options.UseSqlServer(Configuration.GetConnectionString("RaceConnection")));

            services.AddSpaStaticFiles(spa => spa.RootPath = "racefrontend");

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //addig swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(
                setup =>
                {
                    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Race API");
                    setup.RoutePrefix = string.Empty;
                });

            app.UseCors("AllowCredentials");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
