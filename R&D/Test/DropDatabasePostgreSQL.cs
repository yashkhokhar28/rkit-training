using Npgsql;
using System;
using System.Diagnostics;

namespace Test
{
    public static class DropDatabasePostgreSQL
    {
        public static void DropDatabases(int number, string server, string userId, string password)
        {
            // Connection string with connection pooling enabled
            string connectionString = $"Host={server};Database=postgres;Username={userId};Password={password};Pooling=true;MaxPoolSize=10;MinPoolSize=1;";

            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();  // Start the timer

            try
            {
                // Connect to the PostgreSQL server
                using (var serverConnection = new NpgsqlConnection(connectionString))
                {
                    serverConnection.Open();
                    Console.WriteLine("Connected to PostgreSQL server.");

                    for (int i = 1; i <= number; i++)
                    {
                        string databaseName = $"test_db_{i}";

                        try
                        {
                            // Check if the database exists
                            if (DatabaseExists(serverConnection, databaseName))
                            {
                                // Forcefully drop the database if it exists
                                ForceDropDatabase(serverConnection, databaseName);
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
                Console.WriteLine($"An error occurred while connecting to the PostgreSQL server: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();  // Stop the timer
                // Output the elapsed time
                Console.WriteLine($"Total time taken : {stopwatch.Elapsed.TotalSeconds} seconds");
            }
        }

        // Check if a database exists
        private static bool DatabaseExists(NpgsqlConnection serverConnection, string databaseName)
        {
            try
            {
                string checkDbQuery = $"SELECT 1 FROM pg_database WHERE datname = @databaseName;";
                using (var checkCmd = new NpgsqlCommand(checkDbQuery, serverConnection))
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

        // Forcefully drop the specified database
        private static void ForceDropDatabase(NpgsqlConnection serverConnection, string databaseName)
        {
            try
            {
                // First, terminate all active connections to the database
                TerminateConnections(serverConnection, databaseName);

                // Now, drop the database
                string dropDbQuery = $"DROP DATABASE IF EXISTS \"{databaseName}\";";
                using (var dropCmd = new NpgsqlCommand(dropDbQuery, serverConnection))
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

        // Terminate all active connections to the specified database
        private static void TerminateConnections(NpgsqlConnection serverConnection, string databaseName)
        {
            try
            {
                // Query to find all backend processes using the database
                string terminateQuery = $@"
                    SELECT pg_terminate_backend(pid)
                    FROM pg_stat_activity
                    WHERE datname = @databaseName AND pid <> pg_backend_pid();";

                using (var terminateCmd = new NpgsqlCommand(terminateQuery, serverConnection))
                {
                    terminateCmd.Parameters.AddWithValue("@databaseName", databaseName);
                    var terminated = terminateCmd.ExecuteNonQuery();
                    Console.WriteLine($"Terminated {terminated} connection(s) to database '{databaseName}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error terminating connections for database '{databaseName}': {ex.Message}");
            }
        }
    }
}