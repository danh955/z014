// <copyright file="StockPriceEntity.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Repository
{
    using System;

    /// <summary>
    /// Stock price entity class.
    /// </summary>
    public class StockPriceEntity
    {
        /// <summary>
        /// Gets or sets the date Period.
        /// </summary>
        public DateTime Period { get; set; }

        /// <summary>
        /// Gets or sets the open price.
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// Gets or sets the low price.
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Gets or sets the high price.
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Gets or sets the close price.
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// Gets or sets the adjusted open price.
        /// </summary>
        public double AdjOpen { get; set; }

        /// <summary>
        /// Gets or sets the adjusted low price.
        /// </summary>
        public double AdjLow { get; set; }

        /// <summary>
        /// Gets or sets the adjusted high price.
        /// </summary>
        public double AdjHigh { get; set; }

        /// <summary>
        /// Gets or sets the adjusted close price.
        /// </summary>
        public double AdjClose { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public long Volume { get; set; }
    }
}