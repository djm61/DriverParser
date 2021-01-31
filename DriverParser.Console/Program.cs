using System.IO;

using DriverParser.Service;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DriverParser.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<ILoggerFactory>()
                //.AddConsole()
                //.AddDebug()
                //.AddLog4Net()
                ;

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Getting Service");
            var app = serviceProvider.GetService<App>();
            app.Run();
            logger.LogDebug("Success!");
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                //.AddEnvironmentVariables()
                .Build();

            serviceCollection.AddSingleton(configuration);

            //var connString = configuration.GetConnectionString("Default");
            //serviceCollection
            //    .AddEntityFrameworkSqlite()
            //    .AddDbContext<DriverParserContext>(options =>
            //    options.UseSqlite(connString)
            //        .EnableDetailedErrors()
            //        .EnableSensitiveDataLogging()
            //    );

            serviceCollection.Configure<DriverParserSettings>(configuration.GetSection("DriverParserSettings"));

            serviceCollection.AddLogging();

            //serviceCollection.AddTransient<DbService>();
            serviceCollection.AddTransient<IService, Service.Service>();

            serviceCollection.AddTransient<App>();
        }
    }
}
