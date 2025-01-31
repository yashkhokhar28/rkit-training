using MySql.Data.MySqlClient;
using StockPortfolioAPI.Helpers;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using System;
using System.Collections.Generic;
using StockPortfolioAPI.Extension;

namespace StockPortfolioAPI.BL
{
    /// <summary>
    /// Business logic class for handling stock transactions such as buying and selling stocks.
    /// </summary>
    public class BLTransaction
    {
        /// <summary>
        /// Gets or sets the type of entry operation (Add, Edit, Delete).
        /// </summary>
        public EnmEntryType Type { get; set; }

        /// <summary>
        /// Response object to hold the result of operations.
        /// </summary>
        public Response objResponse;

        /// <summary>
        /// Transaction object representing the transaction entity.
        /// </summary>
        public TRN01 objTRN01;

        /// <summary>
        /// Transaction ID for operations.
        /// </summary>
        public int id;

        /// <summary>
        /// Converter object for converting between DTO and POCO.
        /// </summary>
        public BLConverter objBLConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BLTransaction"/> class.
        /// </summary>
        public BLTransaction()
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        /// <summary>
        /// Saves the transaction into the database.
        /// </summary>
        /// <returns>A response object indicating success or failure of the operation.</returns>
        public Response Save()
        {
            int result;
            string query = "";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            try
            {
                // Common parameters for Insert
                parameters.Add("N01F02", objTRN01.N01F02);  // UserId
                parameters.Add("N01F03", objTRN01.N01F03);  // StockId
                parameters.Add("N01F04", objTRN01.N01F04);  // Quantity
                parameters.Add("N01F05", objTRN01.N01F05);  // PriceAtTransaction
                parameters.Add("N01F06", objTRN01.N01F06);  // TransactionType (Buy/Sell)
                parameters.Add("N01F07", objTRN01.N01F07.ToString("yyyy-MM-dd HH:mm:ss"));  // TransactionDate

                // Generate Insert Query dynamically
                query = DynamicQueryHelper.GenerateInsertQuery("TRN01", parameters);

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
                            objResponse.Message = "Transaction recorded successfully.";
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
            UpdatePortfolioAfterTransaction();
            UpdatePortfolioIndividually();
            return objResponse;
        }

        /// <summary>
        /// Deletes a transaction from the database.
        /// </summary>
        /// <param name="id">The ID of the transaction to delete.</param>
        /// <returns>A response object indicating success or failure of the operation.</returns>
        public Response Delete(int id)
        {
            string query = "";
            var whereConditions = new Dictionary<string, object> { { "N01F01", id } };

            try
            {
                query = DynamicQueryHelper.GenerateDeleteQuery("TRN01", whereConditions);

                using (MySqlConnection objMySqlConnection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    objMySqlConnection.Open();
                    using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                    {
                        foreach (var param in whereConditions)
                        {
                            objMySqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        int rowsAffected = objMySqlCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            objResponse.IsError = false;
                            objResponse.Message = "Transaction deleted successfully.";
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
        /// Prepares transaction details (Validate and set price).
        /// </summary>
        /// <param name="objDTOTRN01">The transaction data transfer object.</param>
        /// <param name="userID">The user ID for the transaction.</param>
        public void PreSave(DTOTRN01 objDTOTRN01, int userID)
        {
            objTRN01 = objDTOTRN01.Convert<TRN01>();
            objTRN01.N01F02 = userID;  // Set the UserId for the transaction

            // Set the transaction type (Buy or Sell)
            if (objDTOTRN01.N01F06 == EnmTransactionType.Sell)  // 0 for Sell
            {
                objTRN01.N01F06 = "Sell";
            }
            else if (objDTOTRN01.N01F06 == EnmTransactionType.Buy)  // 1 for Buy
            {
                objTRN01.N01F06 = "Buy";
            }

            // Get the current price of the stock at the time of the transaction
            string priceQuery = "SELECT K01F04 FROM STK01 WHERE K01F01 = @StockId";  // Assuming K01F04 is the stock price
            objTRN01.N01F05 = GetPriceOfStock(priceQuery, objTRN01.N01F03);  // Set the price

            // Set the transaction timestamp
            objTRN01.N01F07 = DateTime.Now;
        }

        /// <summary>
        /// Validates if the portfolio and stock exist, based on their IDs.
        /// </summary>
        /// <returns>A response object indicating success or failure of the validation.</returns>
        public Response Validation()
        {
            if (objResponse.IsError)
            {
                return objResponse;
            }

            // Validate if the User exists (N01F02 is the UserId)
            string checkQuery = "SELECT COUNT(1) FROM USR01 WHERE R01F01 = @UserId"; // R01F01 is the UserId column in the USR01 table
            if (!CheckIfExists(checkQuery, objTRN01.N01F02))  // N01F02 is the UserId in the TRN01 table
            {
                objResponse.IsError = true;
                objResponse.Message = "User not found.";
                return objResponse;
            }

            return objResponse;
        }

        /// <summary>
        /// Helper method to check if a record exists based on the query and the ID passed.
        /// </summary>
        /// <param name="query">The query to check the record existence.</param>
        /// <param name="id">The ID to check.</param>
        /// <returns>True if the record exists, otherwise false.</returns>
        private bool CheckIfExists(string query, int id)
        {
            using (MySqlConnection connection = new MySqlConnection(BLConnection.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Pass the dynamic parameter based on the check (UserId or StockId)
                    command.Parameters.AddWithValue("@UserId", id); // This will be replaced with either UserId or StockId
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        /// <summary>
        /// Gets the stock price by StockId.
        /// </summary>
        /// <param name="query">The query to get the stock price.</param>
        /// <param name="id">The stock ID.</param>
        /// <returns>The stock price.</returns>
        private decimal GetPriceOfStock(string query, int id)
        {
            decimal price = 0.0m;
            using (MySqlConnection connection = new MySqlConnection(BLConnection.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StockId", id);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        price = Convert.ToDecimal(result);
                    }
                }
            }
            return price;
        }

        /// <summary>
        /// Calculates the valuation (price * quantity) of the transaction.
        /// </summary>
        /// <returns>The calculated valuation.</returns>
        private decimal CalculateValuation()
        {
            return objTRN01.N01F04 * objTRN01.N01F05;  // Quantity * PriceAtTransaction
        }

        /// <summary>
        /// Updates the portfolio's total value after a transaction (Buy/Sell).
        /// </summary>
        /// <returns>A response object indicating success or failure of the operation.</returns>
        public Response UpdatePortfolioAfterTransaction()
        {
            decimal transactionValue = CalculateValuation(); // Calculate transaction value (price * quantity)
            string updateQuery = "";

            // If it's a Buy transaction, increase the portfolio's total value
            if (objTRN01.N01F06 == "Buy")
            {
                updateQuery = "UPDATE PRT01 SET T01F04 = T01F04 + @TransactionValue WHERE T01F02 = @PortfolioId";
            }
            // If it's a Sell transaction, decrease the portfolio's total value
            else if (objTRN01.N01F06 == "Sell")
            {
                updateQuery = "UPDATE PRT01 SET T01F04 = T01F04 - @TransactionValue WHERE T01F02 = @PortfolioId";
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        // Pass the quantity and transaction value to the query
                        command.Parameters.AddWithValue("@TransactionValue", transactionValue);
                        command.Parameters.AddWithValue("@PortfolioId", objTRN01.N01F02); // Use PortfolioId for the update

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            objResponse.IsError = false;
                            objResponse.Message = "Portfolio's total value updated successfully.";
                            objResponse.Data = objBLConverter.ObjectToDataTable(new { transactionValue });
                        }
                        else
                        {
                            objResponse.IsError = true;
                            objResponse.Message = "Portfolio update failed.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"An error occurred while updating the portfolio's total value: {ex.Message}";
            }

            return objResponse;
        }

        /// <summary>
        /// Updates the portfolio's stock quantity and value after a transaction (Buy/Sell).
        /// </summary>
        /// <returns>A response object indicating success or failure of the operation.</returns>
        public Response UpdatePortfolioIndividually()
        {
            decimal transactionValue = CalculateValuation(); // Calculate transaction value (price * quantity)
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    connection.Open();

                    // Check if the stock already exists in the portfolio
                    var whereConditions = new Dictionary<string, object>
                    {
                        { "S01F02", objTRN01.N01F02 }, // UserID
                        { "S01F03", objTRN01.N01F03 } // StockId
                    };
                    string checkQuery = DynamicQueryHelper.GenerateSelectQuery("PTS01", whereConditions);
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        foreach (var param in whereConditions)
                        {
                            checkCommand.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        object result = checkCommand.ExecuteScalar();
                        bool stockExists = result != null && Convert.ToInt32(result) > 0;

                        if (stockExists)
                        {
                            // Update existing stock record in the portfolio
                            string updateQuery = @"
    UPDATE PTS01 
    SET 
        S01F04 = S01F04 + CASE WHEN @TransactionType = 'Buy' THEN @Quantity ELSE -@Quantity END, -- Adjust quantity
        S01F07 = S01F04 * S01F06, -- Total valuation (Quantity * Current Value)
        S01F09 = @ModifiedAt -- Update timestamp
    WHERE S01F02 = @UserID AND S01F03 = @StockId";

                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@TransactionType", objTRN01.N01F06); // "Buy" or "Sell"
                                updateCommand.Parameters.AddWithValue("@Quantity", objTRN01.N01F04); // Quantity
                                updateCommand.Parameters.AddWithValue("@ModifiedAt", DateTime.Now); // ModifiedAt
                                updateCommand.Parameters.AddWithValue("@UserID", objTRN01.N01F02); // UserId
                                updateCommand.Parameters.AddWithValue("@StockId", objTRN01.N01F03); // StockId

                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    objResponse.IsError = false;
                                    objResponse.Message = "Portfolio stock updated successfully.";
                                }
                                else
                                {
                                    objResponse.IsError = true;
                                    objResponse.Message = "Portfolio stock update failed.";
                                }
                            }
                        }
                        else
                        {
                            // Stock does not exist in portfolio, insert it
                            parameters.Clear();
                            parameters.Add("S01F02", objTRN01.N01F02); // user ID
                            parameters.Add("S01F03", objTRN01.N01F03); // Stock ID
                            parameters.Add("S01F04", objTRN01.N01F04); // Quantity
                            parameters.Add("S01F05", objTRN01.N01F05); // Purchase Price
                            parameters.Add("S01F06", objTRN01.N01F05); // User ID
                            parameters.Add("S01F07", transactionValue); // Total Value
                            parameters.Add("S01F08", DateTime.Now); // CreatedAt
                            parameters.Add("S01F09", DateTime.Now); // ModifiedAt

                            string insertQuery = DynamicQueryHelper.GenerateInsertQuery("PTS01", parameters);
                            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                            {
                                foreach (var param in parameters)
                                {
                                    insertCommand.Parameters.AddWithValue(param.Key, param.Value);
                                }

                                int rowsAffected = insertCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    objResponse.IsError = false;
                                    objResponse.Message = "Portfolio stock added successfully.";
                                }
                                else
                                {
                                    objResponse.IsError = true;
                                    objResponse.Message = "Failed to add stock to portfolio.";
                                }
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