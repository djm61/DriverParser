using System;
using System.Runtime.Serialization;

namespace DriverParser.Logging
{
    [Serializable]
    public class LoggingConfigurationException : Exception
    {
        public LoggingConfigurationException(string message)
            : this(message, string.Empty)
        {
        }

        public LoggingConfigurationException(string message, string loggerType)
            : base(message)
        {
            LoggerType = loggerType ?? string.Empty;
        }

        protected LoggingConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string LoggerType { get; private set; }
    }
}
