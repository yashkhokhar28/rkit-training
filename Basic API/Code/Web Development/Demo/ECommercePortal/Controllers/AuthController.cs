using ECommercePortal.Helpers;
using ECommercePortal.Models;
using System.Web.Http;

namespace ECommercePortal.Controllers
{
    /// <summary>
    /// Controller responsible for handling authentication-related operations like login.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
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
            // Check if the username or password is null or empty, return a bad request if so.
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
                return BadRequest("Username and Password are required.");

            // Simple role assignment based on hardcoded credentials (for testing purposes).
            // If the username and password match "admin", assign "admin" role, else assign "user" role.
            string role = loginModel.Username == "admin" && loginModel.Password == "admin" ? "admin" : "user";

            // Generate and return a JWT token based on the provided username and assigned role.
            return Ok(JWTHelper.GenerateJWTToken(loginModel.Username, role));
        }
    }
}