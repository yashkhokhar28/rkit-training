using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockPortfolioAPI.Models
{
    public class UserSession
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

}