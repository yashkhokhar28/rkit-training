using System;

namespace StockPortfolioAPI.Models.POCO
{
    /// <summary>
    /// Represents a stock in the system.
    /// </summary>
    public class STK01
    {
        /// <summary>
        /// Unique identifier for each stock.
        /// </summary>
        public int K01F01 { get; set; }

        /// <summary>
        /// The stock ticker symbol (e.g., AAPL for Apple).
        /// </summary>
        public string K01F02 { get; set; }

        /// <summary>
        /// The full name of the stock (e.g., Apple Inc.).
        /// </summary>
        public string K01F03 { get; set; }

        /// <summary>
        /// The current price of the stock.
        /// </summary>
        public decimal K01F04 { get; set; }

        /// <summary>
        /// The market capitalization of the stock (optional).
        /// </summary>
        public decimal? K01F05 { get; set; }

        /// <summary>
        /// Timestamp when the stock was added.
        /// </summary>
        public DateTime K01F06 { get; set; }

        /// <summary>
        /// Timestamp when the stock details were last updated.
        /// </summary>
        public DateTime K01F07 { get; set; }
    }
}