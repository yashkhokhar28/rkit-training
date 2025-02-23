using StockPortfolioAPI.BL;
using StockPortfolioAPI.Filters;
using StockPortfolioAPI.Helpers;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StockPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for managing stock operations such as adding, updating, and retrieving stocks.
    /// </summary>
    [ValidateModelState]
    public class CLStockController : ApiController
    {
        public BLStock objBLStock;
        public Response objResponse;
        public BLConverter objBLConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLStockController"/> class.
        /// </summary>
        public CLStockController()
        {
            objBLStock = new BLStock();
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        /// <summary>
        /// Retrieves all stocks.
        /// </summary>
        /// <returns>A response containing all stocks or an error message.</returns>
        [HttpGet]
        [Route("api/stocks/get-all-stocks")]
        [JWTAuthorizationFilter]
        public IHttpActionResult GetAllStocks()
        {
            int result = objBLStock.GetAllStocks().Count;
            if (result > 0)
            {
                objResponse.IsError = false;
                objResponse.Message = "Data Found!!";
                List<STK01> lstStocks = objBLStock.GetAllStocks();
                objResponse.Data = objBLConverter.ToDataTable(lstStocks);
                return Ok(objResponse);
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Data Not Found";
                return Ok(objResponse);
            }
        }

        /// <summary>
        /// Adds a new stock.
        /// </summary>
        /// <param name="objDTOSTK01">The stock data transfer object.</param>
        /// <returns>The response indicating success or failure of the operation.</returns>
        [HttpPost]
        [Route("api/stocks/add-stock")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]
        public IHttpActionResult AddStock([FromBody] DTOSTK01 objDTOSTK01)
        {
            objBLStock.Type = EnmEntryType.A;
            objBLStock.PreSave(objDTOSTK01);

            string token = GetTokenFromRequest();
            int userID = JWTHelper.GetUserIdFromToken(token);

            Response objResponse = objBLStock.Validation();
            if (objResponse.IsError)
            {
                return Ok(objResponse);
            }

            objResponse = objBLStock.Save();  // Save data using BL
            return Ok(objResponse);
        }

        /// <summary>
        /// Adds multiple stocks.
        /// </summary>
        /// <param name="lstObjDTOSTK01">The list of stock data transfer objects.</param>
        /// <returns>The response indicating success or failure of the operation.</returns>
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

        /// <summary>
        /// Updates an existing stock.
        /// </summary>
        /// <param name="objDTOSTK01">The stock data transfer object.</param>
        /// <returns>The response indicating success or failure of the operation.</returns>
        [HttpPut]
        [Route("api/stocks/update-stock")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]
        public IHttpActionResult UpdateStock([FromBody] DTOSTK01 objDTOSTK01)
        {   
            objBLStock.Type = EnmEntryType.E; // Update type
            objBLStock.PreSave(objDTOSTK01);

            Response objResponse = objBLStock.Validation(); // Validate authorization
            if (objResponse.IsError)
            {
                return Ok(objResponse);
            }

            objResponse = objBLStock.Save();  // Update stock using BL
            return Ok(objResponse);
        }

        /// <summary>
        /// Updates multiple stocks.
        /// </summary>
        /// <param name="lstObjDTOSTK01">The list of stock data transfer objects.</param>
        /// <returns>The response indicating success or failure of the operation.</returns>
        [HttpPut]
        [Route("api/stocks/update-multiple-stocks")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]
        public IHttpActionResult UpdateMultipleStocks([FromBody] List<DTOSTK01> lstObjDTOSTK01)
        {
            if (lstObjDTOSTK01 == null || !lstObjDTOSTK01.Any())
            {
                return BadRequest("No stocks provided.");
            }

            // Loop through the list of stocks and update each one
            foreach (var stock in lstObjDTOSTK01)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                objBLStock.Type = EnmEntryType.E;  // Update type
                objBLStock.PreSave(stock);  // Prepare the stock data

                // Validate stock data
                string token = GetTokenFromRequest();
                int userID = JWTHelper.GetUserIdFromToken(token);

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
            objResponse.Message = "All stocks updated successfully.";
            return Ok(objResponse);  // Return success after all stocks are updated
        }

        /// <summary>
        /// Extracts the JWT token from the request headers.
        /// </summary>
        /// <returns>The JWT token.</returns>
        private string GetTokenFromRequest()
        {
            var token = string.Empty;

            // Check if the Authorization header exists
            if (Request.Headers.Authorization != null)
            {
                // The token should be in the format 'Bearer <token>'
                var authorizationHeader = Request.Headers.Authorization.Parameter;
                if (!string.IsNullOrEmpty(authorizationHeader))
                {
                    token = authorizationHeader;
                }
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Authorization token is missing.");
            }

            return token;
        }
    }
}