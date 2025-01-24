using ExceptionHandlingDemo.Models;
using Newtonsoft.Json;
using System.Net;

namespace ExceptionHandlingDemo.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions globally in the application.
    /// Catches unhandled exceptions, logs them, and returns a structured error response to the client.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// Represents the next middleware in the HTTP request pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Logger for recording error details and other information.
        /// </summary>
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger to use for logging errors.</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Intercepts HTTP requests, executes the next middleware, and handles any exceptions that occur.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Pass the request to the next middleware in the pipeline.
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Log the exception.
                _logger.LogError($"Something went wrong: {ex}");

                // Handle the exception and return a proper response to the client.
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handles exceptions by setting the HTTP response and returning a structured error response in JSON format.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="exception">The exception that was thrown.</param>
        /// <returns>A task representing the asynchronous operation of writing the error response.</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set the content type to JSON.
            context.Response.ContentType = "application/json";

            // Set the HTTP status code to 500 (Internal Server Error).
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Create a structured error response.
            var errorResponse = new ErrorResponse
            {
                Message = "Internal Server Error",
                Details = exception.Message // Optionally, hide details in production
            };

            // Serialize the error response to JSON and write it to the HTTP response body.
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}