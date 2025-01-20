using System;

namespace StockPortfolioAPI.Models.POCO
{
    /// <summary>
    /// Represents historical price data for a stock.
    /// </summary>
    public class STP01
    {
        /// <summary>
        /// Unique identifier for each historical stock price entry.
        /// </summary>
        public int P01F01 { get; set; }

        /// <summary>
        /// Foreign key referencing the stock for which the price is being recorded.
        /// </summary>
        public int P01F02 { get; set; }

        /// <summary>
        /// The price of the stock at the given date.
        /// </summary>
        public decimal P01F03 { get; set; }

        /// <summary>
        /// The date on which the stock price was recorded.
        /// </summary>
        public DateTime P01F04 { get; set; }
    }
}
