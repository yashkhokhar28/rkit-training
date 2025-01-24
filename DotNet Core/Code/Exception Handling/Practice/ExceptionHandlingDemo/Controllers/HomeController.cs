using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlingDemo.Controllers
{
    /// <summary>
    /// A controller for testing exception handling in the application.
    /// Provides endpoints to simulate and test error scenarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Simulates an exception for testing the global exception handling middleware.
        /// </summary>
        /// <returns>This method does not return any data because it always throws an exception.</returns>
        /// <exception cref="Exception">Throws a simulated exception with a test message.</exception>
        [HttpGet("trigger-error")]
        public IActionResult TriggerError()
        {
            // Simulate an unhandled exception to test exception handling.
            throw new Exception("This is a simulated exception.");
        }
    }
}
