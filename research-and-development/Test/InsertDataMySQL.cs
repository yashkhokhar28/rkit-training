using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using MySql.Data.MySqlClient;

namespace Test
{
    public class InsertDataMySQL
    {
        /// <summary>
        /// Inserts data from a CSV file into multiple tables within a single MySQL database.
        /// This method handles parallel insertions into multiple tables.
        /// </summary>
        /// <param name="from">Starting database index.</param>
        /// <param name="to">Ending database index.</param>
        /// <param name="csvFilePath">Path to the CSV file.</param>
        /// <param name="server">MySQL server address.</param>
        /// <param name="userId">MySQL user ID.</param>
        /// <param name="password">MySQL password.</param>
        /// <param name="fromTable">Starting table index.</param>
        /// <param name="toTable">Ending table index.</param>
        public static void InsertDataFromCsv(int from, int to, string csvFilePath, string server, string userId, string password, int fromTable, int toTable)
        {
            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();  // Start the timer

            try
            {
                // Set up the CSV reader configuration
                CsvConfiguration objCsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // Ensure the CSV has headers
                    ReadingExceptionOccurred = (ex) => false // Ensure no exception is thrown on invalid rows
                };

                // Loop through all databases in the specified range [from, to]
                for (int i = from; i <= to; i++)
                {
                    string dbName = $"test_db_{i}";
                    string dbConnectionString = $"Server={server};Database={dbName};User ID={userId};Password={password};Pooling=true;Max Pool Size=100;Min Pool Size=10;";

                    // Start parallel tasks for inserting data into multiple tables within the range [fromTable, toTable]
                    var tableInsertTasks = new List<Task>();

                    for (int j = fromTable; j <= toTable; j++)
                    {
                        int tableIndex = j;
                        string tableName = $"orders_{tableIndex}";  // Dynamically generate table names

                        // Add a task to insert data into the current table
                        tableInsertTasks.Add(Task.Run(() =>
                        {
                            InsertDataForTable(csvFilePath, dbConnectionString, objCsvConfiguration, tableName);
                        }));
                    }

                    // Wait for all table insert tasks to complete
                    Task.WhenAll(tableInsertTasks).Wait();
                }

                Console.WriteLine("Data insertion completed for all databases and tables.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while processing the CSV: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();  // Stop the timer
                Console.WriteLine($"Total time taken: {stopwatch.Elapsed.TotalSeconds} seconds");
            }
        }

