using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using StockPortfolioAPI.Extension;
using StockPortfolioAPI.Security;
using System;

namespace StockPortfolioAPI.BL
{
    public class BLUser
    {
        #region Properties

        public EnmEntryType Type { get; set; }

        public Response objResponse;

        public USR01 objUSR01;

        public int id;

        #endregion

        public BLUser()
        {
            objResponse = new Response();
        }


        public Response Save()
        {
            int result;
            // Hash the password
            string hashedPassword = HashHelper.ComputeSHA256Hash(objUSR01.R01F04);
            // Define the query separately
            string query = string.Format("INSERT INTO USR01 (R01F02, R01F03, R01F04) VALUES ('{0}', '{1}', '{2}')", objUSR01.R01F02, objUSR01.R01F03, hashedPassword);
            try
            {
                using (MySqlConnection objMySqlConnection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    objMySqlConnection.Open();
                    if (Type == EnmEntryType.A) // Insert
                    {
                        using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                        {
                            Console.WriteLine(objUSR01);
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

            // Determine EmployeeStatus based on Age
            // Set the user role (if not set already)
            if (string.IsNullOrEmpty(objUSR01.R01F05))
            {
                objUSR01.R01F05 = EnmRoles.User.ToString(); // Assign default role
            }

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
    }
}