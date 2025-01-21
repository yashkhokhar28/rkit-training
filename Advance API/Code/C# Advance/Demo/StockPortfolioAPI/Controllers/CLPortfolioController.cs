using Mysqlx.Prepare;
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
    public class CLPortfolioController : ApiController
    {
        public BLPortfolio objBLPortfolio;

        public Response objResponse;

        public BLConverter objBLConverter;

        public CLPortfolioController()
        {
            objBLPortfolio = new BLPortfolio();
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        [HttpPost]
        [Route("api/portfolio/create-portfolio")]
        [JWTAuthorizationFilter(EnmRoles.User)]  // Only users can add stocks to their portfolio
        public IHttpActionResult CreatePortfolio([FromBody] DTOPRT01 objDTOPRT01)
        {
            // Extract UserId from the JWT Token
            string token = GetTokenFromRequest();
            int userID = JWTHelper.GetUserIdFromToken(token);  // Get UserId from the token


            objBLPortfolio.Type = EnmEntryType.A;
            objBLPortfolio.PreSave(objDTOPRT01, userID);
            Response objResponse = objBLPortfolio.Validation();
            if (objResponse.IsError)
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                return Ok(objResponse);
            }
            else
            {
                objBLPortfolio.Save();
                objResponse.IsError = false;
                objResponse.Message = "Data Added Successfully";
                return Ok(objResponse);
            }
        }

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