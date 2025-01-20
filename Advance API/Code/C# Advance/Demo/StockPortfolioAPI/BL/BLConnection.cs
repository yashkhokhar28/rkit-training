using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace StockPortfolioAPI.BL
{
    public class BLConnection
    {
        /// <summary>
        /// The connection string
        /// </summary>
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}