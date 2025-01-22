using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace StockPortfolioAPI.BL
{
    /// <summary>
    /// Provides a static connection string for database access.
    /// </summary>
    public class BLConnection
    {
        /// <summary>
        /// The connection string for the database.
        /// </summary>
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}