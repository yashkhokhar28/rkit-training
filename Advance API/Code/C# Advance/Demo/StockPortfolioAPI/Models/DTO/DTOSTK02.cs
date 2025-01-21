using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockPortfolioAPI.Models.DTO
{
    /// <summary>
    /// model for view all stocks
    /// </summary>
    public class DTOSTK02
    {
        public string K01F02 { get; set; } // StockSymbol

        public string K01F03 { get; set; } // StockName

        public decimal K01F04 { get; set; } // Price

        public decimal? K01F05 { get; set; } // MarketCap (optional)
    }
}