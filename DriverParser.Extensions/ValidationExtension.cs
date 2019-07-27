using System;

namespace DriverParser.Extensions
{
    public static class ValidationExtensions
    {
        public static T ThrowIfNull<T>(this T parameter) where T : class
        {
            return parameter.ThrowIfNull<T>(nameof(parameter));
        }

        public static T ThrowIfNull<T>(this T parameter, string parameterName) where T : class
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return parameter;
        }

        public static string ThrowIfNullOrEmpty(this string parameter)
        {
            return parameter.ThrowIfNullOrEmpty(nameof(parameter));
        }

        public static string ThrowIfNullOrEmpty(this string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentOutOfRangeException(parameterName, "Argument cannot be an empty string");
            }

            return parameter;
        }

        public static string ThrowIfNullOrEmptyOrWhitespace(this string parameter)
        {
            return parameter.ThrowIfNullOrEmptyOrWhitespace(nameof(parameter));
        }

        public static string ThrowIfNullOrEmptyOrWhitespace(this string parameter, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentOutOfRangeException(parameterName, "Argument cannot be an all whitespace string");
            }

            return parameter;
        }

        public static int ThrowIfNotGreaterThan(this int parameter, int lowerBound)
        {
            return parameter.ThrowIfNotGreaterThan(lowerBound, nameof(parameter));
        }

        public static int ThrowIfNotGreaterThan(this int parameter, int lowerBound, string parameterName)
        {
            if (parameter <= lowerBound)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                    string.Format("Value must be greater than {0}", lowerBound));
            }

            return parameter;
        }

        public static long ThrowIfNotGreaterThan(this long parameter, long lowerBound)
        {
            return parameter.ThrowIfNotGreaterThan(lowerBound, nameof(parameter));
        }

        public static long ThrowIfNotGreaterThan(this long parameter, long lowerBound, string parameterName)
        {
            if (parameter <= lowerBound)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                    string.Format("Value must be greater than {0}", lowerBound));
            }

            return parameter;
        }

        public static int ThrowIfNotLessThan(this int parameter, int upperBound)
        {
            return parameter.ThrowIfNotLessThan(upperBound, nameof(parameter));
        }

        public static int ThrowIfNotLessThan(this int parameter, int upperBound, string parameterName)
        {
            if (parameter >= upperBound)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                    string.Format("Value must be less than {0}", upperBound));
            }

            return parameter;
        }

        public static long ThrowIfNotLessThan(this long parameter, long lowerBound)
        {
            return parameter.ThrowIfNotLessThan(lowerBound, nameof(parameter));
        }

        public static long ThrowIfNotLessThan(this long parameter, long lowerBound, string parameterName)
        {
            if (parameter <= lowerBound)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                    string.Format("Value must be greater than {0}", lowerBound));
            }

            return parameter;
        }

        public static int ThrowIfNotInRange(this int parameter, int lowerBound, int upperBound)
        {
            return parameter.ThrowIfNotInRange(lowerBound, upperBound, nameof(parameter));
        }

        public static int ThrowIfNotInRange(this int parameter, int lowerBound, int upperBound, string parameterName)
        {
            if (parameter < lowerBound || parameter > upperBound)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                    string.Format("Value must be in the range {0} to {1}", lowerBound, upperBound));
            }

            return parameter;
        }

        public static long ThrowIfNotInRange(this long parameter, long lowerBound, long upperBound)
        {
            return parameter.ThrowIfNotInRange(lowerBound, upperBound, nameof(parameter));
        }

        public static long ThrowIfNotInRange(this long parameter, long lowerBound, long upperBound, string parameterName)
        {
            if (parameter < lowerBound || parameter > upperBound)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                    string.Format("Value must be in the range {0} to {1}", lowerBound, upperBound));
            }

            return parameter;
        }
    }
}
