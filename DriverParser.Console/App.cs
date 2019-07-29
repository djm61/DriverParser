using System;
using DriverParser.Extensions;
using DriverParser.Service;
using Microsoft.Extensions.Logging;

namespace DriverParser.Console
{
    public class App
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<App> _logger;
        private readonly IService _service;

        public App(ILoggerFactory loggerFactory, IService service)
        {
            _loggerFactory = loggerFactory.ThrowIfNull(nameof(loggerFactory));
            _logger = loggerFactory.ThrowIfNull(nameof(loggerFactory)).CreateLogger<App>();
            _service = service.ThrowIfNull(nameof(service));
        }

        public void Run()
        {
            try
            {
                _logger.LogDebug("Running DriverParser");
                System.Console.WriteLine("Running DriverParser...");

                _service.ParseFile();

                System.Console.WriteLine($"Done Parsing File!  Status Message[{_service.StatusMessage}]");

                _service.ComputeResults();

                System.Console.WriteLine($"Done Computing Results!  Status Message[{_service.StatusMessage}]");

                _service.OutputResults();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Run: Error running[{ex.GetExceptionMessage()}]", ex);
                System.Console.WriteLine(ex.Message);
            }

            System.Console.ReadKey();
        }
    }
}
