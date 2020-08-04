using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Race.Repo.ApplicationContext;
using System.IO;

namespace Race.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateWebHostBuilder(args).Build();

               using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                  //  var context = services.GetRequiredService<RaceContext>();

                    DataGenerator.Initialize(services);
                }

                host.Run();

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseContentRoot(Directory.GetCurrentDirectory());

    }
}
