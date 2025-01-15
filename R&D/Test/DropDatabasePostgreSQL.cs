using Npgsql;
using System;

namespace Test
{
    public static class DropDatabasePostgreSQL
    {
        public static void DropDatabases(int number, string server, string userId, string password)
        {
            string connectionString = $"Host={server};Database=postgres;Username={userId};Password={password};";

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
                Console.WriteLine($"An error occurred while connecting to the PostgreSQL server: {ex.Message}");
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

        // Drop the specified database
        private static void DropDatabase(NpgsqlConnection serverConnection, string databaseName)
        {
            try
            {
                string dropDbQuery = string.Format(Query.DropDatabasePostgreSQL, databaseName);
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
    }
}