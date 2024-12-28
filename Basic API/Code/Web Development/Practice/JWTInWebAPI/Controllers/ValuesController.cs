using JWTInWebAPI.Helpers;
using JWTInWebAPI.Models;
using System.Web.Http;

namespace JWTInWebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for handling operations that require different levels of authentication, such as accessing secure data.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Endpoint that returns sensitive data if a valid JWT token is provided in the request body.
        /// </summary>
        /// <param name="request">The request containing the JWT token to be validated.</param>
        /// <returns>An HTTP response containing secure data if the token is valid, or an unauthorized status if not.</returns>
        [HttpPost]
        [Route("secure-data")]
        public IHttpActionResult GetSecureData([FromBody] TokenRequestModel request)
        {
            // Check if the request or the token is null or empty, return a bad request if so.
            if (request == null || string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("Token is required.");
            }

            // Validate the provided JWT token. If valid, return the secure data along with the token's payload.
            if (JWTHelper.ValidateJWTToken(request.Token, out string payload))
            {
                return Ok(new { SecureData = "This is sensitive data", TokenPayload = payload });
            }

            // If the token is invalid, return an Unauthorized status.
            return Unauthorized();
        }
    }
}