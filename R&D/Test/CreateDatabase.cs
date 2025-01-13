using MySql.Data.MySqlClient;

namespace Test
{
    public static class CreateDatabase
    {
        public static void CreateDatabases(int number, string query, string server, string userID, string password)
        {
            try
            {
                for (int i = 1; i <= number; i++)
                {
                    string databaseName = $"test_db_{i}";

                    // Connection string to connect to the MySQL server (not a specific database)
                    string connectionString = $"Server={server};User ID={userID};Password={password};";

                    // Connect to the MySQL server and create the database if it doesn't exist
                    using (MySqlConnection objMySqlConnection = new MySqlConnection(connectionString))
                    {
                        objMySqlConnection.Open();
                        Console.WriteLine($"Connected to MySQL server.");

                        // Create database if it doesn't exist
                        string createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS {databaseName};";
                        MySqlCommand objMySqlCommand = new MySqlCommand(createDatabaseQuery, objMySqlConnection);

                        objMySqlCommand.ExecuteNonQuery();
                        Console.WriteLine($"Database '{databaseName}' is ready.");

                        // Now, connect to the specific database
                        string dbConnectionString = $"Server={server};Database={databaseName};User ID={userID};Password={password};";
                        using (MySqlConnection objMySqlConnection1 = new MySqlConnection(dbConnectionString))
                        {
                            objMySqlConnection1.Open();
                            Console.WriteLine($"Connected to database {databaseName}");

                            // Execute the SQL command to create the table
                            MySqlCommand objMySqlCommand1 = new MySqlCommand(query, objMySqlConnection1);
                            objMySqlCommand1.ExecuteNonQuery();

                            Console.WriteLine($"Table 'orders' created in {databaseName}");
                        }
                    }
                }

                Console.WriteLine("Table creation completed for all databases.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
