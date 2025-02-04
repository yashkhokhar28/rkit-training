using Microsoft.AspNetCore.Mvc;

namespace MiddlewareDemo.Controllers
{
    /// <summary>
    /// Controller that handles requests for reading static files.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        /// <summary>
        /// IWebHostEnvironment instance to access web root path.
        /// </summary>
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Constructor that initializes the FileController with IWebHostEnvironment.
        /// </summary>
        /// <param name="env">The IWebHostEnvironment instance, which provides access to the web root path.</param>
        public FileController(IWebHostEnvironment env)
        {
            // Assigning the injected IWebHostEnvironment instance to the private field
            _env = env;
        }

        /// <summary>
        /// Endpoint that reads the content of a static file located in the wwwroot folder.
        /// </summary>
        /// <returns>An IActionResult containing the file content if found, or a 404 if the file doesn't exist.</returns>
        [HttpGet("read-static-file")]
        public IActionResult ReadStaticFile()
        {
            // Combine the WebRootPath (wwwroot) with the filename to get the full file path
            var filePath = Path.Combine(_env.WebRootPath, "sample.txt");

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found!"); // Return 404 if file doesn't exist

            // Read the content of the file
            var content = System.IO.File.ReadAllText(filePath);

            // Return the content as part of a response
            return Ok(new { FileContent = content });
        }
    }
}