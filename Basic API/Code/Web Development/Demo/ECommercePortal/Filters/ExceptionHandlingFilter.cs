using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;
using ECommercePortal.Models;
using System.Net.Http;

namespace ECommercePortal.Filters
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

            // Prepare a custom error response to return to the client
            ErrorResponseModel response = new ErrorResponseModel
            {
                Message = "An unexpected error occurred. Please try again later.", // User-friendly error message
                ExceptionType = context.Exception.GetType().Name,
                Details = context.Exception.Message // Technical details (consider removing in production for security)
            };

            // Create an HTTP 500 Internal Server Error response with the custom error object
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, response);
        }
    }
}