// <copyright file="Parse.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.YahooFinance
{
    using CsvHelper;

    /// <summary>
    /// For parsing data.
    /// </summary>
    internal static class Parse
    {
        /// <summary>
        /// Parse a string into a double.  Value of 0 if invalid data.
        /// </summary>
        /// <param name="csv">CsvReader.</param>
        /// <param name="index">Column index.</param>
        /// <returns>double value.</returns>
        internal static double? GetDouble(this CsvReader csv, int index)
        {
            return double.TryParse(csv[index], out double value) ? value : null;
        }

        /// <summary>
        /// Parse a string into a long.  Value of 0 if invalid data.
        /// </summary>
        /// <param name="csv">CsvReader.</param>
        /// <param name="index">Column index.</param>
        /// <returns>double value.</returns>
        internal static long? GetLong(this CsvReader csv, int index)
        {
            return long.TryParse(csv[index], out long value) ? value : null;
        }
    }
}