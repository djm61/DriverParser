using System;
using System.IO;
using System.Windows.Forms;

using DriverParser.Service;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DriverParser.Desktop
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<ILoggerFactory>();

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(Program));

            logger.LogDebug("Getting Form");
            var form = serviceProvider.GetService<frmMain>();
            Application.Run(form);
            logger.LogDebug("Success!");
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            serviceCollection.AddSingleton(configuration);

            serviceCollection.Configure<DriverParserSettings>(configuration.GetSection("DriverParserSettings"));//

            serviceCollection.AddLogging();

            serviceCollection.AddScoped<IService, Service.Service>();

            serviceCollection.AddScoped<frmMain>();
        }
    }
}
