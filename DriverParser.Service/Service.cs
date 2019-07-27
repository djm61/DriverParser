using System.IO;
using DriverParser.Data;
using DriverParser.Extensions;
using DriverParser.Model;
using DriverParser.Service.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DriverParser.Service
{
    public class Service : IService
    {
        private readonly ILogger<Service> _logger;
        private readonly DriverParserContext _context;
        private readonly string _filePath;
        
        public string StatusMessage { get; set; }

        public Service(ILoggerFactory loggerFactory, DriverParserContext context, IOptions<DriverParserSettings> settings)
        {
            _logger = loggerFactory.ThrowIfNull(nameof(loggerFactory)).CreateLogger<Service>();
            _context = context.ThrowIfNull(nameof(context));
            _filePath = settings
                .ThrowIfNull(nameof(settings))
                .Value.ThrowIfNull(nameof(settings.Value))
                .FilePath
                .ThrowIfNullOrEmptyOrWhitespace(nameof(settings.Value.FilePath));

            StatusMessage = string.Empty;

            _logger.LogDebug("Service created");
        }

        public void ParseFile()
        {
            _logger.LogDebug($"ParseFile: Parsing file [{_filePath}]");
            if (string.IsNullOrWhiteSpace(_filePath))
            {
                _logger.LogError("ParseFile: File name is blank or empty.  Cannot continue.");
                StatusMessage = "Filename is blank or empty.  Cannot continue.";
                return;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _filePath);

            Result result = null;

            using (var sr = File.OpenText(filePath))
            {
                var serializer = new JsonSerializer();
                result = (Result)serializer.Deserialize(sr, typeof(Result));
            }

            var entityResult = result.ConvertToEntity();
            _context.Add(entityResult);
            _context.SaveChanges();

            StatusMessage = "Successfully parsed file.";
        }
    }
}
