using MySql.Data.MySqlClient;

namespace Test
{
    public static class DropDatabase
    {
        public static void DropDatabases(int number, string server, string userID, string password)
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
                        string createDatabaseQuery = Query.dropDatabase + " " + databaseName;
                        MySqlCommand objMySqlCommand = new MySqlCommand(createDatabaseQuery, objMySqlConnection);

                        objMySqlCommand.ExecuteNonQuery();
                        Console.WriteLine($"Database '{databaseName}' Dropped Successfully.");
                    }
                }

                Console.WriteLine("Table Drop completed for all databases.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
