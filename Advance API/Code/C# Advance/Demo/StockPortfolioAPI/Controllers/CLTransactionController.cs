using StockPortfolioAPI.BL;
using StockPortfolioAPI.Filters;
using StockPortfolioAPI.Helpers;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for handling stock transactions such as buying and selling stocks.
    /// </summary>
    [ValidateModelState]
    public class CLTransactionController : ApiController
    {
        public BLPortfolio objBLPortfolio;
        public BLTransaction objBLTransaction;  // Handle transactions
        public Response objResponse;
        public BLConverter objBLConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLTransactionController"/> class.
        /// </summary>
        public CLTransactionController()
        {
            objBLPortfolio = new BLPortfolio();
            objBLTransaction = new BLTransaction();  // Transaction handling
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        /// <summary>
        /// Handles the buying and selling of stocks.
        /// </summary>
        /// <param name="objDTOTRN01">The transaction data transfer object.</param>
        /// <returns>The response indicating success or failure of the transaction.</returns>
        [HttpPost]
        [Route("api/transaction/buy-sell-stock")]
        [JWTAuthorizationFilter(EnmRoles.User, EnmRoles.Admin)]  // Ensure user is authenticated
        public IHttpActionResult BuySellStock([FromBody] DTOTRN01 objDTOTRN01)
        {
            string token = GetTokenFromRequest();
            int userID = JWTHelper.GetUserIdFromToken(token);  // Extract UserId from JWT token

            objBLTransaction.PreSave(objDTOTRN01, userID);  // Prepare the transaction

            objResponse = objBLTransaction.Validation();
            if (objResponse.IsError)
            {
                return Ok(objResponse);  // Return validation error
            }

            // Update transaction in the transaction table
            objResponse = objBLTransaction.Save();
            objResponse = objBLTransaction.UpdatePortfolioAfterTransaction();
            objResponse = objBLTransaction.UpdatePortfolioIndividually();
            if (objResponse.IsError)
            {
                return Ok(objResponse.Message);  // Return error if transaction failed
            }
            return Ok(objResponse);  // Return success or failure based on portfolio update
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