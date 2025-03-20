using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddlewareDemo.Middlewares
{
    /// <summary>
    /// Custom middleware that logs incoming HTTP requests and outgoing HTTP responses.
    /// </summary>
    public class LoggingMiddleware
    {
        /// <summary>
        /// The next middleware in the pipeline, passed to this middleware during initialization.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor to initialize the LoggingMiddleware with the next middleware.
        /// </summary>
        /// <param name="next">The next middleware to be executed in the pipeline.</param>
        public LoggingMiddleware(RequestDelegate next)
        {
            // Assign the next middleware to the private field
            _next = next;
        }

        /// <summary>
        /// This method handles the HTTP request and response logging.
        /// It logs the HTTP method and path of incoming requests, 
        /// and logs the status code of the outgoing response.
        /// </summary>
        /// <param name="context">The current HTTP context for the incoming request.</param>
        /// <returns>A task representing the asynchronous operation of this middleware.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // Log incoming request details (HTTP method and path)
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

            // Pass control to the next middleware in the pipeline
            await _next(context);

            // Log outgoing response status code after processing the request
            Console.WriteLine($"Response: {context.Response.StatusCode}");
        }
    }

    /// <summary>
    /// Static extension class to add the LoggingMiddleware to the application pipeline.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Extension method to add the LoggingMiddleware to the application's middleware pipeline.
        /// </summary>
        /// <param name="builder">The IApplicationBuilder used to configure the middleware pipeline.</param>
        /// <returns>The IApplicationBuilder instance with the LoggingMiddleware added.</returns>
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            // Add the LoggingMiddleware to the middleware pipeline
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}