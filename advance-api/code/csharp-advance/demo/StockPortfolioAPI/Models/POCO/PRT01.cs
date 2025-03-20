using System;

namespace StockPortfolioAPI.Models.POCO
{
    /// <summary>
    /// Represents a portfolio owned by a user.
    /// </summary>
    public class PRT01
    {
        /// <summary>
        /// Unique identifier for each portfolio.
        /// </summary>
        public int T01F01 { get; set; }

        /// <summary>
        /// Foreign key referencing the User who owns the portfolio.
        /// </summary>
        public int T01F02 { get; set; }

        /// <summary>
        /// The name of the portfolio (e.g., "My First Portfolio").
        /// </summary>
        public string T01F03 { get; set; }

        /// <summary>
        /// The total value of the portfolio (calculated based on stocks).
        /// </summary>
        public decimal T01F04 { get; set; }

        /// <summary>
        /// Timestamp when the portfolio was created.
        /// </summary>
        public DateTime T01F05 { get; set; }

        /// <summary>
        /// Timestamp when the portfolio details were last updated.
        /// </summary>
        public DateTime T01F06 { get; set; }
    }
}
