using CRUDDemo.Extension;
using CRUDDemo.Models;
using CRUDDemo.Models.DTO;
using CRUDDemo.Models.ENUM;
using CRUDDemo.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDDemo.BL
{
    /// <summary>
    /// Business Logic class for handling CRUD operations on Employee data.
    /// Implements the IDataHandlerService interface for DTOEMP01.
    /// </summary>
    public class BLEmployee : IDataHandlerService<DTOEMP01>
    {
        #region Properties

        /// <summary>
        /// Defines the type of operation being performed (Add or Edit).
        /// </summary>
        public EnmEntryType Type { get; set; }

        /// <summary>
        /// The connection string used to connect to the database.
        /// </summary>
        public string connectionString;

        /// <summary>
        /// The Employee model object used during operations.
        /// </summary>
        public EMP01 objEMP01;

        /// <summary>
        /// The Response object to capture operation results and errors.
        /// </summary>
        public Response objResponse;

        /// <summary>
        /// The ID of the Employee, used for Edit or Delete operations.
        /// </summary>
        public int id;

        /// <summary>
        /// Initializes the BLEmployee class with the default connection string and response object.
        /// </summary>
        public BLEmployee()
        {
            connectionString = HttpContext.Current.Application["ConnectionString"] as string;
            objResponse = new Response();
        }

        #endregion

        #region CRUD Operations

        #region GetAll
        /// <summary>
        /// Retrieves all employees from the EMP01 table.
        /// </summary>
        /// <returns>A list of Employee objects.</returns>
        public List<EMP01> GetAll()
        {
            List<EMP01> lstEmployees = new List<EMP01>();

            // Define the query separately
            string query = "SELECT P01F01, P01F02, P01F03, P01F04, P01F05, P01F06 FROM EMP01";

            // Open connection and execute query
            using (MySqlConnection objMySqlConnection = new MySqlConnection(connectionString))
            {
                objMySqlConnection.Open();
                using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                {
                    using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                    {
                        while (objMySqlDataReader.Read())
                        {
                            lstEmployees.Add(new EMP01
                            {
                                P01F01 = objMySqlDataReader.GetInt32("P01F01"),
                                P01F02 = objMySqlDataReader.GetString("P01F02"),
                                P01F03 = objMySqlDataReader.GetInt32("P01F03"),
                                P01F04 = objMySqlDataReader.GetString("P01F04"),
                                P01F05 = objMySqlDataReader.GetDateTime("P01F05"),
                                P01F06 = objMySqlDataReader.GetDateTime("P01F06")
                            });
                        }
                    }
                }
            }

            return lstEmployees;
        }
        #endregion

        #region Get
        /// <summary>
        /// Retrieves a single employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The Employee object if found, or null if not.</returns>
        public EMP01 Get(int id)
        {
            EMP01 objEMP01 = null;

            // Define the query separately
            string query = string.Format("SELECT P01F01, P01F02, P01F03, P01F04, P01F05, P01F06 FROM EMP01 WHERE P01F01 = {0}", id);

            using (MySqlConnection objMySqlConnection = new MySqlConnection(connectionString))
            {
                objMySqlConnection.Open();

                using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                {
                    // Add the parameter
                    objMySqlCommand.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                    {
                        if (objMySqlDataReader.Read())
                        {
                            objEMP01 = new EMP01()
                            {
                                P01F01 = objMySqlDataReader.GetInt32("P01F01"),
                                P01F02 = objMySqlDataReader.GetString("P01F02"),
                                P01F03 = objMySqlDataReader.GetInt32("P01F03"),
                                P01F04 = objMySqlDataReader.GetString("P01F04"),
                                P01F05 = objMySqlDataReader.GetDateTime("P01F05"),
                                P01F06 = objMySqlDataReader.GetDateTime("P01F06")
                            };
                        }
                    }
                }
            }

            return objEMP01;
        }
        #endregion

        #region PreSave
        /// <summary>
        /// Prepares the Employee object for saving (Insert or Update).
        /// Sets required fields like EmployeeStatus, CreatedAt, and ModifiedAt.
        /// Validates the ID for Update operations.
        /// </summary>
        /// <param name="objDTOEMP01">The DTO object containing Employee data.</param>
        public void PreSave(DTOEMP01 objDTOEMP01)
        {
            objEMP01 = objDTOEMP01.Convert<EMP01>();

            // Determine EmployeeStatus based on Age
            objEMP01.P01F04 = objEMP01.P01F03 >= 18 ? "Adult" : "Minor";

            // Set ModifiedAt for all operations
            objEMP01.P01F06 = DateTime.Now;

            // Set CreatedAt only for Insert operations
            if (Type == EnmEntryType.A)
            {
                objEMP01.P01F05 = DateTime.Now;
            }

            // Set the ID for Update operations
            if (Type == EnmEntryType.E)
            {
                id = objEMP01.P01F01;
            }

            // Validate the ID for Update
            if (Type == EnmEntryType.E && id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Enter Correct Id";
            }
        }
        #endregion

        /// <summary>
        /// Validates the object before saving or updating.
        /// Ensures that required conditions for the operation are met.
        /// </summary>
        /// <returns>A Response object indicating success or errors.</returns>
        public Response Validation()
        {
            if (Type == EnmEntryType.E && id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Enter Correct Id";
            }
            return objResponse;
        }

        #region Save
        /// <summary>
        /// Executes Insert or Update operations on the EMP01 table.
        /// </summary>
        /// <returns>A Response object indicating success or errors.</returns>
        public Response Save()
        {
            try
            {
                using (MySqlConnection objMySqlConnection = new MySqlConnection(connectionString))
                {
                    objMySqlConnection.Open();

                    if (Type == EnmEntryType.A) // Insert
                    {
                        // Using string.Format for query formatting (not recommended in practice)
                        string insertQuery = string.Format("INSERT INTO EMP01 (P01F02, P01F03, P01F04, P01F05, P01F06) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                            objEMP01.P01F02, objEMP01.P01F03, objEMP01.P01F04, objEMP01.P01F05, objEMP01.P01F06);

                        using (MySqlCommand objMySqlCommand = new MySqlCommand(insertQuery, objMySqlConnection))
                        {
                            objMySqlCommand.ExecuteNonQuery();
                        }
                        objResponse.Message = "Data Added Successfully!";
                    }
                    else if (Type == EnmEntryType.E) // Update
                    {
                        // Using string.Format for query formatting (not recommended in practice)
                        string updateQuery = string.Format("UPDATE EMP01 SET P01F02 = '{0}', P01F03 = '{1}', P01F04 = '{2}', P01F06 = '{3}' WHERE P01F01 = '{4}'",
                            objEMP01.P01F02, objEMP01.P01F03, objEMP01.P01F04, objEMP01.P01F06, objEMP01.P01F01);

                        using (MySqlCommand objMySqlCommand = new MySqlCommand(updateQuery, objMySqlConnection))
                        {
                            objMySqlCommand.ExecuteNonQuery();
                        }
                        objResponse.Message = "Data Updated Successfully!";
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
        #endregion

        #region Delete
        /// <summary>
        /// Retrieves an Employee before deletion to validate its existence.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>The Employee object if found.</returns>
        public EMP01 PreDelete(int id)
        {
            return Get(id);
        }

        /// <summary>
        /// Validates the Employee object before deletion.
        /// </summary>
        /// <param name="objEMP01">The Employee object to validate.</param>
        /// <returns>A Response object indicating success or errors.</returns>
        public Response ValidationOnDelete(EMP01 objEMP01)
        {
            if (objEMP01 == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "Employee Not Found";
            }
            return objResponse;
        }

        /// <summary>
        /// Deletes an Employee from the EMP01 table.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>The number of rows affected.</returns>
        public int Delete(int id)
        {
            int count = 0;
            EMP01 objEMP01 = PreDelete(id);
            Response objResponse = ValidationOnDelete(objEMP01);
            if (objResponse.IsError)
            {
                return count;
            }

            using (MySqlConnection objMySqlConnection = new MySqlConnection(connectionString))
            {
                objMySqlConnection.Open();

                // Using string.Format for query formatting (not recommended for SQL queries)
                string deleteQuery = string.Format("DELETE FROM EMP01 WHERE P01F01 = {0}", id);

                using (MySqlCommand objMySqlCommand = new MySqlCommand(deleteQuery, objMySqlConnection))
                {
                    count = objMySqlCommand.ExecuteNonQuery();
                }
            }

            return count;
        }
        #endregion

        #endregion
    }
}