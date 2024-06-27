using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketProcessingApp.Data;
using TicketProcessor;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostContext, services) =>
               {
                   var configuration = hostContext.Configuration;
                   var connectionString = configuration.GetConnectionString("TicketConnectionStrings");
                   services.AddDbContext<AppDbContext>(options =>
                       options.UseSqlServer(connectionString, builder =>
                       {
                           builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(120), null);
                       }));
                   services.AddHostedService<TicketProcessorService>();
                   services.AddLogging(configure => configure.AddConsole());
               });

}