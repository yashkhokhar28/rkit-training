using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Test
{
    /// <summary>
    /// This class provides functionality to insert data into MySQL databases from a CSV file.
    /// It handles multiple databases, uses connection pooling, and optimizes the insertion process 
    /// with batch inserts within transactions.
    /// </summary>
    public class InsertDataMySQL
    {
        /// <summary>
        /// Inserts data from a CSV file into multiple MySQL databases.
        /// Reinitializes the CSV reader for each database to avoid data exhaustion.
        /// </summary>
        /// <param name="number">The number of databases to insert data into.</param>
        /// <param name="csvFilePath">The file path of the CSV file to read the data from.</param>
        /// <param name="server">The MySQL server address.</param>
        /// <param name="userId">The MySQL user ID.</param>
        /// <param name="password">The MySQL password.</param>
        public static void InsertDataFromCsv(int number, string csvFilePath, string server, string userId, string password)
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
                    // Reading data line by line, instead of loading the entire file in memory
                    ReadingExceptionOccurred = (ex) => false
                };

                // Loop through all databases and perform the operation
                for (int i = 1; i <= number; i++)
                {
                    string dbName = $"test_db_{i}";
                    var connectionString = $"Server={server};Database={dbName};User ID={userId};Password={password};Pooling=true;Max Pool Size=100;Min Pool Size=10;";

                    using (MySqlConnection objMySqlConnection = new MySqlConnection(connectionString))
                    {
                        objMySqlConnection.Open();
                        Console.WriteLine($"Connected to MySQL database: {dbName}");

                        // Begin a transaction for batch processing
                        using (var transaction = objMySqlConnection.BeginTransaction())
                        {
                            // Reinitialize the CSV reader for each database to avoid exhausting the records
                            using (StreamReader objStreamReader = new StreamReader(csvFilePath))
                            using (CsvReader objCsvReader = new CsvReader(objStreamReader, objCsvConfiguration))
                            {
                                // Read the records from the CSV for each database
                                while (objCsvReader.Read())
                                {
                                    var record = objCsvReader.GetRecord<dynamic>();
                                    try
                                    {
                                        InsertRecord(objMySqlConnection, record, transaction);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error inserting record into {dbName}: {ex.Message}");
                                    }
                                }
                            }

                            // Commit the transaction to ensure all records are inserted atomically
                            transaction.Commit();
                            Console.WriteLine($"Data inserted into {dbName}");
                        }
                    }
                }

                Console.WriteLine("Data insertion completed for all databases.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while processing the CSV: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();  // Stop the timer
                // Output the elapsed time
                Console.WriteLine($"Total time taken : {stopwatch.Elapsed.TotalSeconds} seconds");
            }
        }

        /// <summary>
        /// Inserts a single record into the database using a MySQL command.
        /// This method is optimized for batch insertion within a transaction.
        /// </summary>
        /// <param name="connection">The MySQL connection to use for the insert.</param>
        /// <param name="record">The record to insert into the database.</param>
        /// <param name="transaction">The transaction to be used for batch insert.</param>
        private static void InsertRecord(MySqlConnection connection, dynamic record, MySqlTransaction transaction)
        {
            string query = @"
                INSERT INTO orders 
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
        /// <param name="command">The MySQL command object to which parameters are added.</param>
        /// <param name="record">The record containing the data for the parameters.</param>
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