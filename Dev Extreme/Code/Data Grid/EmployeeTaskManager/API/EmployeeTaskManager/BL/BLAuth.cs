using MySql.Data.MySqlClient;
using EmployeeTaskManager.BL;
using EmployeeTaskManager.Helpers;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using EmployeeTaskManager.Models;
using System.Data;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace EmployeeTaskManager.BL
{
    public class BLAuth
    {
        #region Properties

        public EnmEntryType EnmEntryType { get; set; }
        public Response objResponse;
        private readonly IDbConnectionFactory objIDbConnectionFactory;
        public USR01 objUSR01;
        public int id;
        public BLConverter objBLConverter;
        private readonly IConfiguration _config;
        #endregion

        public BLAuth(IDbConnectionFactory dbConnectionFactory, IConfiguration config)
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
            objIDbConnectionFactory = dbConnectionFactory;
            _config = config;
        }

        public Response GetAllUser()
        {
            try
            {
                using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
                {
                    var users = db.Select<USR01>();
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

        public void PreSave(USR01 objDTOUSR01)
        {
            objUSR01 = objDTOUSR01;
            id = EnmEntryType == EnmEntryType.E ? objUSR01.R01F01 : 0;
        }

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

        public string GetRole(string username)
        {
            using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
            {
                return db.Scalar<string>("SELECT R01F04 FROM USR01 WHERE R01F02 = @username", new { username });
            }
        }

        public Response Login(DTOUSR02 objDTOUSR02)
        {
            try
            {
                using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
                {
                    var user = db.Single<USR01>(e => e.R01F02 == objDTOUSR02.R01F02);
                    if (user == null || !BCrypt.Net.BCrypt.Verify(objDTOUSR02.R01F02, user.R01F02))
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Invalid username or password.";
                        return objResponse;
                    }

                    var role = GetRole(user.R01F02);

                    // Generate JWT Token
                    JWTHelper objJWTHelper = new JWTHelper(_config);
                    var token = objJWTHelper.GenerateJWTToken(user.R01F02, user.R01F01, role, 1);
                    var loginData = new
                    {
                        Token = token,
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