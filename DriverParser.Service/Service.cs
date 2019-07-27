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
            _logger.LogDebug($"ParseFile: Opening file [{filePath}]");
            using (var sr = File.OpenText(filePath))
            {
                var serializer = new JsonSerializer();
                _logger.LogDebug("ParseFile: attempting to deserialize file");
                result = (Result)serializer.Deserialize(sr, typeof(Result));
            }

            if (result != null)
            {
                _logger.LogDebug("ParseFile: successfully deserialized file, attempting to convert to an entity so it can be saved");
                var entityResult = result.ConvertToEntity();
                _context.Add(entityResult);
                var saveResult = _context.SaveChanges();
                _logger.LogDebug($"ParseFile: save result [{saveResult}]");

                StatusMessage = "Successfully parsed file.";
            }
            else
            {
                _logger.LogWarning("ParseFile: unable to deserialize file");
                StatusMessage = "Unable to parse file";
            }
        }
    }
}
