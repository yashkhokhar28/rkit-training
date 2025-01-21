using StockPortfolioAPI.BL;
using StockPortfolioAPI.Filters;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StockPortfolioAPI.Controllers
{
    public class CLStockController : ApiController
    {
        public BLStock objBLStock;

        public Response objResponse;

        public BLConverter objBLConverter;

        public CLStockController()
        {
            objBLStock = new BLStock();
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        [HttpGet]
        [Route("api/stocks/get-all-stocks")]
        [JWTAuthorizationFilter]
        public IHttpActionResult GetAllEmployee()
        {
            int result = objBLStock.GetAllStocks().Count;
            if (result > 0)
            {
                objResponse.IsError = false;
                objResponse.Message = "Data Found!!";
                List<DTOSTK02> lstStocks = objBLStock.GetAllStocks();
                objResponse.Data = objBLConverter.ToDataTable(lstStocks);
                return Ok(objResponse);
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                return Ok(objResponse);
            }
        }

        [HttpPost]
        [Route("api/stocks/add-stock")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]
        public IHttpActionResult AddStock([FromBody] DTOSTK01 objDTOSTK01)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            objBLStock.Type = EnmEntryType.A;
            objBLStock.PreSave(objDTOSTK01);
            Response objResponse = objBLStock.Validation();
            if (objResponse.IsError)
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                return Ok(objResponse);
            }
            else
            {
                objBLStock.Save();
                objResponse.IsError = false;
                objResponse.Message = "Data Added Successfully";
                return Ok(objResponse);
            }
        }

        [HttpPost]
        [Route("api/stocks/add-multiple-stock")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]
        public IHttpActionResult AddMultipleStock([FromBody] List<DTOSTK01> lstObjDTOSTK01)
        {
            if (lstObjDTOSTK01 == null || !lstObjDTOSTK01.Any())
            {
                return BadRequest("No stocks provided.");
            }

            // Loop through the list of stocks and add each one
            foreach (var stock in lstObjDTOSTK01)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                objBLStock.Type = EnmEntryType.A;  // Insert type
                objBLStock.PreSave(stock);  // Prepare the stock data

                // Validate stock data
                Response objResponse = objBLStock.Validation();
                if (objResponse.IsError)
                {
                    objResponse.Message = "Error occurred during validation for one or more stocks.";
                    return Ok(objResponse);  // Return error if validation fails for any stock
                }

                // Save the stock data
                objBLStock.Save();
            }

            objResponse.IsError = false;
            objResponse.Message = "All stocks added successfully.";
            return Ok(objResponse);  // Return success after all stocks are added
        }
    }
}