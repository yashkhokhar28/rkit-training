using CsvHelper;
using CsvHelper.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class InsertData
    {
        public static void InsertDataFromCsv(int number, string csvFilePath, string server, string userID, string password)
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
                            string connectionString = $"Server={server};Database={dbName};User ID={userID};Password={password};";

                            using (MySqlConnection objMySqlConnection = new MySqlConnection(connectionString))
                            {
                                objMySqlConnection.Open();

                                foreach (dynamic record in records)
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

                                    MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);

                                    // Handle empty/null values and add parameters
                                    objMySqlCommand.Parameters.AddWithValue("@index", string.IsNullOrEmpty(record.index) ? DBNull.Value : Convert.ToInt32(record.index));
                                    objMySqlCommand.Parameters.AddWithValue("@Order_ID", record.Order_ID ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Date", record.Date ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Status", record.Status ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Fulfilment", record.Fulfilment ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Sales_Channel", record.Sales_Channel ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Ship_Service_Level", record.ship_service_level ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Style", record.Style ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@SKU", record.SKU ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Category", record.Category ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Size", record.Size ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@ASIN", record.ASIN ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Courier_Status", record.Courier_Status ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Qty", string.IsNullOrEmpty(record.Qty) ? DBNull.Value : Convert.ToInt32(record.Qty));
                                    objMySqlCommand.Parameters.AddWithValue("@Currency", record.currency ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Amount", string.IsNullOrEmpty(record.Amount) ? DBNull.Value : Convert.ToDecimal(record.Amount));
                                    objMySqlCommand.Parameters.AddWithValue("@Ship_City", record.ship_city ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Ship_State", record.ship_state ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Ship_Postal_Code", record.ship_postal_code ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Ship_Country", record.ship_country ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Promotion_IDs", record.promotion_ids ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@B2B", record.B2B ?? DBNull.Value);
                                    objMySqlCommand.Parameters.AddWithValue("@Fulfilled_By", record.fulfilled_by ?? DBNull.Value);

                                    objMySqlCommand.ExecuteNonQuery();
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
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
