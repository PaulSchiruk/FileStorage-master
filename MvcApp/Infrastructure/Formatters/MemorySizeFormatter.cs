using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MvcApp.Infrastructure.Formatters
{
    /// <summary>
    /// Formats numbers to memory size format
    /// </summary>
    /// <seealso cref="System.IFormatProvider" />
    /// <seealso cref="System.ICustomFormatter" />
    public class MemorySizeFormatter : IFormatProvider, ICustomFormatter
    {
        #region Fields

        private static readonly string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        private static readonly string formatTemplate = "{0}{1:0.#} {2}";
        #endregion

        #region Public methods
        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return.</param>
        /// <returns>
        /// An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, null.
        /// </returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="arg">An object to format.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>
        /// The string representation of the value of <paramref name="arg" />, formatted as specified by <paramref name="format" /> and <paramref name="formatProvider" />.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null) throw new ArgumentNullException("arg");
            return PerformFormat(format, arg, formatProvider);
        }
        #endregion

        #region Private methods

        private string PerformFormat(string format, object arg, IFormatProvider formatProvider)
        {
            if (format.ToLower() == "memory")
            {
                return GetMemorySizeRepresentOfNumber(arg);
            }
            else
            {
                return HandleOtherFormats(format, arg);
            }
        }

        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
            {
                try
                {
                    return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
                }
                catch (FormatException e)
                {
                    throw new FormatException(String.Format("The format of '{0}' is invalid.", format), e);
                }
            }
            else if (arg != null)
                return arg.ToString();
            else
                return String.Empty;
        }

        private string GetMemorySizeRepresentOfNumber(object arg)
        {
            if (!(arg is int)) throw new ArgumentException("Wrong size provided");
            int size = (int)arg;

            if (size == 0)
            {
                return string.Format(formatTemplate, null, 0, sizeSuffixes[0]);
            }

            var absSize = Math.Abs((double)size);
            var fpPower = Math.Log(absSize, 1024);
            var intPower = (int)fpPower;
            var iUnit = intPower >= sizeSuffixes.Length
                ? sizeSuffixes.Length - 1
                : intPower;
            var normSize = absSize / Math.Pow(1024, iUnit);

            return string.Format(
                formatTemplate,
                size < 0 ? "-" : null, normSize, sizeSuffixes[iUnit]);

        }

        #endregion
    }
}