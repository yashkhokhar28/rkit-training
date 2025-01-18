using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Test
{
    /// <summary>
    /// Handles dropping multiple MS SQL Server databases.
    /// Uses connection pooling to optimize database connection handling.
    /// </summary>
    public static class DropDatabaseMSSQL
    {
        /// <summary>
        /// Drops a specified range of databases from the MS SQL Server.
        /// The connection to the server uses pooling for efficient connection reuse.
        /// </summary>
        /// <param name="from">The starting index of the database range to drop.</param>
        /// <param name="to">The ending index of the database range to drop.</param>
        /// <param name="server">The MS SQL Server address.</param>
        /// <param name="userId">The MS SQL Server user ID.</param>
        /// <param name="password">The MS SQL Server password.</param>
        public static void DropDatabases(int from, int to, string server)
        {
            // Connection string with connection pooling enabled
            string connectionString = $"Data Source={server};Integrated Security=true;Pooling=true;Max Pool Size=100;Min Pool Size=10;";

            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();  // Start the timer

            try
            {
                // Connect to the MS SQL Server
                using (var serverConnection = new SqlConnection(connectionString))
                {
                    serverConnection.Open();
                    Console.WriteLine("Connected to MS SQL Server.");

                    // Loop through the range of database indices [from, to]
                    for (int i = from; i <= to; i++)
                    {
                        string databaseName = $"test_db_{i}";

                        try
                        {
                            // Check if the database exists
                            if (DatabaseExists(serverConnection, databaseName))
                            {
                                // Drop the database if it exists
                                DropDatabase(serverConnection, databaseName);
                            }
                            else
                            {
                                Console.WriteLine($"Database '{databaseName}' does not exist.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error while handling database '{databaseName}': {ex.Message}");
                        }
                    }
                }

                Console.WriteLine("Database drop process completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to the MS SQL Server: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();  // Stop the timer
                                   // Output the elapsed time
                Console.WriteLine($"Total time taken: {stopwatch.Elapsed.TotalSeconds} seconds");
            }
        }

        /// <summary>
        /// Checks if the specified database exists on the MS SQL Server.
        /// </summary>
        /// <param name="serverConnection">The connection to the MS SQL Server.</param>
        /// <param name="databaseName">The name of the database to check.</param>
        /// <returns>True if the database exists; otherwise, false.</returns>
        private static bool DatabaseExists(SqlConnection serverConnection, string databaseName)
        {
            try
            {
                string checkDbQuery = "SELECT 1 FROM sys.databases WHERE name = @databaseName;";

                using (var checkCmd = new SqlCommand(checkDbQuery, serverConnection))
                {
                    checkCmd.Parameters.AddWithValue("@databaseName", databaseName);
                    var dbExists = checkCmd.ExecuteScalar();
                    return dbExists != null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking existence of database '{databaseName}': {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Drops the specified database from the MS SQL Server.
        /// </summary>
        /// <param name="serverConnection">The connection to the MS SQL Server.</param>
        /// <param name="databaseName">The name of the database to drop.</param>
        private static void DropDatabase(SqlConnection serverConnection, string databaseName)
        {
            try
            {

                string dropDbQuery = string.Format(Query.DropDatabaseMSSQL, databaseName);

                using (var dropCmd = new SqlCommand(dropDbQuery, serverConnection))
                {
                    dropCmd.ExecuteNonQuery();
                    Console.WriteLine($"Database '{databaseName}' dropped successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error dropping database '{databaseName}': {ex.Message}");
            }
        }
    }
}
