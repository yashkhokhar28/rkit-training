using Npgsql; // PostgreSQL library
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Test
{
    public static class InsertDataPostgreSQL
    {
        public static void InsertDataFromCsvPostgres(int number, string csvFilePath, string server, string username, string password)
        {
            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();  // Start the timer

            try
            {
                string connectionStringTemplate = "Host={0};Database={1};Username={2};Password={3};Pooling=true;MaxPoolSize=10;MinPoolSize=1;";

                // Read CSV data
                CsvConfiguration objCsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // Ensure the CSV has headers
                    ReadingExceptionOccurred = (ex) => false
                };

                // Loop through all databases and perform the operation
                for (int i = 1; i <= number; i++)
                {
                    string databaseName = $"test_db_{i}";
                    string connectionString = string.Format(connectionStringTemplate, server, databaseName, username, password);

                    using (NpgsqlConnection objNpgsqlConnection = new NpgsqlConnection(connectionString))
                    {
                        objNpgsqlConnection.Open();
                        Console.WriteLine($"Connected to PostgreSQL database: {databaseName}");

                        // Begin a transaction for batch processing
                        using (var transaction = objNpgsqlConnection.BeginTransaction())
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
                                        InsertRecord(objNpgsqlConnection, record, transaction); // Insert the record into the database
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error inserting record into {databaseName}: {ex.Message}");
                                    }
                                }
                            }

                            // Commit the transaction to ensure all records are inserted atomically
                            transaction.Commit();
                            Console.WriteLine($"Data inserted into {databaseName}");
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
                Console.WriteLine($"Total time taken: {stopwatch.Elapsed.TotalSeconds} seconds");
            }
        }

        /// <summary>
        /// Inserts a single record into the database using a PostgreSQL command.
        /// This method is optimized for batch insertion within a transaction.
        /// </summary>
        private static void InsertRecord(NpgsqlConnection connection, dynamic record, NpgsqlTransaction transaction)
        {
            string query = @"
                INSERT INTO orders 
                (index, Order_ID, Date, Status, Fulfilment, Sales_Channel, Ship_Service_Level, Style, SKU, 
                 Category, Size, ASIN, Courier_Status, Qty, Currency, Amount, Ship_City, Ship_State, 
                 Ship_Postal_Code, Ship_Country, Promotion_IDs, B2B, Fulfilled_By)
                VALUES 
                (@index, @Order_ID, @Date, @Status, @Fulfilment, @Sales_Channel, @Ship_Service_Level, @Style, @SKU, 
                 @Category, @Size, @ASIN, @Courier_Status, @Qty, @Currency, @Amount, @Ship_City, @Ship_State, 
                 @Ship_Postal_Code, @Ship_Country, @Promotion_IDs, @B2B, @Fulfilled_By);
            ";

            using (NpgsqlCommand objNpgsqlCommand = new NpgsqlCommand(query, connection, transaction))
            {
                AddParameters(objNpgsqlCommand, record);  // Add parameters to the command
                objNpgsqlCommand.ExecuteNonQuery();  // Execute the insert query within the transaction
            }
        }

        /// <summary>
        /// Adds parameters to the PostgreSQL command.
        /// </summary>
        private static void AddParameters(NpgsqlCommand command, dynamic record)
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
