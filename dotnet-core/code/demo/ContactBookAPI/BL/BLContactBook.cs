﻿using ContactBookAPI.Models;
using ContactBookAPI.Models.DTO;
using ContactBookAPI.Models.ENUM;
using ContactBookAPI.Models.POCO;
using ContactBookAPI.Extension;
using MySql.Data.MySqlClient;

namespace ContactBookAPI.BL
{
    public class BLContactBook
    {
        /// <summary>
        /// Database connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Entry type for data
        /// </summary>
        public EnmEntryType Type { get; set; }

        /// <summary>
        /// Response object to hold result data
        /// </summary>
        public Response objResponse;

        /// <summary>
        /// Contact data model
        /// </summary>
        public CNT01 objCNT01;

        /// <summary>
        /// Contact ID (if applicable)
        /// </summary>
        public int id;

        /// <summary>
        /// Converter object for data transformation
        /// </summary>
        public BLConverter objBLConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BLContactBook"/> class.
        /// </summary>
        public BLContactBook()
        {
            _connectionString = BLConnection.ConnectionString;
            objBLConverter = new BLConverter();
            objResponse = new Response();
        }

        /// <summary>
        /// Retrieves all contacts from the database.
        /// </summary>
        /// <returns>A list of contact objects.</returns>
        public List<CNT01> GetAllContacts()
        {
            string query = DynamicQueryHelper.GenerateSelectQuery("CNT01");
            List<CNT01> lstContacts = new List<CNT01>();

            try
            {
                using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
                {
                    objMySqlConnection.Open();
                    using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                    {
                        using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                        {
                            if (objMySqlDataReader.HasRows)
                            {
                                while (objMySqlDataReader.Read())
                                {
                                    lstContacts.Add(new CNT01
                                    {
                                        T01F01 = objMySqlDataReader.GetInt32("T01F01"),
                                        T01F02 = objMySqlDataReader.GetString("T01F02"),
                                        T01F03 = objMySqlDataReader.GetString("T01F03"),
                                        T01F04 = objMySqlDataReader.GetString("T01F04"),
                                        T01F05 = objMySqlDataReader.GetString("T01F05"),
                                        T01F06 = objMySqlDataReader.GetString("T01F06")
                                    });
                                }
                            }
                            else
                            {
                                Console.WriteLine("No rows found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return lstContacts;
        }

        /// <summary>
        /// Retrieves a contact by ID from the database.
        /// </summary>
        /// <param name="id">The contact ID.</param>
        /// <returns>The contact object if found, otherwise null.</returns>
        public CNT01 GetUserByID(int id)
        {
            // Create the query with the where condition
            Dictionary<string, object> whereCondition = new Dictionary<string, object>
            {
                { "T01F01", id }
            };
            string query = DynamicQueryHelper.GenerateSelectQuery("CNT01", whereCondition);

            CNT01 objCNT01 = null;

            try
            {
                using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
                {
                    objMySqlConnection.Open();
                    using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                    {
                        foreach (var param in whereCondition)
                        {
                            objMySqlCommand.Parameters.AddWithValue($"@{param.Key}", param.Value);
                        }
                        using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                        {

                            if (objMySqlDataReader.Read())
                            {
                                // Map the reader data to the CNT01 object
                                objCNT01 = new CNT01
                                {
                                    T01F01 = objMySqlDataReader.GetInt32("T01F01"),
                                    T01F02 = objMySqlDataReader.GetString("T01F02"),
                                    T01F03 = objMySqlDataReader.GetString("T01F03"),
                                    T01F04 = objMySqlDataReader.GetString("T01F04"),
                                    T01F05 = objMySqlDataReader.GetString("T01F05"),
                                    T01F06 = objMySqlDataReader.GetString("T01F06")
                                };
                            }
                            else
                            {
                                Console.WriteLine("No rows found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return objCNT01;
        }

        /// <summary>
        /// Deletes a contact by ID from the database.
        /// </summary>
        /// <param name="id">The contact ID.</param>
        /// <returns>The number of rows affected.</returns>
        public int Delete(int id)
        {
            string query = "";
            int rowsAffected = 0;
            Dictionary<string, object> whereConditions = new Dictionary<string, object> { { "T01F01", id } };

            try
            {
                query = DynamicQueryHelper.GenerateDeleteQuery("CNT01", whereConditions);

                using (MySqlConnection objMySqlConnection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    objMySqlConnection.Open();
                    using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                    {
                        foreach (var param in whereConditions)
                        {
                            objMySqlCommand.Parameters.AddWithValue($"@{param.Key}", param.Value);
                        }
                        rowsAffected = objMySqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return rowsAffected;
        }

        /// <summary>
        /// Saves the contact data to the database.
        /// </summary>
        /// <returns>The response object with the result of the save operation.</returns>
        public Response Save()
        {
            int result = 0;
            string query = "";
            var parameters = new Dictionary<string, object>();
            Dictionary<string, object> whereConditions = null;

            try
            {
                // Common parameters for both Insert and Update
                parameters.Add("T01F02", objCNT01.T01F02); // First Name
                parameters.Add("T01F03", objCNT01.T01F03); // Last Name
                parameters.Add("T01F04", objCNT01.T01F04); // Email Address
                parameters.Add("T01F05", objCNT01.T01F05); // Phone Number
                parameters.Add("T01F06", objCNT01.T01F06); // Address

                if (Type == EnmEntryType.A) // Insert
                {
                    query = DynamicQueryHelper.GenerateInsertQuery("CNT01", parameters);
                }
                else if (Type == EnmEntryType.E) // Update
                {
                    whereConditions = new Dictionary<string, object>
                    {
                        { "T01F01", objCNT01.T01F01 } // ID for WHERE clause
                    };
                    query = DynamicQueryHelper.GenerateUpdateQuery("CNT01", parameters, whereConditions);
                }

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue($"@{param.Key}", param.Value);
                        }

                        // Add parameters for WHERE conditions (if update)
                        if (whereConditions != null)
                        {
                            foreach (var whereParam in whereConditions)
                            {
                                command.Parameters.AddWithValue($"@{whereParam.Key}", whereParam.Value);
                            }
                        }

                        result = command.ExecuteNonQuery();

                        objResponse.IsError = result <= 0;
                        objResponse.Message = result > 0
                            ? (Type == EnmEntryType.A ? "Contact inserted successfully." : "Contact updated successfully.")
                            : "No rows affected.";
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
        /// Prepares the contact data for saving.
        /// </summary>
        /// <param name="objDTOCNT01">The contact DTO object.</param>
        public void PreSave(DTOCNT01 objDTOCNT01)
        {
            objCNT01 = objDTOCNT01.Convert<CNT01>();

            // Set the ID for Update operations
            if (Type == EnmEntryType.E)
            {
                id = objCNT01.T01F01;
            }

            // Validate the ID for Update
            if (Type == EnmEntryType.E && objCNT01.T01F01 <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Invalid Contact ID.";
            }
        }

        /// <summary>
        /// Validates the contact data before saving.
        /// </summary>
        /// <returns>The response object with the result of the validation.</returns>
        public Response Validation()
        {
            if (Type == EnmEntryType.E && id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Enter Correct Id";
            }
            return objResponse;
        }
    }
}