using MySql.Data.MySqlClient;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using StockPortfolioAPI.Extension;
using StockPortfolioAPI.Security;
using System;
using StockPortfolioAPI.Helpers;
using System.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using StockPortfolioAPI.ORMLite;

namespace StockPortfolioAPI.BL
{
    /// <summary>
    /// Business logic class for user-related operations such as fetching, saving, deleting, and validating users.
    /// </summary>
    public class BLUser
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
        /// User object representing the user entity.
        /// </summary>
        public USR01 objUSR01;

        /// <summary>
        /// User ID for operations.
        /// </summary>
        public int id;

        /// <summary>
        /// Converter object for converting between DTO and POCO.
        /// </summary>
        public BLConverter objBLConverter;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BLUser"/> class.
        /// </summary>
        public BLUser()
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A response object containing the list of users or an error message.</returns>
        public Response GetAllUser()
        {
            try
            {
                using (IDbConnection objIDbConnection = OrmLiteHelper.OpenConnection())
                {
                    var users = objIDbConnection.Select<USR01>();
                    objResponse.IsError = false;
                    objResponse.Data = objBLConverter.ObjectToDataTable(users);
                    objResponse.Message = "Users fetched successfully.";
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
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A response object containing the user data or an error message.</returns>
        public Response GetUserByID(int id)
        {
            try
            {
                using (IDbConnection objIDbConnection = OrmLiteHelper.OpenConnection())
                {
                    var user = objIDbConnection.SingleById<USR01>(id);
                    if (user != null)
                    {
                        objResponse.IsError = false;
                        objResponse.Data = objBLConverter.ObjectToDataTable(user);
                        objResponse.Message = "User fetched successfully.";
                    }
                    else
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "User not found.";
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
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A response object indicating success or failure of the operation.</returns>
        public Response Delete(int id)
        {
            try
            {
                using (IDbConnection objIDbConnection = OrmLiteHelper.OpenConnection())
                {
                    int rowsAffected = objIDbConnection.Delete<USR01>(x => x.R01F01 == id);
                    if (rowsAffected > 0)
                    {
                        objResponse.IsError = false;
                        objResponse.Message = "User deleted successfully.";
                    }
                    else
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "User not found.";
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
        /// Saves a user (insert or update).
        /// </summary>
        /// <returns>A response object indicating success or failure of the operation.</returns>
        public Response Save()
        {
            int result = 0;
            string query = "";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            try
            {
                // Common parameters for both Insert and Update
                parameters.Add("R01F02", objUSR01.R01F02); // Name
                parameters.Add("R01F03", objUSR01.R01F03); // Other fields
                parameters.Add("R01F04", HashHelper.ComputeSHA256Hash(objUSR01.R01F04)); // Hashed password
                parameters.Add("R01F07", objUSR01.R01F07.ToString("yyyy-MM-dd HH:mm:ss")); // ModifiedAt

                if (Type == EnmEntryType.A) // Insert
                {
                    parameters.Add("R01F06", objUSR01.R01F06.ToString("yyyy-MM-dd HH:mm:ss")); // CreatedAt
                    query = DynamicQueryHelper.GenerateInsertQuery("USR01", parameters);
                }
                else if (Type == EnmEntryType.E) // Update
                {
                    parameters.Add("R01F01", objUSR01.R01F01); // ID for WHERE clause
                    query = DynamicQueryHelper.GenerateUpdateQuery("USR01", parameters, "R01F01");
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
        /// Prepares the user data for saving (insert or update).
        /// </summary>
        /// <param name="objDTOUSR01">The user data transfer object.</param>
        public void PreSave(DTOUSR01 objDTOUSR01)
        {
            objUSR01 = objDTOUSR01.Convert<USR01>();

            // Set ModifiedAt for all operations
            objUSR01.R01F07 = DateTime.Now;

            // Set CreatedAt only for Insert operations
            if (Type == EnmEntryType.A)
            {
                objUSR01.R01F06 = DateTime.Now;
            }

            // Set the ID for Update operations
            if (Type == EnmEntryType.E)
            {
                id = objUSR01.R01F01;
            }

            // Validate the ID for Update
            if (Type == EnmEntryType.E && id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Enter Correct Id";
            }
        }

        /// <summary>
        /// Validates the user data before saving.
        /// </summary>
        /// <param name="userID">The user ID for validation.</param>
        /// <returns>A response object indicating success or failure of the validation.</returns>
        public Response Validation(int userID)
        {
            // Ensure the ID is valid for update
            if (Type == EnmEntryType.E && id <= 0)
            {
                // Fetch the user to validate if it exists
                objResponse = GetUserByID(id);
                if (objResponse.IsError)  // If user is not found
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User not found.";
                    return objResponse;
                }

                // Assuming objResponse.Data contains a DataTable, we retrieve the first row
                DataTable userDataTable = (DataTable)objResponse.Data;

                // If DataTable is empty, return an error
                if (userDataTable.Rows.Count == 0)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User not found.";
                    return objResponse;
                }

                // Get the current user's ID and role from the first row in the DataTable
                int currentUserId = Convert.ToInt32(userDataTable.Rows[0]["R01F01"]);
                string role = userDataTable.Rows[0]["R01F05"].ToString();

                // Check if the user ID matches the one trying to make the update
                if (currentUserId != userID && role != EnmRoles.Admin.ToString())
                {
                    objResponse.IsError = true;
                    objResponse.Message = "You are not authorized to update this record.";
                    return objResponse;
                }
            }
            // If everything is fine, allow the update
            return objResponse;
        }

        /// <summary>
        /// Retrieves the role of a user by their username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The role of the user.</returns>
        public string GetRole(string username)
        {
            using (IDbConnection objIDbConnection = OrmLiteHelper.OpenConnection())
            {
                // Query the database for the role
                return objIDbConnection.Scalar<string>("SELECT R01F05 FROM USR01 WHERE R01F02 = @username", new { username });
            }
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="objDTOUSR02">The user login data transfer object.</param>
        /// <returns>A response object containing the JWT token and user information or an error message.</returns>
        public Response Login(DTOUSR02 objDTOUSR02)
        {
            string query = string.Format("SELECT R01F01, R01F02, R01F04, R01F05 FROM USR01 WHERE R01F02 = '{0}'", objDTOUSR02.R01F02);
            // Fetch user from DB
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLConnection.ConnectionString))
            {
                try
                {
                    objMySqlConnection.Open();

                    using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                    {
                        using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                        {
                            if (objMySqlDataReader.Read())
                            {
                                string storedPasswordHash = objMySqlDataReader["R01F04"].ToString();
                                string username = objMySqlDataReader["R01F02"].ToString();
                                string role = objMySqlDataReader["R01F05"].ToString();
                                int userID = Convert.ToInt32(objMySqlDataReader["R01F01"]);
                                // Verify the entered password with the stored password hash
                                if (HashHelper.VerifyPassword(objDTOUSR02.R01F04, storedPasswordHash))
                                {
                                    // Generate JWT token
                                    DateTime expirationDate = DateTime.UtcNow.AddYears(1);
                                    object objToken = JWTHelper.GenerateJWTToken(username, userID, role, 1);

                                    var objLoginData = new
                                    {
                                        Token = objToken,
                                        UserID = userID,
                                        Username = username,
                                        Role = role
                                    };
                                    // Return the token and user information
                                    objResponse.IsError = false;
                                    objResponse.Message = "Login successful.";
                                    objResponse.Data = objBLConverter.ObjectToDataTable(objLoginData);
                                    return objResponse;
                                }
                                else
                                {
                                    objResponse.IsError = true;
                                    objResponse.Message = "Invalid username or password.";
                                    return objResponse;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    objResponse.IsError = true;
                    objResponse.Message = $"An error occurred: {ex.Message}";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Retrieves the stock transactions of a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A response object containing the stock transactions or an error message.</returns>
        public Response GetUserStockTransactions(int userId)
        {
            try
            {
                string query = @"
            SELECT 
                u.R01F01 AS UserId, 
                t.N01F06 AS TransactionType, 
                SUM(t.N01F04) AS TotalQuantity,
                t.N01F07 AS TransactionDate,
                s.K01F02 AS StockSymbol,
                s.K01F03 AS StockName,
                t.N01F05 AS PriceAtTransaction
            FROM 
                USR01 u
            JOIN 
                TRN01 t ON u.R01F01 = t.N01F02  -- Join on User ID
            JOIN 
                STK01 s ON t.N01F03 = s.K01F01  -- Join on Stock ID
            WHERE 
                u.R01F01 = @UserId  -- Filter by specific UserId
            GROUP BY 
                u.R01F01, t.N01F06, t.N01F07, s.K01F02, s.K01F03, t.N01F05
            ORDER BY 
                t.N01F07 DESC;";  // Sorting by TransactionDate in descending order

                using (MySqlConnection conn = new MySqlConnection(BLConnection.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);  // Add the UserId parameter to the query

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Create a DataTable to store the result
                                DataTable dtTransactions = new DataTable();

                                // Define the columns of the DataTable
                                dtTransactions.Columns.Add("UserId", typeof(int));
                                dtTransactions.Columns.Add("TransactionType", typeof(string));
                                dtTransactions.Columns.Add("TotalQuantity", typeof(int));
                                dtTransactions.Columns.Add("TransactionDate", typeof(DateTime));
                                dtTransactions.Columns.Add("StockSymbol", typeof(string));
                                dtTransactions.Columns.Add("StockName", typeof(string));
                                dtTransactions.Columns.Add("PriceAtTransaction", typeof(decimal));

                                // Read data from the MySQL DataReader and fill the DataTable
                                while (reader.Read())
                                {
                                    DataRow row = dtTransactions.NewRow();
                                    row["UserId"] = reader.GetInt32("UserId");
                                    row["TransactionType"] = reader.GetString("TransactionType");
                                    row["TotalQuantity"] = reader.GetInt32("TotalQuantity");
                                    row["TransactionDate"] = reader.GetDateTime("TransactionDate");
                                    row["StockSymbol"] = reader.GetString("StockSymbol");
                                    row["StockName"] = reader.GetString("StockName");
                                    row["PriceAtTransaction"] = reader.GetDecimal("PriceAtTransaction");

                                    dtTransactions.Rows.Add(row);
                                }

                                objResponse.IsError = false;
                                objResponse.Message = "Stock transactions fetched successfully.";
                                objResponse.Data = dtTransactions;  // Return the DataTable
                            }
                            else
                            {
                                objResponse.IsError = true;
                                objResponse.Message = "No transactions found for this user.";
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