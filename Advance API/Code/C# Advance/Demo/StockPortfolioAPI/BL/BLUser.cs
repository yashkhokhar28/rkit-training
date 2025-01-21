using MySql.Data.MySqlClient;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using StockPortfolioAPI.Extension;
using StockPortfolioAPI.Security;
using System;
using StockPortfolioAPI.Helpers;

namespace StockPortfolioAPI.BL
{
    public class BLUser
    {
        #region Properties

        public EnmEntryType Type { get; set; }

        public Response objResponse;

        public USR01 objUSR01;

        public int id;

        public BLConverter objBLConverter;

        #endregion

        public BLUser()
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }


        public Response Save()
        {
            int result;
            // Hash the password
            string hashedPassword = HashHelper.ComputeSHA256Hash(objUSR01.R01F04);
            // Define the query separately
            string query = string.Format("INSERT INTO USR01 (R01F02, R01F03, R01F04, R01F06, R01F07) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", objUSR01.R01F02, objUSR01.R01F03, hashedPassword, objUSR01.R01F06.ToString("yyyy-MM-dd HH:mm:ss"), objUSR01.R01F07.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                using (MySqlConnection objMySqlConnection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    objMySqlConnection.Open();
                    if (Type == EnmEntryType.A) // Insert
                    {
                        using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                        {
                            using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                            {
                                result = objMySqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
            }
            return objResponse;
        }

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

        public Response Validation()
        {
            if (Type == EnmEntryType.E && id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Enter Correct Id";
            }
            return objResponse;
        }

        public string GetRole(string username)
        {
            string role;
            // Define the query separately
            string query = string.Format("SELECT R01F05 FROM USR01 WHERE R01F02 = {0}", username);

            // Open connection and execute query
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLConnection.ConnectionString))
            {
                objMySqlConnection.Open();
                using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                {
                    using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                    {
                        role = objMySqlDataReader.GetString(0);
                    }
                }
            }
            return role;
        }

        public Response Login(DTOUSR02 objDTOUSR02)
        {
            string query = string.Format("SELECT R01F01,R01F02,R01F04,R01F05 FROM USR01 WHERE R01F02 = '{0}'", objDTOUSR02.R01F02);
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
                                    DateTime expirationDate = DateTime.UtcNow.AddHours(1);
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
    }
}