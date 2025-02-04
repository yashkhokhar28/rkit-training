using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddlewareDemo.Middlewares
{
    /// <summary>
    /// ShortCircuitMiddleware is used to interrupt the request pipeline early based on certain conditions.
    /// If the request does not meet the required condition, it immediately returns a response without passing to the next middleware.
    /// </summary>
    public class ShortCircuitMiddleware
    {
        /// <summary>
        /// The next middleware delegate in the pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor that initializes the ShortCircuitMiddleware with the next middleware.
        /// </summary>
        /// <param name="next">The next middleware to be executed in the pipeline.</param>
        public ShortCircuitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// The main method that checks for the presence of a required header ('X-Api-Key') in the request.
        /// If the header is missing, it short-circuits the pipeline by returning a 400 Bad Request response.
        /// If the header is present, it allows the request to proceed to the next middleware.
        /// </summary>
        /// <param name="context">The current HTTP context, which contains the request and response information.</param>
        /// <returns>A Task representing the asynchronous operation of this middleware.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the request contains the required 'X-Api-Key' header
            if (!context.Request.Headers.ContainsKey("X-Api-Key"))
            {
                // If the header is missing, short-circuit the request and return a 400 Bad Request
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Bad Request: Missing X-Api-Key header.");
                return; // Terminate request processing here
            }

            // If the header is present, pass the request to the next middleware in the pipeline
            await _next(context);
        }
    }

    /// <summary>
    /// Extension method to add the ShortCircuitMiddleware to the application's middleware pipeline.
    /// </summary>
    public static class ShortCircuitMiddlewareExtensions
    {
        /// <summary>
        /// Adds the ShortCircuitMiddleware to the application's request pipeline.
        /// </summary>
        /// <param name="builder">The IApplicationBuilder used to configure the request pipeline.</param>
        /// <returns>The IApplicationBuilder instance with the ShortCircuitMiddleware added.</returns>
        public static IApplicationBuilder UseShortCircuitMiddleware(this IApplicationBuilder builder)
        {
            // Add the ShortCircuitMiddleware to the pipeline
            return builder.UseMiddleware<ShortCircuitMiddleware>();
        }
    }
}