using MySql.Data.MySqlClient;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using StockPortfolioAPI.Extension;
using System;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Web.Http;

namespace StockPortfolioAPI.BL
{
    public class BLPortfolio
    {
        #region Properties

        public EnmEntryType Type { get; set; }

        public Response objResponse;

        public PRT01 objPRT01;

        public int id;

        public BLConverter objBLConverter;

        #endregion

        public BLPortfolio()
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        public Response Save()
        {
            string query = string.Format(
        "INSERT INTO PRT01 (T01F02, T01F03, T01F05, T01F06) VALUES ({0}, '{1}', '{2}', '{3}')",
        objPRT01.T01F02,  // UserId
        objPRT01.T01F03,  // PortfolioName
        objPRT01.T01F05.ToString("yyyy-MM-dd HH:mm:ss"),
        objPRT01.T01F06.ToString("yyyy-MM-dd HH:mm:ss")
    );

            try
            {
                using (MySqlConnection connection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
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

        public void PreSave(DTOPRT01 objDTOPRT01, int userID)
        {
            objPRT01 = objDTOPRT01.Convert<PRT01>();

            objPRT01.T01F02 = userID;

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