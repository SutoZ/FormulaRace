using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
using System;

namespace Race.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.isDevelopment = env.IsDevelopment();
        }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private bool isDevelopment { get; set; }

        private int passwordLength = 8;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

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

            services.AddHealthChecks();

            services.AddScoped(typeof(IPilotRepository), typeof(PilotRepository));
            services.AddScoped(typeof(ITeamRepository), typeof(TeamRepository));

            //services.AddScoped(typeof(IRaceContext), typeof(RaceContext));
            
            services.AddScoped(typeof(ITeamService), typeof(TeamService));
            services.AddScoped(typeof(IPilotService), typeof(PilotService));

            AddDbContext(services, isDevelopment);

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = passwordLength;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddRoles<IdentityRole>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<RaceContext>();

            services.AddIdentityServer().AddApiAuthorization<ApplicationUser, RaceContext>();

            services.AddAuthorization();
            services.AddSpaStaticFiles(spa => spa.RootPath = "racefrontend");

            services.AddSwaggerGen();
        }

        protected virtual void AddDbContext(IServiceCollection services, bool isDevelopment = false)
        {
            services.AddDbContext<RaceContext>(options =>
            {
                if (isDevelopment)
                {
                    options.UseSqlServer(Configuration.GetConnectionString("RaceConnection"));
                    options.EnableSensitiveDataLogging();
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();


            //addig swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(
                setup =>
                {
                    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Race API");
                    setup.RoutePrefix = string.Empty;
                });

            app.UseCors("AllowCredentials");

            app.UseRouting();

            app.UseEndpoints(conf =>
            {
                conf.MapRazorPages();
            });

            app.UseEndpoints(conf =>
            {
                conf.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                conf.MapRazorPages();
            });
        }
    }
}
