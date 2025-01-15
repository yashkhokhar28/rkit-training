using MySql.Data.MySqlClient;
using System;

namespace Test
{
    public static class DropDatabaseMySQL
    {
        public static void DropDatabases(int number, string server, string userId, string password)
        {
            string connectionString = $"Server={server};User ID={userId};Password={password};";

            try
            {
                // Connect to the MySQL server to manage databases
                using (var serverConnection = new MySqlConnection(connectionString))
                {
                    serverConnection.Open();
                    Console.WriteLine("Connected to MySQL server.");

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
                Console.WriteLine($"An error occurred while connecting to the MySQL server: {ex.Message}");
            }
        }

        // Check if a database exists
        private static bool DatabaseExists(MySqlConnection serverConnection, string databaseName)
        {
            try
            {
                string checkDbQuery = $"SELECT 1 FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @databaseName;";
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

        // Drop the specified database
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