using DriverParser.Logging.Log4Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DriverParser.Logging
{
    public static class LoggingExtensions
    {
        private const string DefaultLog4NetConfigFile = "log4net.config";

        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider>(provider => new Log4NetProvider(DefaultLog4NetConfigFile));
            return builder;
        }

        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, string log4NetConfigFile)
        {
            builder.Services.AddSingleton<ILoggerProvider>(provider => new Log4NetProvider(log4NetConfigFile));
            return builder;
        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string log4NetConfigFile)
        {
            factory.AddProvider(new Log4NetProvider(log4NetConfigFile));
            return factory;
        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory)
        {
            factory.AddLog4Net(DefaultLog4NetConfigFile);
            return factory;
        }
    }
}
