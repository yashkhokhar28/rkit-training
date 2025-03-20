using StockPortfolioAPI.BL;
using StockPortfolioAPI.Filters;
using StockPortfolioAPI.Helpers;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using System;
using System.Web.Http;

namespace StockPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for managing portfolio operations such as creating and viewing portfolios.
    /// </summary>
    public class CLPortfolioController : ApiController
    {
        public BLPortfolio objBLPortfolio;
        public Response objResponse;
        public BLConverter objBLConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLPortfolioController"/> class.
        /// </summary>
        public CLPortfolioController()
        {
            objBLPortfolio = new BLPortfolio();
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        /// <summary>
        /// Creates a new portfolio for the user.
        /// </summary>
        /// <param name="objDTOPRT01">The portfolio data transfer object.</param>
        /// <returns>The response indicating success or failure of the operation.</returns>
        [HttpPost]
        [Route("api/portfolio/create-portfolio")]
        [JWTAuthorizationFilter(EnmRoles.User)]  // Only users can create portfolios
        public IHttpActionResult CreatePortfolio([FromBody] DTOPRT01 objDTOPRT01)
        {
            string token = GetTokenFromRequest();
            int userID = JWTHelper.GetUserIdFromToken(token);  // Extract UserId from the JWT token

            objBLPortfolio.Type = EnmEntryType.A;
            objBLPortfolio.PreSave(objDTOPRT01, userID);  // Prepare portfolio data with user ID

            // Validate portfolio before saving
            objResponse = objBLPortfolio.Validation();
            if (objResponse.IsError)
            {
                return Ok(objResponse);  // Return validation error if any
            }

            // Save portfolio data
            objResponse = objBLPortfolio.Save();
            return Ok(objResponse);  // Return success or failure based on Save
        }

        /// <summary>
        /// Views a user's portfolio by their user ID. Only accessible by Admins.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>The portfolio data or an error message.</returns>
        [HttpGet]
        [Route("api/portfolio/view-portfolio/{userID}")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]  // Admin or the user can view the portfolio
        public IHttpActionResult ViewPortfolioByID(int userID)
        {
            // Fetch the user's portfolio based on their user ID
            objResponse = objBLPortfolio.GetPortfolioByUserID(userID);

            return Ok(objResponse);  // Return the portfolio data or error message
        }

        /// <summary>
        /// Views the current user's portfolio based on their JWT token. Accessible by Users.
        /// </summary>
        /// <returns>The portfolio data or an error message.</returns>
        [HttpGet]
        [Route("api/portfolio/view-portfolio")]
        [JWTAuthorizationFilter(EnmRoles.User)]  // Admin or the user can view the portfolio
        public IHttpActionResult ViewPortfolio()
        {
            string token = GetTokenFromRequest();
            int userID = JWTHelper.GetUserIdFromToken(token);

            // Fetch the user's portfolio based on their user ID
            objResponse = objBLPortfolio.GetPortfolioByUserID(userID);

            return Ok(objResponse);  // Return the portfolio data or error message
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