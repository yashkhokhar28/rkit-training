using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;

namespace Test
{
    /// <summary>
    /// Handles dropping multiple MySQL databases.
    /// Uses connection pooling to optimize database connection handling.
    /// </summary>
    public static class DropDatabaseMySQL
    {
        /// <summary>
        /// Drops a specified number of databases from the MySQL server.
        /// The connection to the server uses pooling for efficient connection reuse.
        /// </summary>
        /// <param name="number">The number of databases to drop.</param>
        /// <param name="server">The MySQL server address.</param>
        /// <param name="userId">The MySQL user ID.</param>
        /// <param name="password">The MySQL password.</param>
        public static void DropDatabases(int from, int to, string server, string userId, string password)
        {
            // Connection string with connection pooling enabled
            string connectionString = $"Server={server};User ID={userId};Password={password};Pooling=true;Max Pool Size=100;Min Pool Size=10;";

            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();  // Start the timer

            try
            {
                // Connect to the MySQL server to manage databases
                using (var serverConnection = new MySqlConnection(connectionString))
                {
                    serverConnection.Open();
                    Console.WriteLine("Connected to MySQL server.");

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
                Console.WriteLine($"An error occurred while connecting to the MySQL server: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();  // Stop the timer
                                   // Output the elapsed time
                Console.WriteLine($"Total time taken : {stopwatch.Elapsed.TotalSeconds} seconds");
            }
        }

        /// <summary>
        /// Checks if the specified database exists on the MySQL server.
        /// </summary>
        /// <param name="serverConnection">The connection to the MySQL server.</param>
        /// <param name="databaseName">The name of the database to check.</param>
        /// <returns>True if the database exists; otherwise, false.</returns>
        private static bool DatabaseExists(MySqlConnection serverConnection, string databaseName)
        {
            try
            {
                string checkDbQuery = "SELECT 1 FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @databaseName;";

                using (var checkCmd = new MySqlCommand(checkDbQuery, serverConnection))
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
        /// Drops the specified database from the MySQL server.
        /// </summary>
        /// <param name="serverConnection">The connection to the MySQL server.</param>
        /// <param name="databaseName">The name of the database to drop.</param>
        private static void DropDatabase(MySqlConnection serverConnection, string databaseName)
        {
            try
            {
                string dropDbQuery = $"DROP DATABASE IF EXISTS `{databaseName}`;";

                using (var dropCmd = new MySqlCommand(dropDbQuery, serverConnection))
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