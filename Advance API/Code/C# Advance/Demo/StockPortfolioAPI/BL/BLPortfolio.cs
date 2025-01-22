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

namespace StockPortfolioAPI.BL
{
    /// <summary>
    /// Business logic class for handling portfolio-related operations such as fetching, saving, and validating portfolios.
    /// </summary>
    public class BLPortfolio
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
        /// Portfolio object representing the portfolio entity.
        /// </summary>
        public PRT01 objPRT01;

        /// <summary>
        /// Portfolio ID for operations.
        /// </summary>
        public int id;

        /// <summary>
        /// Converter object for converting between DTO and POCO.
        /// </summary>
        public BLConverter objBLConverter;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BLPortfolio"/> class.
        /// </summary>
        public BLPortfolio()
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        /// <summary>
        /// Saves the portfolio (insert or update) into the database.
        /// </summary>
        /// <returns>A response object indicating success or failure of the operation.</returns>
        public Response Save()
        {
            string query = "";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            // Prepare parameters for the query
            parameters.Add("T01F02", objPRT01.T01F02);  // UserId
            parameters.Add("T01F03", objPRT01.T01F03);  // PortfolioName
            parameters.Add("T01F06", objPRT01.T01F06.ToString("yyyy-MM-dd HH:mm:ss")); // ModifiedAt

            if (Type == EnmEntryType.A) // Insert
            {
                parameters.Add("T01F05", objPRT01.T01F05.ToString("yyyy-MM-dd HH:mm:ss")); // CreatedAt
                query = DynamicQueryHelper.GenerateInsertQuery("PRT01", parameters);
            }
            else if (Type == EnmEntryType.E) // Update
            {
                parameters.Add("T01F01", objPRT01.T01F01); // ID for WHERE clause
                query = DynamicQueryHelper.GenerateUpdateQuery("PRT01", parameters, "T01F01");
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            objResponse.IsError = false;
                            objResponse.Message = Type == EnmEntryType.A ? "Portfolio saved successfully." : "Portfolio updated successfully.";
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
        /// Prepares the portfolio data for saving (insert or update).
        /// </summary>
        /// <param name="objDTOPRT01">The portfolio data transfer object.</param>
        /// <param name="userID">The user ID for setting the portfolio owner.</param>
        public void PreSave(DTOPRT01 objDTOPRT01, int userID)
        {
            objPRT01 = objDTOPRT01.Convert<PRT01>();

            objPRT01.T01F02 = userID;  // Set the user ID

            // Set ModifiedAt for all operations
            objPRT01.T01F06 = DateTime.Now;

            // Set CreatedAt only for Insert operations
            if (Type == EnmEntryType.A)
            {
                objPRT01.T01F05 = DateTime.Now;
            }

            // Set the ID for Update operations
            if (Type == EnmEntryType.E)
            {
                id = objPRT01.T01F01;
            }

            // Validate the ID for Update
            if (Type == EnmEntryType.E && id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Enter Correct Id";
            }
        }

        /// <summary>
        /// Validates the portfolio data before saving.
        /// </summary>
        /// <returns>A response object indicating success or failure of the validation.</returns>
        public Response Validation()
        {
            if (Type == EnmEntryType.E && id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Enter Correct Id";
            }
            return objResponse;
        }

        /// <summary>
        /// Fetches the portfolio by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>A response object containing the portfolio data or an error message.</returns>
        public Response GetPortfolioByUserID(int userID)
        {
            try
            {
                // Generate the dynamic SELECT query to get the portfolio by user ID
                string query = DynamicQueryHelper.GenerateSelectQuery("PRT01", $"T01F02 = {userID}");

                using (MySqlConnection connection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);

                                objResponse.IsError = false;
                                objResponse.Data = dt;
                                objResponse.Message = "Portfolio fetched successfully.";
                            }
                            else
                            {
                                objResponse.IsError = true;
                                objResponse.Message = "No portfolio found for the user.";
                            }
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
    }
}