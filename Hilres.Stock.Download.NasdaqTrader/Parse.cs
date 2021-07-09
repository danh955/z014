// <copyright file="Parse.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Download.NasdaqTrader
{
    using System;

    /// <summary>
    /// Data parser helper class.
    /// </summary>
    internal static class Parse
    {
        /// <summary>
        /// Get a boolean value.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <returns>True if the value is "Y" or "y".</returns>
        internal static bool IsTrue(string value)
        {
            return value == "Y" || value == "y";
        }

        /// <summary>
        /// Get the NASDAQ date time.
        /// MMDDYYYYHH:MM = 0114202117:02.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <returns>DateTime.</returns>
        internal static DateTime FileCreationTime(string value)
        {
            value = value.Trim();
            return new DateTime(value.IntSub(4, 4), value.IntSub(0, 2), value.IntSub(2, 2), value.IntSub(8, 2), value.IntSub(11, 2), 0);
        }

        private static int IntSub(this string value, int start, int length)
        {
            return int.Parse(value.Substring(start, length));
        }
    }
}