using JWTInWebAPI.Models;
using System.Web.Http;

namespace JWTInWebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for handling authentication-related operations.
    /// </summary>
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        /// <summary>
        /// Authenticates the user and generates a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="loginModel">The login model containing username and password.</param>
        /// <returns>An HTTP response containing the generated JWT token or an error message.</returns>
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginModel loginModel)
        {
            // Validate the username input field
            if (string.IsNullOrEmpty(loginModel.Username))
            {
                return BadRequest("Username is required.");
            }

            // Validate the password input field
            if (string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Password is required.");
            }

            string role = string.Empty; // Variable to store the user's role

            // Check if the provided credentials match the "user" account
            if (loginModel.Username == "user" && loginModel.Password == "user")
            {
                role = "user"; // Assign "user" role
            }
            // Check if the provided credentials match the "admin" account
            else if (loginModel.Username == "admin" && loginModel.Password == "admin")
            {
                role = "admin"; // Assign "admin" role
            }
            else
            {
                // If credentials do not match any account, return unauthorized response
                return Unauthorized();
            }

            // Generate a JWT token for the authenticated user with the assigned role
            return Ok(JWTHelper.GenerateJWTToken(loginModel.Username, role));
        }
    }
}