using MySql.Data.MySqlClient;
using EmployeeTaskManager.Helpers;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using EmployeeTaskManager.Models;
using System.Data;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using EmployeeTaskManager.Extension;

namespace EmployeeTaskManager.BL
{
    /// <summary>
    /// Business Logic class for handling authentication and user-related operations
    /// </summary>
    public class BLAuth
    {
        #region Properties

        /// <summary>
        /// Gets or sets the entry type (Add/Edit) for user operations
        /// </summary>
        public EnmEntryType EnmEntryType { get; set; }

        /// <summary>
        /// Response object containing operation results and data
        /// </summary>
        public Response objResponse;

        /// <summary>
        /// Database connection factory for creating database connections
        /// </summary>
        private readonly IDbConnectionFactory objIDbConnectionFactory;

        /// <summary>
        /// User entity object for database operations
        /// </summary>
        public USR01 objUSR01;

        /// <summary>
        /// User ID for operations
        /// </summary>
        public int id;

        /// <summary>
        /// Converter object for data type conversions
        /// </summary>
        public BLConverter objBLConverter;

        /// <summary>
        /// Configuration interface for accessing application settings
        /// </summary>
        private readonly IConfiguration _config;

        #endregion

        /// <summary>
        /// Constructor for BLAuth class
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory</param>
        /// <param name="config">Application configuration</param>
        public BLAuth(IDbConnectionFactory dbConnectionFactory, IConfiguration config)
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
            objIDbConnectionFactory = dbConnectionFactory;
            _config = config;
        }

        /// <summary>
        /// Retrieves all users from the database
        /// </summary>
        /// <returns>Response object containing user data or error information</returns>
        public Response GetAllUser()
        {
            try
            {
                using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
                {
                    var users = db.Select<USR01>();
                    objResponse.IsError = false;
                    objResponse.Data = objBLConverter.ToDataTable(users);
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
        /// Retrieves a specific user by their ID
        /// </summary>
        /// <param name="id">The ID of the user to retrieve</param>
        /// <returns>Response object containing user data or error information</returns>
        public Response GetUserByID(int id)
        {
            try
            {
                using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
                {
                    var user = db.SingleById<USR01>(id);
                    objResponse.IsError = user == null;
                    objResponse.Message = user != null ? "User fetched successfully." : "User not found.";
                    objResponse.Data = user != null ? objBLConverter.ObjectToDataTable(user) : null;
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
        /// Deletes a user from the database
        /// </summary>
        /// <param name="ID">The ID of the user to delete</param>
        /// <returns>Response object indicating success or failure</returns>
        public Response Delete(int ID)
        {
            try
            {
                using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
                {
                    var user = db.SingleById<USR01>(ID);
                    if (user == null)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "User Not Found";
                        return objResponse;
                    }
                    db.DeleteById<USR01>(ID);
                    objResponse.IsError = false;
                    objResponse.Message = "User Deleted Successfully";
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                // Handle foreign key constraint violation
                objResponse.IsError = true;
                objResponse.Message = "Cannot delete user due to a database constraint.";
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"An error occurred: {ex.Message}";
            }
            return objResponse;
        }

        /// <summary>
        /// Saves a user to the database (insert or update based on EnmEntryType)
        /// </summary>
        /// <returns>Response object indicating success or failure</returns>
        public Response Save()
        {
            using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
            {
                if (EnmEntryType == EnmEntryType.A)
                {
                    db.Insert(objUSR01);
                    objResponse.Message = "User Inserted Successfully";
                }
                else if (EnmEntryType == EnmEntryType.E)
                {
                    db.Update(objUSR01);
                    objResponse.Message = "User Updated Successfully";
                }
                objResponse.IsError = false;
            }
            return objResponse;
        }

        /// <summary>
        /// Prepares user data for saving
        /// </summary>
        /// <param name="objDTOUSR01">DTO containing user data</param>
        public void PreSave(DTOUSR01 objDTOUSR01)
        {
            objUSR01 = objDTOUSR01.Convert<USR01>();
            id = EnmEntryType == EnmEntryType.E ? objUSR01.R01F01 : 0;
        }

        /// <summary>
        /// Validates user data before saving
        /// </summary>
        /// <param name="userID">The ID of the user to validate</param>
        /// <returns>Response object indicating validation result</returns>
        public Response Validation(int userID)
        {
            if (EnmEntryType == EnmEntryType.E && id <= 0)
            {
                objResponse = GetUserByID(id);
                if (objResponse.IsError)
                {
                    objResponse.Message = "User not found.";
                    return objResponse;
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Retrieves the role for a given username
        /// </summary>
        /// <param name="username">The username to look up</param>
        /// <returns>The role as a string</returns>
        public string GetRole(string username)
        {
            using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
            {
                // Convert EnmRole to string when fetching from DB
                var role = db.Scalar<EnmRole>("SELECT R01F04 FROM USR01 WHERE R01F02 = @username", new { username });
                return role.ToString();
            }
        }

        /// <summary>
        /// Handles user login and JWT token generation
        /// </summary>
        /// <param name="objDTOUSR02">DTO containing login credentials</param>
        /// <returns>Response object containing login result and token</returns>
        public Response Login(DTOUSR02 objDTOUSR02)
        {
            try
            {
                using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
                {
                    // Fetch user by username
                    var user = db.Single<USR01>(e => e.R01F02 == objDTOUSR02.R01F02);
                    if (user == null || objDTOUSR02.R01F03 != user.R01F03) // Compare password hash directly
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Invalid username or password.";
                        return objResponse;
                    }

                    // Get role as string
                    string role = GetRole(user.R01F02);

                    // Generate JWT Token
                    JWTHelper objJWTHelper = new JWTHelper(_config);
                    var tokenResponse = objJWTHelper.GenerateJWTToken(user.R01F02, user.R01F01, role, 1);

                    // Prepare login response data
                    var loginData = new
                    {
                        Token = tokenResponse, // Access Token property from TokenResponse
                        UserID = user.R01F01,
                        Username = user.R01F02,
                        Role = role
                    };

                    objResponse.IsError = false;
                    objResponse.Message = "Login successful.";
                    objResponse.Data = objBLConverter.ObjectToDataTable(loginData);
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