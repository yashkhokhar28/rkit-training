using MySql.Data.MySqlClient;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using StockPortfolioAPI.Extension;
using StockPortfolioAPI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Protobuf.WellKnownTypes;

namespace StockPortfolioAPI.BL
{
    public class BLStock
    {
        #region Properties

        public EnmEntryType Type { get; set; }

        public Response objResponse;

        public STK01 objSTK01;

        public int id;

        public BLConverter objBLConverter;

        #endregion

        public BLStock()
        {
            objBLConverter = new BLConverter();
            objResponse = new Response();
        }

        public List<DTOSTK02> GetAllStocks()
        {
            List<DTOSTK02> lstStocks = new List<DTOSTK02>();

            // Define the query separately
            string query = "SELECT K01F02, K01F03, K01F04, K01F05 FROM STK01";

            // Open connection and execute query
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLConnection.ConnectionString))
            {
                objMySqlConnection.Open();
                using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                {
                    using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                    {
                        while (objMySqlDataReader.Read())
                        {
                            lstStocks.Add(new DTOSTK02
                            {
                                K01F02 = objMySqlDataReader.GetString("K01F02"),
                                K01F03 = objMySqlDataReader.GetString("K01F03"),
                                K01F04 = objMySqlDataReader.GetDecimal("K01F04"),
                                K01F05 = objMySqlDataReader.GetDecimal("k01F04")
                            });
                        }
                    }
                }
            }

            return lstStocks;
        }

        public Response Save()
        {
            int result;
            // Define the query separately
            string query = string.Format("INSERT INTO STK01 (K01F02, K01F03, K01F04, K01F05, K01F06, K01F07) VALUES ('{0}', '{1}', '{2}', '{3}','{4}','{5}')", objSTK01.K01F02, objSTK01.K01F03, objSTK01.K01F04, objSTK01.K01F05, objSTK01.K01F06.ToString("yyyy-MM-dd HH:mm:ss"), objSTK01.K01F07.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                using (MySqlConnection objMySqlConnection = new MySqlConnection(BLConnection.ConnectionString))
                {
                    objMySqlConnection.Open();
                    if (Type == EnmEntryType.A)
                    {
                        using (MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection))
                        {
                            result = objMySqlCommand.ExecuteNonQuery();
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

        public void PreSave(DTOSTK01 objDTOSTK01)
        {
            objSTK01 = objDTOSTK01.Convert<STK01>();

            // Set ModifiedAt for all operations
            objSTK01.K01F07 = DateTime.Now;

            // Set CreatedAt only for Insert operations
            if (Type == EnmEntryType.A)
            {
                objSTK01.K01F06 = DateTime.Now;
            }

            // Set the ID for Update operations
            if (Type == EnmEntryType.E)
            {
                id = objSTK01.K01F01;
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