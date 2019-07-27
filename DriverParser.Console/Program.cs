using System.IO;
using System.Reflection;
using DriverParser.Data;
using DriverParser.Logging;
using DriverParser.Service;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DriverParser.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider
                .GetService<ILoggerFactory>()
                //.AddConsole()
                //.AddDebug()
                .AddLog4Net();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            var app = serviceProvider.GetService<App>();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var connString = configuration.GetConnectionString("Default");
            serviceCollection
                .AddEntityFrameworkSqlite()
                .AddDbContext<DriverParserContext>(options =>
                options.UseSqlite(connString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                );

            serviceCollection.Configure<DriverParserSettings>(configuration.GetSection("DriverParserSettings"));
            
            serviceCollection.AddLogging();

            serviceCollection.AddTransient<DbService>();
            serviceCollection.AddTransient<IService, Service.Service>();

            serviceCollection.AddTransient<App>();
        }
    }
}
