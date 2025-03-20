using LoggingDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggingDemo.Controllers
{
    /// <summary>
    /// This controller handles logging-related actions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        /// <summary>
        /// Logger instance for logging messages.
        /// </summary>
        private readonly ILogger<LoggingController> _logger;

        /// <summary>
        /// Service instance for performing tasks.
        /// </summary>
        private readonly SampleService _service;

        /// <summary>
        /// Constructor to initialize the logger and service instances.
        /// </summary>
        /// <param name="logger">Logger instance injected via dependency injection.</param>
        /// <param name="service">Service instance injected via dependency injection.</param>
        public LoggingController(ILogger<LoggingController> logger, SampleService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <returns>Returns an OK result with a message.</returns>
        [HttpGet("info")]
        public IActionResult LogInfo()
        {
            _logger.LogInformation("LoggingController: Info log message.");
            return Ok("Info log written.");
        }

        /// <summary>
        /// Logs an error message and performs a task using the service.
        /// </summary>
        /// <returns>Returns a 500 status code with a message.</returns>
        [HttpGet("error")]
        public IActionResult LogError()
        {
            _logger.LogError("LoggingController: Error log message.");
            _service.PerformTask();
            return StatusCode(500, "Error log written.");
        }
    }
}