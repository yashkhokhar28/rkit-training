using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Test
{
    /// <summary>
    /// Handles MS SQL Server database and table creation with connection pooling enabled.
    /// This class provides functionality to create multiple databases and tables within those databases.
    /// </summary>
    public static class CreateDatabaseMSSQL
    {
        /// <summary>
        /// Creates multiple databases and tables in MS SQL Server.
        /// It opens a connection to the SQL Server, checks if the databases exist, and creates them if they do not.
        /// Then, it creates a table in each of the created databases using the specified query.
        /// </summary>
        /// <param name="from">The starting number of the database range.</param>
        /// <param name="to">The ending number of the database range.</param>
        /// <param name="query">The SQL query to create the table in each database.</param>
        /// <param name="server">The SQL Server address.</param>
        public static void CreateDatabases(int from, int to, string query, string server)
        {
            // Connection string with pooling enabled
            string serverConnectionString = $"Data Source={server};Integrated Security=true;Pooling=true;Max Pool Size=100;Min Pool Size=10;";

            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();  // Start the timer

            try
            {
                // Open connection to the SQL Server
                using (var serverConnection = new SqlConnection(serverConnectionString))
                {
                    Console.WriteLine($"[Server Connection] Opening connection...");
                    serverConnection.Open();
                    Console.WriteLine($"[Server Connection] Opened.");

                    // Loop through each database in the range [from, to]
                    for (int i = from; i <= to; i++)
                    {
                        string databaseName = $"test_db_{i}";

                        // Check if the database exists
                        if (!DatabaseExists(serverConnection, databaseName))
                        {
                            // Create the database if it doesn't exist
                            CreateDatabase(serverConnection, databaseName);
                        }

                        // Connection string for the individual database
                        string dbConnectionString = $"Data Source={server};Initial Catalog={databaseName};Integrated Security=true;Pooling=true;";

                        // Create a connection for the specific database
                        using (var dbConnection = new SqlConnection(dbConnectionString))
                        {
                            Console.WriteLine($"[DB Connection for {databaseName}] Opening connection...");
                            dbConnection.Open();
                            Console.WriteLine($"[DB Connection for {databaseName}] Opened.");

                            // Create the table in the database using the provided query
                            CreateTableInDatabase(dbConnection, query);

                            Console.WriteLine($"[DB Connection for {databaseName}] Closing connection...");
                        }
                        Console.WriteLine($"[DB Connection for {databaseName}] Closed.");
                    }
                }
                Console.WriteLine("[Server Connection] Closed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();  // Stop the timer
                                   // Output the elapsed time
                Console.WriteLine($"Total time taken : {stopwatch.Elapsed.TotalSeconds} seconds");
            }
        }

        /// <summary>
        /// Checks if a specific database exists on the SQL Server.
        /// Uses the system catalog to verify if the database is present.
        /// </summary>
        /// <param name="serverConnection">The connection to the SQL Server.</param>
        /// <param name="databaseName">The name of the database to check for existence.</param>
        /// <returns>True if the database exists; otherwise, false.</returns>
        private static bool DatabaseExists(SqlConnection serverConnection, string databaseName)
        {
            // SQL query to check if the database exists
            string checkDbQuery = "SELECT 1 FROM sys.databases WHERE name = @databaseName;";

            try
            {
                // Prepare the command to execute the query
                using (var checkCmd = new SqlCommand(checkDbQuery, serverConnection))
                {
                    checkCmd.Parameters.AddWithValue("@databaseName", databaseName);
                    return checkCmd.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking existence of database '{databaseName}': {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Creates a new database on the SQL Server if it doesn't already exist.
        /// </summary>
        /// <param name="serverConnection">The connection to the SQL Server.</param>
        /// <param name="databaseName">The name of the database to create.</param>
        private static void CreateDatabase(SqlConnection serverConnection, string databaseName)
        {
            // SQL query to create the database
            string createDbQuery = $"CREATE DATABASE {databaseName};";

            try
            {
                // Prepare the command to execute the query
                using (var createCmd = new SqlCommand(createDbQuery, serverConnection))
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

        /// <summary>
        /// Creates a table in the specified database using the provided SQL query.
        /// </summary>
        /// <param name="dbConnection">The connection to the specific database.</param>
        /// <param name="createTableQuery">The SQL query to create the table.</param>
        private static void CreateTableInDatabase(SqlConnection dbConnection, string createTableQuery)
        {
            try
            {
                // Prepare the command to execute the table creation query
                using (var createTableCmd = new SqlCommand(createTableQuery, dbConnection))
                {
                    createTableCmd.ExecuteNonQuery();
                    Console.WriteLine("Table created successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table: {ex.Message}");
            }
        }
    }
}
