using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace ExceptionHandlingFilterInWebAPI.Filters
{
    /// <summary>
    /// Custom exception handling filter for Web API.
    /// This filter intercepts unhandled exceptions thrown by Web API actions
    /// and allows for custom error responses to be sent to the client.
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.ExceptionFilterAttribute" />
    public class ExceptionHandlingFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Called asynchronously when an unhandled exception occurs during the execution of a Web API action.
        /// </summary>
        /// <param name="context">The context for the exception, including the request and exception details.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        public override async Task OnExceptionAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            // Log the exception details (you can replace this with an actual logging service)
            // Example: LogException(context.Exception);

            // Prepare a custom error response to return to the client
            var response = new ErrorResponse
            {
                Message = "An unexpected error occurred. Please try again later.", // User-friendly error message
                Details = context.Exception.Message // Technical details (consider removing in production for security)
            };

            // Create an HTTP 500 Internal Server Error response with the custom error object
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, response);

            // Call the base implementation (optional, based on your needs)
            await base.OnExceptionAsync(context, cancellationToken);
        }
    }
}
