using StockPortfolioAPI.Models.ENUM;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockPortfolioAPI.Models.DTO
{
    public class DTOTRN01
    {
        /// <summary>
        /// Unique identifier for each transaction.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "ID must be Positive.")]
        public int N01F01 { get; set; }

        /// <summary>
        /// Foreign key referencing the stock involved in the transaction.
        /// </summary>
        [Required(ErrorMessage = "Stock ID is required.")]
        public int N01F03 { get; set; }

        /// <summary>
        /// The number of shares bought or sold in the transaction.
        /// </summary>
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int N01F04 { get; set; }

        /// <summary>
        /// The type of transaction, either "Buy" or "Sell" (represented by integer values 1 and 0).
        /// </summary>
        [Required(ErrorMessage = "Transaction type is required.")]
        [Range(0, 1, ErrorMessage = "Transaction type must be either 0 (Sell) or 1 (Buy).")]
        public EnmTransactionType N01F06 { get; set; } // Using the enum EnmTransactionType for "Buy" and "Sell"
    }
}