        /// <summary>
        /// Inserts data into a specific table within a database.
        /// This method runs in parallel for each table.
        /// </summary>
        private static void InsertDataForTable(string csvFilePath, string dbConnectionString, CsvConfiguration csvConfiguration, string tableName)
        {
            try
            {
                using (MySqlConnection objMySqlConnection = new MySqlConnection(dbConnectionString))
                {
                    objMySqlConnection.Open();
                    Console.WriteLine($"Connected to MySQL database for table: {tableName}");

                    // Begin a transaction for batch processing
                    using (var transaction = objMySqlConnection.BeginTransaction())
                    {
                        // Reinitialize the CSV reader for each table
                        using (StreamReader objStreamReader = new StreamReader(csvFilePath))
                        using (CsvReader objCsvReader = new CsvReader(objStreamReader, csvConfiguration))
                        {
                            // Read the records from the CSV for each table
                            while (objCsvReader.Read())
                            {
                                var record = objCsvReader.GetRecord<dynamic>();
                                try
                                {
                                    InsertRecord(objMySqlConnection, record, transaction, tableName);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error inserting record into {tableName}: {ex.Message}");
                                }
                            }
                        }

                        // Commit the transaction to ensure all records are inserted atomically
                        transaction.Commit();
                        Console.WriteLine($"Data inserted into table {tableName} successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while inserting data into {tableName}: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserts a single record into the specified table using a MySQL command.
        /// </summary>
        private static void InsertRecord(MySqlConnection connection, dynamic record, MySqlTransaction transaction, string tableName)
        {
            string query = $@"
                INSERT INTO {tableName} 
                (`index`, `Order_ID`, `Date`, `Status`, `Fulfilment`, `Sales_Channel`, `Ship_Service_Level`, `Style`, `SKU`, 
                 `Category`, `Size`, `ASIN`, `Courier_Status`, `Qty`, `Currency`, `Amount`, `Ship_City`, `Ship_State`, 
                 `Ship_Postal_Code`, `Ship_Country`, `Promotion_IDs`, `B2B`, `Fulfilled_By`)
                VALUES 
                (@index, @Order_ID, @Date, @Status, @Fulfilment, @Sales_Channel, @Ship_Service_Level, @Style, @SKU, 
                 @Category, @Size, @ASIN, @Courier_Status, @Qty, @Currency, @Amount, @Ship_City, @Ship_State, 
                 @Ship_Postal_Code, @Ship_Country, @Promotion_IDs, @B2B, @Fulfilled_By);
            ";

            using (MySqlCommand objMySqlCommand = new MySqlCommand(query, connection, transaction))
            {
                AddParameters(objMySqlCommand, record);
                objMySqlCommand.ExecuteNonQuery();  // Execute the insert query within the transaction
            }
        }

        /// <summary>
        /// Adds parameters to the MySQL command.
        /// </summary>
        private static void AddParameters(MySqlCommand command, dynamic record)
        {
            command.Parameters.AddWithValue("@index", string.IsNullOrEmpty(record.index) ? DBNull.Value : Convert.ToInt32(record.index));
            command.Parameters.AddWithValue("@Order_ID", record.Order_ID ?? DBNull.Value);
            command.Parameters.AddWithValue("@Date", record.Date ?? DBNull.Value);
            command.Parameters.AddWithValue("@Status", record.Status ?? DBNull.Value);
            command.Parameters.AddWithValue("@Fulfilment", record.Fulfilment ?? DBNull.Value);
            command.Parameters.AddWithValue("@Sales_Channel", record.Sales_Channel ?? DBNull.Value);
            command.Parameters.AddWithValue("@Ship_Service_Level", record.ship_service_level ?? DBNull.Value);
            command.Parameters.AddWithValue("@Style", record.Style ?? DBNull.Value);
            command.Parameters.AddWithValue("@SKU", record.SKU ?? DBNull.Value);
            command.Parameters.AddWithValue("@Category", record.Category ?? DBNull.Value);
            command.Parameters.AddWithValue("@Size", record.Size ?? DBNull.Value);
            command.Parameters.AddWithValue("@ASIN", record.ASIN ?? DBNull.Value);
            command.Parameters.AddWithValue("@Courier_Status", record.Courier_Status ?? DBNull.Value);
            command.Parameters.AddWithValue("@Qty", string.IsNullOrEmpty(record.Qty) ? DBNull.Value : Convert.ToInt32(record.Qty));
            command.Parameters.AddWithValue("@Currency", record.currency ?? DBNull.Value);
            command.Parameters.AddWithValue("@Amount", string.IsNullOrEmpty(record.Amount) ? DBNull.Value : Convert.ToDecimal(record.Amount));
            command.Parameters.AddWithValue("@Ship_City", record.ship_city ?? DBNull.Value);
            command.Parameters.AddWithValue("@Ship_State", record.ship_state ?? DBNull.Value);
            command.Parameters.AddWithValue("@Ship_Postal_Code", record.ship_postal_code ?? DBNull.Value);
            command.Parameters.AddWithValue("@Ship_Country", record.ship_country ?? DBNull.Value);
            command.Parameters.AddWithValue("@Promotion_IDs", record.promotion_ids ?? DBNull.Value);
            command.Parameters.AddWithValue("@B2B", record.B2B ?? DBNull.Value);
            command.Parameters.AddWithValue("@Fulfilled_By", record.fulfilled_by ?? DBNull.Value);
        }
    }
}