using System.Collections.Concurrent;
using System.IO;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace DriverParser.Logging.Log4Net
{
    public class Log4NetProvider : ILoggerProvider
    {
        private readonly string _configFile;
        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers;

        public Log4NetProvider(string configFile)
        {
            _configFile = configFile;
            _loggers = new ConcurrentDictionary<string, Log4NetLogger>();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, CreateLoggerImplementation);
        }

        public void Dispose()
        {
            _loggers?.Clear();
        }

        private Log4NetLogger CreateLoggerImplementation(string name)
        {
            return new Log4NetLogger(name, ParseLog4NetConfigFile(_configFile));
        }

        private static XmlElement ParseLog4NetConfigFile(string filename)
        {
            var document = new XmlDocument();
            document.Load(File.OpenRead(filename));
            return document["log4net"];
        }
    }
}
