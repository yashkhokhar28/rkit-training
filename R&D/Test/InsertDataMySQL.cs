using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace Test
{
    public class InsertDataMySQL
    {
        public static void InsertDataFromCsv(int number, string csvFilePath, string server, string userId, string password)
        {
            try
            {
                // Read CSV data
                CsvConfiguration objCsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // Ensure the CSV has headers
                };

                using (StreamReader objStreamReader = new StreamReader(csvFilePath))
                {
                    using (CsvReader objCsvReader = new CsvReader(objStreamReader, objCsvConfiguration))
                    {
                        IEnumerable<dynamic> records = objCsvReader.GetRecords<dynamic>();

                        // Loop through all databases
                        for (int i = 1; i <= number; i++)
                        {
                            string dbName = $"test_db_{i}";
                            var connectionString = $"Server={server};Database={dbName};User ID={userId};Password={password};";

                            using (MySqlConnection objMySqlConnection = new MySqlConnection(connectionString))
                            {
                                objMySqlConnection.Open();
                                Console.WriteLine($"Connected to MySQL database: {dbName}");

                                foreach (dynamic record in records)
                                {
                                    try
                                    {
                                        InsertRecord(objMySqlConnection, record);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error inserting record into {dbName}: {ex.Message}");
                                    }
                                }

                                Console.WriteLine($"Data inserted into {dbName}");
                            }
                        }

                        Console.WriteLine("Data insertion completed for all databases.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while processing the CSV: {ex.Message}");
            }
        }

        private static void InsertRecord(MySqlConnection connection, dynamic record)
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

            using (MySqlCommand objMySqlCommand = new MySqlCommand(query, connection))
            {
                AddParameters(objMySqlCommand, record);
                objMySqlCommand.ExecuteNonQuery();
            }
        }

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