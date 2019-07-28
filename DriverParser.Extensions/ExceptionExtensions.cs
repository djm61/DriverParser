using System;
using System.Text;

namespace DriverParser.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionMessage(this Exception exception)
        {
            var sb = new StringBuilder();
            sb.AppendLine(exception.Message);
            sb.AppendLine(exception.StackTrace);

            if (exception.InnerException != null)
            {
                var line = GetExceptionMessage(exception.InnerException);
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
