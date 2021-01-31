using System;
using DriverParser.Extensions;
using DriverParser.Service;
using Microsoft.Extensions.Logging;

namespace DriverParser.Console
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly IService _service;

        public App(ILogger<App> logger, IService service)
        {
            _logger = logger.ThrowIfNull(nameof(logger));
            _service = service.ThrowIfNull(nameof(service));

            _logger.LogDebug("App created");
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
