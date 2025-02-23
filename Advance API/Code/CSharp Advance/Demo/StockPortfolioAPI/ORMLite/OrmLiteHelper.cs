using ServiceStack.OrmLite;
using StockPortfolioAPI.BL;
using System.Data;

namespace StockPortfolioAPI.ORMLite
{
    /// <summary>
    /// Provides helper methods for working with OrmLite connections.
    /// </summary>
    public static class OrmLiteHelper
    {
        // Connection factory for creating database connections
        private static readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Static constructor to initialize the connection factory with the appropriate dialect and connection string.
        /// </summary>
        static OrmLiteHelper()
        {
            // Choose the appropriate dialect (MySqlDialect, SqlServerDialect, etc.)
            _dbFactory = new OrmLiteConnectionFactory(BLConnection.ConnectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Opens a new database connection using the configured connection factory.
        /// </summary>
        /// <returns>An open IDbConnection instance.</returns>
        public static IDbConnection OpenConnection()
        {
            return _dbFactory.OpenDbConnection();
        }
    }
}