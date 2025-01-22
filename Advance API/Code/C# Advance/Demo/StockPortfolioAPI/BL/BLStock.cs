using MySql.Data.MySqlClient;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using StockPortfolioAPI.Extension;
using StockPortfolioAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack.OrmLite;
using StockPortfolioAPI.ORMLite;

namespace StockPortfolioAPI.BL
{
    /// <summary>
    /// Business logic class for handling stock-related operations such as fetching, saving, and validating stocks.
    /// </summary>
    public class BLStock
    {
        #region Properties

        /// <summary>
        /// Gets or sets the type of entry operation (Add, Edit, Delete).
        /// </summary>
        public EnmEntryType Type { get; set; }

        /// <summary>
        /// Response object to hold the result of operations.
        /// </summary>
        public Response objResponse;

        /// <summary>
        /// Stock object representing the stock entity.
        /// </summary>
        public STK01 objSTK01;

        /// <summary>
        /// Stock ID for operations.
        /// </summary>
        public int id;

        /// <summary>
        /// Converter object for converting between DTO and POCO.
        /// </summary>
        public BLConverter objBLConverter;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BLStock"/> class.
        /// </summary>
        public BLStock()
        {
            objBLConverter = new BLConverter();
            objResponse = new Response();
        }

        /// <summary>
        /// Retrieves all stocks.
        /// </summary>
        /// <returns>A list of all stocks.</returns>
        public List<STK01> GetAllStocks()
        {
            using (IDbConnection objIDbConnection = OrmLiteHelper.OpenConnection())
            {
                return objIDbConnection.Select<STK01>();
            }
        }

        /// <summary>
        /// Saves the stock (insert or update) into the database.
        /// </summary>
        /// <returns>A response object indicating success or failure of the operation.</returns>
        public Response Save()
        {
            int result;
            string query = "";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            try
            {
                // Common parameters for both Insert and Update
                parameters.Add("K01F02", objSTK01.K01F02); // Stock name
                parameters.Add("K01F03", objSTK01.K01F03); // Other fields
                parameters.Add("K01F04", objSTK01.K01F04); // Additional field
                parameters.Add("K01F05", objSTK01.K01F05); // Additional field
                parameters.Add("K01F06", objSTK01.K01F06.ToString("yyyy-MM-dd HH:mm:ss")); // CreatedAt
                parameters.Add("K01F07", objSTK01.K01F07.ToString("yyyy-MM-dd HH:mm:ss")); // ModifiedAt

                if (Type == EnmEntryType.A) // Insert
                {
                    query = DynamicQueryHelper.GenerateInsertQuery("STK01", parameters);
                }
                else if (Type == EnmEntryType.E) // Update
                {
                    parameters.Add("K01F01", objSTK01.K01F01); // ID for WHERE clause
                    query = DynamicQueryHelper.GenerateUpdateQuery("STK01", parameters, "K01F01");
                }

                using (MySqlConnection objMySqlConnection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    objMySqlConnection.Open();
                    using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                    {
                        foreach (var param in parameters)
                        {
                            objMySqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        result = objMySqlCommand.ExecuteNonQuery();

                        if (result > 0)
                        {
                            objResponse.IsError = false;
                            objResponse.Message = Type == EnmEntryType.A ? "Record inserted successfully." : "Record updated successfully.";
                        }
                        else
                        {
                            objResponse.IsError = true;
                            objResponse.Message = "No rows affected.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"An error occurred: {ex.Message}";
            }

            return objResponse;
        }

        /// <summary>
        /// Prepares the stock data for saving (insert or update).
        /// </summary>
        /// <param name="objDTOSTK01">The stock data transfer object.</param>
        public void PreSave(DTOSTK01 objDTOSTK01)
        {
            objSTK01 = objDTOSTK01.Convert<STK01>();

            // Set ModifiedAt for all operations
            objSTK01.K01F07 = DateTime.Now;

            // Set CreatedAt only for Insert operations
            if (Type == EnmEntryType.A)
            {
                objSTK01.K01F06 = DateTime.Now;
            }

            // Set the ID for Update operations
            if (Type == EnmEntryType.E)
            {
                id = objSTK01.K01F01;
            }

            // Validate the ID for Update
            if (Type == EnmEntryType.E && id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Enter Correct Id";
            }
        }

        /// <summary>
        /// Validates the stock data before saving.
        /// </summary>
        /// <returns>A response object indicating success or failure of the validation.</returns>
        public Response Validation()
        {
            if (Type == EnmEntryType.E && id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Enter Correct Id";
                return objResponse;
            }
            // If everything is fine, allow the update
            return objResponse;
        }
    }
}