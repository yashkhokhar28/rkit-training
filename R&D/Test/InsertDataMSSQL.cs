using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Test
{
    /// <summary>
    /// Handles inserting data into multiple MS SQL Server databases from a CSV file.
    /// Uses connection pooling, multithreading, and transactions for optimal performance.
    /// </summary>
    public class InsertDataMSSQL
    {
        /// <summary>
        /// Inserts data from a CSV file into multiple MS SQL Server databases using threading for parallel execution.
        /// </summary>
        /// <param name="from">The starting index of the database range.</param>
        /// <param name="to">The ending index of the database range.</param>
        /// <param name="csvFilePath">The file path of the CSV file.</param>
        /// <param name="server">The MS SQL Server address.</param>
        /// <param name="userId">The MS SQL Server user ID.</param>
        /// <param name="password">The MS SQL Server password.</param>
        public static void InsertDataFromCsv(int from, int to, string csvFilePath, string server)
        {
            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                // CSV reader configuration
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // Ensure the CSV has headers
                    ReadingExceptionOccurred = (ex) => false // Ignore invalid rows
                };

                // Create a list of tasks for parallel execution
                Task[] tasks = new Task[to - from + 1];

                // Loop through all databases in the specified range [from, to]
                for (int i = from; i <= to; i++)
                {
                    int dbIndex = i;
                    tasks[dbIndex - from] = Task.Run(() =>
                    {
                        InsertDataForDatabase(dbIndex, csvFilePath, server, csvConfig);
                    });
                }

                // Wait for all tasks to complete
                Task.WhenAll(tasks).Wait();

                Console.WriteLine("Data insertion completed for all databases.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while processing the CSV: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Total time taken: {stopwatch.Elapsed.TotalSeconds} seconds");
            }
        }

        /// <summary>
        /// Inserts data into a specific database. Executed in parallel for each database.
        /// </summary>
        private static void InsertDataForDatabase(int databaseIndex, string csvFilePath, string server, CsvConfiguration csvConfig)
        {
            string dbName = $"test_db_{databaseIndex}";
            string connectionString = $"Data Source={server};Initial Catalog={dbName};Pooling=true;Integrated Security=true;Max Pool Size=100;Min Pool Size=10;";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(connectionString))
                {
                    dbConnection.Open();
                    Console.WriteLine($"[Thread ID: {Thread.CurrentThread.ManagedThreadId}] Connected to MS SQL database: {dbName}");

                    // Begin transaction for batch processing
                    using (SqlTransaction transaction = dbConnection.BeginTransaction())
                    {
                        using (StreamReader csvReader = new StreamReader(csvFilePath))
                        using (CsvReader csv = new CsvReader(csvReader, csvConfig))
                        {
                            // Read records and insert into the database
                            while (csv.Read())
                            {
                                var record = csv.GetRecord<dynamic>();
                                try
                                {
                                    InsertRecord(dbConnection, record, transaction);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"[Thread ID: {Thread.CurrentThread.ManagedThreadId}] Error inserting record into {dbName}: {ex.Message}");
                                }
                            }
                        }

                        // Commit transaction after successful insertion
                        transaction.Commit();
                        Console.WriteLine($"[Thread ID: {Thread.CurrentThread.ManagedThreadId}] Data inserted into {dbName}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Thread ID: {Thread.CurrentThread.ManagedThreadId}] Error with database {dbName}: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserts a single record into the database using a SQL command.
        /// </summary>
        private static void InsertRecord(SqlConnection connection, dynamic record, SqlTransaction transaction)
        {
            string query = @"
                INSERT INTO orders 
                ([index], [Order_ID], [Date], [Status], [Fulfilment], [Sales_Channel], [Ship_Service_Level], [Style], [SKU], 
                 [Category], [Size], [ASIN], [Courier_Status], [Qty], [Currency], [Amount], [Ship_City], [Ship_State], 
                 [Ship_Postal_Code], [Ship_Country], [Promotion_IDs], [B2B], [Fulfilled_By])
                VALUES 
                (@index, @Order_ID, @Date, @Status, @Fulfilment, @Sales_Channel, @Ship_Service_Level, @Style, @SKU, 
                 @Category, @Size, @ASIN, @Courier_Status, @Qty, @Currency, @Amount, @Ship_City, @Ship_State, 
                 @Ship_Postal_Code, @Ship_Country, @Promotion_IDs, @B2B, @Fulfilled_By);";

            using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
            {
                AddParameters(cmd, record);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds parameters to the SQL command.
        /// </summary>
        private static void AddParameters(SqlCommand command, dynamic record)
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