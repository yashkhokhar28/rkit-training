using System.Configuration;

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