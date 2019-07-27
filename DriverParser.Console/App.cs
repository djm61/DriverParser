using DriverParser.Data;
using DriverParser.Extensions;
using DriverParser.Service;
using Microsoft.Extensions.Logging;

namespace DriverParser.Console
{
    public class App
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<App> _logger;
        private readonly DbService _dbService;
        private readonly IService _service;

        public App(ILoggerFactory loggerFactory, DbService dbService, IService service)
        {
            _loggerFactory = loggerFactory.ThrowIfNull(nameof(loggerFactory));
            _logger = loggerFactory.ThrowIfNull(nameof(loggerFactory)).CreateLogger<App>();
            _dbService = dbService.ThrowIfNull(nameof(dbService));
            _service = service.ThrowIfNull(nameof(service));
        }

        public void Run()
        {
            _dbService.PerformDbUpdate();

            _logger.LogDebug("Running DriverParser");
            System.Console.WriteLine("Running DriverParser...");

            _service.ParseFile();

            System.Console.WriteLine($"Done!  Status Message[{_service.StatusMessage}]");
            System.Console.ReadKey();
        }
    }
}
