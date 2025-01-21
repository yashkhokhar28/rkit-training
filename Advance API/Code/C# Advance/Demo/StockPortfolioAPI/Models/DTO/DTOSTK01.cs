using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockPortfolioAPI.Models.DTO
{
    /// <summary>
    /// model for stock add
    /// </summary>
    public class DTOSTK01
    {
        /// <summary>
        /// Unique identifier for each stock.
        /// </summary>
        [JsonProperty("K01101")]
        public int K01F01 { get; set; }

        [JsonProperty("K01102")]
        [Required(ErrorMessage = "StockSymbol is required.")]
        [StringLength(20, ErrorMessage = "StockSymbol must not exceed 20 characters.")]
        public string K01F02 { get; set; } // StockSymbol

        [JsonProperty("K01103")]
        [Required(ErrorMessage = "StockName is required.")]
        [StringLength(255, ErrorMessage = "StockName must not exceed 255 characters.")]
        public string K01F03 { get; set; } // StockName

        [JsonProperty("K01104")]
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal K01F04 { get; set; } // Price

        [JsonProperty("K01105")]
        [Range(0, double.MaxValue, ErrorMessage = "MarketCap must be greater than or equal to 0.")]
        public decimal? K01F05 { get; set; } // MarketCap (optional)
    }
}