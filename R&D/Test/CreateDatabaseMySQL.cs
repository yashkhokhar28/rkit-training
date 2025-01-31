using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Test
{
    public static class CreateDatabaseMySQL
    {
        public static async Task CreateDatabasesAsync(int from, int to, int fromTable, int toTable, string queryTemplate, string server, string userId, string password)
        {
            string serverConnectionString = $"Server={server};User ID={userId};Password={password};Pooling=true;Max Pool Size=100;Min Pool Size=10;";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                using (var serverConnection = new MySqlConnection(serverConnectionString))
                {
                    Console.WriteLine($"[Server Connection] Opening connection...");
                    await serverConnection.OpenAsync();
                    Console.WriteLine($"[Server Connection] Opened.");

                    var databaseTasks = new List<Task>();

                    for (int i = from; i <= to; i++)
                    {
                        int dbIndex = i;
                        databaseTasks.Add(Task.Run(async () =>
                        {
                            await CreateDatabaseWithTablesAsync(dbIndex, fromTable, toTable, queryTemplate, server, userId, password);
                        }));
                    }

                    await Task.WhenAll(databaseTasks);
                }
                Console.WriteLine("[Server Connection] Closed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Total time taken : {stopwatch.Elapsed.TotalSeconds} seconds");
            }
        }

        private static async Task CreateDatabaseWithTablesAsync(int dbIndex, int fromTable, int toTable, string queryTemplate, string server, string userId, string password)
        {
            string databaseName = $"test_db_{dbIndex}";
            string serverConnectionString = $"Server={server};User ID={userId};Password={password};Pooling=true;";

            using (var serverConnection = new MySqlConnection(serverConnectionString))
            {
                await serverConnection.OpenAsync();

                if (!await DatabaseExistsAsync(serverConnection, databaseName))
                {
                    await CreateDatabaseAsync(serverConnection, databaseName);
                }
            }

            string dbConnectionString = $"Server={server};Database={databaseName};User ID={userId};Password={password};Pooling=true;";

            var tableTasks = new List<Task>();

            // Create tables within the specified range (fromTable to toTable)
            for (int j = fromTable; j <= toTable; j++)
            {
                string tableName = $"orders_{j}"; // Dynamic table name
                string createTableQuery = queryTemplate.Replace("orders", tableName);

                tableTasks.Add(Task.Run(async () =>
                {
                    await CreateTableInDatabaseAsync(dbConnectionString, createTableQuery, tableName);
                }));
            }

            await Task.WhenAll(tableTasks);
        }

        /// <summary>
        /// Creates a table using a **new MySqlConnection per query** (prevents DataReader issue).
        /// </summary>
        private static async Task CreateTableInDatabaseAsync(string dbConnectionString, string createTableQuery, string tableName)
        {
            try
            {
                using (var dbConnection = new MySqlConnection(dbConnectionString))
                {
                    await dbConnection.OpenAsync();

                    using (var createTableCmd = new MySqlCommand(createTableQuery, dbConnection))
                    {
                        await createTableCmd.ExecuteNonQueryAsync();
                        Console.WriteLine($"Table '{tableName}' created successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table '{tableName}': {ex.Message}");
            }
        }

        private static async Task<bool> DatabaseExistsAsync(MySqlConnection serverConnection, string databaseName)
        {
            string checkDbQuery = $"SELECT 1 FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @databaseName;";
            try
            {
                using (var checkCmd = new MySqlCommand(checkDbQuery, serverConnection))
                {
                    checkCmd.Parameters.AddWithValue("@databaseName", databaseName);
                    var result = await checkCmd.ExecuteScalarAsync();
                    return result != null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking existence of database '{databaseName}': {ex.Message}");
                return false;
            }
        }

        private static async Task CreateDatabaseAsync(MySqlConnection serverConnection, string databaseName)
        {
            string createDbQuery = $"CREATE DATABASE {databaseName};";
            try
            {
                using (var createCmd = new MySqlCommand(createDbQuery, serverConnection))
                {
                    await createCmd.ExecuteNonQueryAsync();
                    Console.WriteLine($"Database '{databaseName}' created.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating database '{databaseName}': {ex.Message}");
            }
        }
    }
}
