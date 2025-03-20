using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareDemo.Controllers
{
    /// <summary>
    /// HomeController handles the requests related to the home page and other routes.
    /// It includes both public and protected endpoints for different use cases.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Returns a welcome message as a response to the request for the home page.
        /// </summary>
        /// <returns>An Ok result with a welcome message.</returns>
        // GET: api/home
        [HttpGet]
        public IActionResult GetHomePage()
        {
            return Ok("Welcome to the Home Page!");  // Returns a 200 OK response with a message
        }

        /// <summary>
        /// Returns a greeting message to the client.
        /// </summary>
        /// <returns>An Ok result with a greeting message.</returns>
        // GET: api/home/greet
        [HttpGet("greet")]
        public IActionResult Greet()
        {
            return Ok("Hello, welcome to our API!");  // Returns a 200 OK response with a greeting
        }

        /// <summary>
        /// Returns the item with the specified ID as a response.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>An Ok result with the item ID.</returns>
        // GET: api/home/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"You requested the item with ID: {id}");  // Returns a 200 OK with the item ID
        }

        /// <summary>
        /// A public endpoint that does not require authentication, it simply returns a message.
        /// </summary>
        /// <returns>An Ok result with an open message.</returns>
        // GET: api/home
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok("This is an open endpoint, no authentication required.");  // No authentication needed
        }

        /// <summary>
        /// A secure endpoint that requires the user to be authenticated.
        /// This demonstrates an endpoint with an authentication requirement.
        /// </summary>
        /// <returns>An Ok result indicating that the endpoint is secured.</returns>
        // GET: api/home/secure
        [HttpGet("secure")]
        [Authorize]  // This endpoint requires authentication
        public IActionResult GetSecure()
        {
            return Ok("This is a secure endpoint, authentication is required.");  // Only authenticated users can access this
        }

        /// <summary>
        /// An admin-only endpoint that requires the user to have the "Admin" role.
        /// This demonstrates role-based authorization.
        /// </summary>
        /// <returns>An Ok result indicating that the user is authorized for the admin endpoint.</returns>
        // GET: api/home/admin
        [HttpGet("admin")]
        [Authorize(Policy = "AdminOnly")]  // Requires 'Admin' role
        public IActionResult GetAdminData()
        {
            return Ok("This is an admin-only endpoint.");  // Only users with 'Admin' role can access this
        }

        /// <summary>
        /// This endpoint throws an exception to test the exception handling middleware.
        /// </summary>
        /// <returns>Throws an exception for testing purposes.</returns>
        /// <exception cref="Exception">A test exception is thrown.</exception>
        // GET: api/home/error
        [HttpGet("error")]
        public IActionResult TriggerError()
        {
            throw new Exception("This is a test exception.");  // This will be caught by the exception handling middleware
        }
    }
}