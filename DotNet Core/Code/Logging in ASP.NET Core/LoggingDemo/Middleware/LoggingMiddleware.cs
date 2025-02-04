namespace LoggingDemo.Middleware
{
    /// <summary>
    /// Middleware for logging HTTP requests and responses.
    /// </summary>
    public class LoggingMiddleware
    {
        /// <summary>
        /// Delegate to call the next middleware in the pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Logger instance for logging messages.
        /// </summary>
        private readonly ILogger<LoggingMiddleware> _logger;

        /// <summary>
        /// Constructor to initialize the next delegate and logger instances.
        /// </summary>
        /// <param name="next">The next middleware delegate in the pipeline.</param>
        /// <param name="logger">Logger instance injected via dependency injection.</param>
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware to log the incoming request and outgoing response.
        /// </summary>
        /// <param name="context">The HTTP context representing the current request and response.</param>
        /// <returns>A task that represents the completion of request processing.</returns>
        public async Task Invoke(HttpContext context)
        {
            // Log the incoming request method and path.
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

            // Call the next middleware in the pipeline.
            await _next(context);

            // Log the response status code.
            _logger.LogInformation($"Response: {context.Response.StatusCode}");
        }
    }
}