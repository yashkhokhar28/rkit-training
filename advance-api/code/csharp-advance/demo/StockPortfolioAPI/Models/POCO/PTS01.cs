using System;

namespace StockPortfolioAPI.Models.POCO
{
    /// <summary>
    /// Represents a portfolio stock entry.
    /// </summary>
    public class PTS01
    {
        /// <summary>
        /// Unique identifier for each portfolio stock entry.
        /// </summary>
        public int S01F01 { get; set; }

        /// <summary>
        /// User ID (Foreign key to USR01).
        /// </summary>
        public int S01F02 { get; set; }

        /// <summary>
        /// Stock ID (Foreign key to STK01).
        /// </summary>
        public int S01F03 { get; set; }

        /// <summary>
        /// Quantity of the stock in the portfolio.
        /// </summary>
        public int S01F04 { get; set; }

        /// <summary>
        /// Purchase price of the stock.
        /// </summary>
        public decimal S01F05 { get; set; }

        /// <summary>
        /// Current value of the stock in the portfolio.
        /// </summary>
        public decimal S01F06 { get; set; }

        /// <summary>
        /// Total value of this stock in the portfolio (calculated as Quantity * CurrentValue).
        /// </summary>
        public decimal S01F07 { get; set; }

        /// <summary>
        /// User ID (Foreign key to USR01).
        /// </summary>
        public int S01F08 { get; set; }

        /// <summary>
        /// Timestamp when the portfolio stock entry was created.
        /// </summary>
        public DateTime S01F09 { get; set; }

        /// <summary>
        /// Timestamp when the portfolio stock entry was last updated.
        /// </summary>
        public DateTime S01F10 { get; set; }
    }
}
