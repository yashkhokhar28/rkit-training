using System;

namespace StockPortfolioAPI.Models.POCO
{
    /// <summary>
    /// Represents a stock transaction within a portfolio.
    /// </summary>
    public class TRN01
    {
        /// <summary>
        /// Unique identifier for each transaction.
        /// </summary>
        public int N01F01 { get; set; }

        /// <summary>
        /// Foreign key referencing the portfolio where the transaction occurred.
        /// </summary>
        public int N01F02 { get; set; }

        /// <summary>
        /// Foreign key referencing the stock involved in the transaction.
        /// </summary>
        public int N01F03 { get; set; }

        /// <summary>
        /// The number of shares bought or sold in the transaction.
        /// </summary>
        public int N01F04 { get; set; }

        /// <summary>
        /// The price of the stock at the time of the transaction.
        /// </summary>
        public decimal N01F05 { get; set; }

        /// <summary>
        /// The type of transaction, either "Buy" or "Sell".
        /// </summary>
        public string N01F06 { get; set; }

        /// <summary>
        /// Timestamp when the transaction occurred.
        /// </summary>
        public DateTime N01F07 { get; set; }
    }
}
