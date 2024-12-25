using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace JWTInWebAPI.Controllers
{
    /// <summary>
    /// Controller for managing operations that require different levels of authentication and roles.
    /// </summary>
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Extracts the username from the JWT claims present in the authenticated user's identity.
        /// </summary>
        /// <returns>The username if available in the claims; otherwise, null.</returns>
        private string GetUsernameFromClaims()
        {
            // Get the current user's identity as ClaimsIdentity
            var identity = User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                // Find the claim with type "username" and return its value
                return identity.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
            }

            // Return null if no valid claims are found
            return null;
        }

        /// <summary>
        /// Endpoint accessible without a token. Returns a generic response.
        /// </summary>
        /// <returns>A message indicating no token is required.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("open")]
        public IHttpActionResult GetOpenName()
        {
            // Return a response indicating no token is required
            return Ok("No Token Required");
        }

        /// <summary>
        /// Endpoint accessible only to users with the "user" role. Returns the username.
        /// </summary>
        /// <returns>The username extracted from the JWT claims.</returns>
        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("user")]
        public IHttpActionResult GetUserName()
        {
            // Extract the username from the claims
            var username = GetUsernameFromClaims();

            // Return the username if found, otherwise return Unauthorized
            if (username != null)
            {
                return Ok(new { username });
            }
            return Unauthorized(); // Explicit Unauthorized response
        }

        /// <summary>
        /// Endpoint accessible only to users with the "admin" role. Returns the username.
        /// </summary>
        /// <returns>The username extracted from the JWT claims.</returns>
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("admin")]
        public IHttpActionResult GetAdminName()
        {
            // Extract the username from the claims
            var username = GetUsernameFromClaims();

            // Return the username if found, otherwise return Unauthorized
            if (username != null)
            {
                return Ok(new { username });
            }
            return Unauthorized(); // Explicit Unauthorized response
        }
    }
}