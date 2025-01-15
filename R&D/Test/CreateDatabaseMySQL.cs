using MySql.Data.MySqlClient;
using System;

namespace Test
{
    public static class CreateDatabaseMySQL
    {
        public static void CreateDatabases(int number, string query, string server, string userId, string password)
        {
            string serverConnectionString = $"Server={server};User ID={userId};Password={password};";

            try
            {
                using (var serverConnection = new MySqlConnection(serverConnectionString))
                {
                    serverConnection.Open();
                    Console.WriteLine("Connected to MySQL server.");

                    for (int i = 1; i <= number; i++)
                    {
                        string databaseName = $"test_db_{i}";

                        try
                        {
                            // Check if database exists, if not, create it
                            if (!DatabaseExists(serverConnection, databaseName))
                            {
                                CreateDatabase(serverConnection, databaseName);
                            }
                            else
                            {
                                Console.WriteLine($"Database '{databaseName}' already exists.");
                            }

                            // Create table in the new database
                            CreateTableInDatabase(server, databaseName, userId, password, query);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error while handling database '{databaseName}': {ex.Message}");
                        }
                    }
                }

                Console.WriteLine("All databases and tables processed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to MySQL server: {ex.Message}");
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

        // Create the database if it doesn't exist
        private static void CreateDatabase(MySqlConnection serverConnection, string databaseName)
        {
            try
            {
                string createDbQuery = $"CREATE DATABASE {databaseName};";
                using (var createCmd = new MySqlCommand(createDbQuery, serverConnection))
                {
                    createCmd.ExecuteNonQuery();
                    Console.WriteLine($"Database '{databaseName}' created.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating database '{databaseName}': {ex.Message}");
            }
        }

        // Create the table in the specified database
        private static void CreateTableInDatabase(string server, string databaseName, string userId, string password, string createTableQuery)
        {
            string dbConnectionString = $"Server={server};Database={databaseName};User ID={userId};Password={password};";
            try
            {
                using (var dbConnection = new MySqlConnection(dbConnectionString))
                {
                    dbConnection.Open();
                    using (var createTableCmd = new MySqlCommand(createTableQuery, dbConnection))
                    {
                        createTableCmd.ExecuteNonQuery();
                        Console.WriteLine($"Table 'orders' created in '{databaseName}'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table in database '{databaseName}': {ex.Message}");
            }
        }
    }
}