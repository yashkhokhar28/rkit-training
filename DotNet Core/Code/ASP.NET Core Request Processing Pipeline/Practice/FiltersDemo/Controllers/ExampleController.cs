using Microsoft.AspNetCore.Mvc;
using FiltersDemo.Filters;

namespace FiltersDemo.Controllers
{
    /// <summary>
    /// Demonstrates the usage of various filters in ASP.NET Core Web API.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        /// <summary>
        /// A secure endpoint demonstrating the use of a custom authorization filter.
        /// </summary>
        /// <remarks>
        /// This endpoint requires the user to be authenticated. The <see cref="CustomAuthorizationFilter"/> 
        /// ensures that unauthorized users are denied access.
        /// </remarks>
        /// <returns>Returns a success message if the user is authorized.</returns>
        [HttpGet("secure-endpoint")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult SecureEndpoint()
        {
            return Ok("You are authorized to access this endpoint.");
        }

        /// <summary>
        /// An endpoint demonstrating the use of a custom exception filter.
        /// </summary>
        /// <remarks>
        /// This endpoint deliberately throws an exception. The <see cref="CustomExceptionFilter"/> 
        /// catches the exception and returns a custom error response.
        /// </remarks>
        /// <returns>Throws an <see cref="InvalidOperationException"/>.</returns>
        /// <exception cref="InvalidOperationException">Simulated exception to demonstrate exception handling.</exception>
        [HttpGet("exception-endpoint")]
        [ServiceFilter(typeof(CustomExceptionFilter))]
        public IActionResult ExceptionEndpoint()
        {
            throw new InvalidOperationException("This is a simulated exception.");
        }

        /// <summary>
        /// An endpoint demonstrating the use of a custom resource filter.
        /// </summary>
        /// <remarks>
        /// The <see cref="CustomResourceFilter"/> runs before and after the action execution, 
        /// allowing you to add resource initialization or caching logic.
        /// </remarks>
        /// <returns>Returns a success message indicating the resource filter was executed.</returns>
        [HttpGet("resource-endpoint")]
        [ServiceFilter(typeof(CustomResourceFilter))]
        public IActionResult ResourceEndpoint()
        {
            return Ok("Resource filter demonstration.");
        }

        /// <summary>
        /// An endpoint demonstrating the use of a custom action filter.
        /// </summary>
        /// <remarks>
        /// The <see cref="CustomActionFilter"/> executes custom logic before and after the action method is called.
        /// Useful for logging, validation, or tracking.
        /// </remarks>
        /// <returns>Returns a success message indicating the action filter was executed.</returns>
        [HttpGet("action-endpoint")]
        [ServiceFilter(typeof(CustomActionFilter))]
        public IActionResult ActionEndpoint()
        {
            return Ok("Action filter demonstration.");
        }

        /// <summary>
        /// An endpoint demonstrating the use of a custom result filter.
        /// </summary>
        /// <remarks>
        /// The <see cref="CustomResultFilter"/> modifies or processes the response after the action method has executed.
        /// </remarks>
        /// <returns>Returns a success message indicating the result filter was executed.</returns>
        [HttpGet("result-endpoint")]
        [ServiceFilter(typeof(CustomResultFilter))]
        public IActionResult ResultEndpoint()
        {
            return Ok("Result filter demonstration.");
        }
    }
}