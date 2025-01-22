using StockPortfolioAPI.BL;
using StockPortfolioAPI.Filters;
using StockPortfolioAPI.Helpers;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using System;
using System.Data;
using System.Web.Http;

namespace StockPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for managing user operations such as registration, login, and user details.
    /// </summary>
    [ValidateModelState]
    public class CLUserController : ApiController
    {
        public BLUser objBLUser;
        public Response objResponse;
        public BLConverter objBLConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLUserController"/> class.
        /// </summary>
        public CLUserController()
        {
            objBLUser = new BLUser();
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="objDTOUSR01">The user data transfer object.</param>
        /// <returns>The response indicating success or failure.</returns>
        [HttpPost]
        [Route("api/user/register")]
        public IHttpActionResult Register([FromBody] DTOUSR01 objDTOUSR01)
        {
            objBLUser.Type = EnmEntryType.A;
            objBLUser.PreSave(objDTOUSR01);
            if (objResponse.IsError)
            {
                return Ok(objResponse);
            }

            objResponse = objBLUser.Save();  // Save data using BL
            return Ok(objResponse);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="objDTOUSR02">The user login data transfer object.</param>
        /// <returns>The response indicating success or failure.</returns>
        [HttpPost]
        [Route("api/user/login")]
        public IHttpActionResult Login([FromBody] DTOUSR02 objDTOUSR02)
        {

            Response objResponse = objBLUser.Login(objDTOUSR02);  // Use BL for login
            return Ok(objResponse);
        }

        /// <summary>
        /// Gets a user by their ID. Only accessible by Admins.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user data or an error message.</returns>
        [HttpGet]
        [Route("api/user/{id}")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]
        public IHttpActionResult GetUser(int id)
        {
            if (id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Invalid User ID";
                return Ok(objResponse);
            }

            objResponse = objBLUser.GetUserByID(id);  // Get user by ID using BL
            return Ok(objResponse);
        }

        /// <summary>
        /// Gets the current user based on their JWT token. Accessible by Admins and Users.
        /// </summary>
        /// <returns>The current user data or an error message.</returns>
        [HttpGet]
        [Route("api/user")]
        [JWTAuthorizationFilter(EnmRoles.Admin, EnmRoles.User)]
        public IHttpActionResult GetUserByID()
        {
            string token = GetTokenFromRequest();
            int userID = JWTHelper.GetUserIdFromToken(token);

            objResponse = objBLUser.GetUserByID(userID);  // Get user using BL
            return Ok(objResponse);
        }

        /// <summary>
        /// Updates the current user's information.
        /// </summary>
        /// <param name="objDTOUSR01">The user data transfer object with updated information.</param>
        /// <returns>The response indicating success or failure.</returns>
        [HttpPut]
        [Route("api/user/update")]
        [JWTAuthorizationFilter(EnmRoles.User)]
        public IHttpActionResult UpdateUser([FromBody] DTOUSR01 objDTOUSR01)
        {
            objBLUser.Type = EnmEntryType.E;
            objBLUser.PreSave(objDTOUSR01);

            string token = GetTokenFromRequest();
            int userID = JWTHelper.GetUserIdFromToken(token);

            Response objResponse = objBLUser.Validation(userID);
            if (objResponse.IsError)
            {
                return Ok(objResponse.Message);
            }

            objResponse = objBLUser.Save();  // Update user using BL
            return Ok(objResponse);
        }

        /// <summary>
        /// Deletes a user by their ID. Only accessible by Admins.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The response indicating success or failure.</returns>
        [HttpDelete]
        [Route("api/user/delete/{id}")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]
        public IHttpActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Invalid User ID";
                return Ok(objResponse);
            }

            objResponse = objBLUser.Delete(id);  // Delete user using BL
            return Ok(objResponse);
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

        /// <summary>
        /// Gets the stock transactions of a user by their ID. Only accessible by Admins.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user's stock transactions or an error message.</returns>
        [HttpGet]
        [Route("api/user/{id}/transactions")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]
        public IHttpActionResult GetUserStockTransactionsByID(int id)
        {
            if (id <= 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Invalid User ID";
                return Ok(objResponse);
            }

            objResponse = objBLUser.GetUserStockTransactions(id);  // Fetch user stock transactions from BL

            if (objResponse.IsError)
            {
                return Ok(objResponse);
            }

            // The data will be in objResponse.Data as DataTable
            DataTable dtTransactions = (DataTable)objResponse.Data;
            return Ok(dtTransactions);  // Return DataTable as JSON
        }

        /// <summary>
        /// Gets the stock transactions of the current user based on their JWT token. Only accessible by Admins.
        /// </summary>
        /// <returns>The current user's stock transactions or an error message.</returns>
        [HttpGet]
        [Route("api/user/transactions")]
        [JWTAuthorizationFilter(EnmRoles.Admin)]
        public IHttpActionResult GetUserStockTransactions()
        {
            string token = GetTokenFromRequest();
            int userID = JWTHelper.GetUserIdFromToken(token);

            objResponse = objBLUser.GetUserStockTransactions(userID);  // Fetch user stock transactions from BL

            if (objResponse.IsError)
            {
                return Ok(objResponse);
            }

            // The data will be in objResponse.Data as DataTable
            DataTable dtTransactions = (DataTable)objResponse.Data;
            return Ok(dtTransactions);  // Return DataTable as JSON
        }
    }
}