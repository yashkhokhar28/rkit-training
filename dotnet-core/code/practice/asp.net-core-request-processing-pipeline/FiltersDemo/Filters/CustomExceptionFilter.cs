using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FiltersDemo.Filters
{
    /// <summary>
    /// CustomExceptionFilter class implements the IExceptionFilter interface to handle exceptions globally.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// This method is called when an exception occurs during the execution of an action.
        /// </summary>
        /// <param name="context">The context for the exception filter.</param>
        public void OnException(ExceptionContext context)
        {
            // Create a response object with a generic error message and the exception details
            var response = new
            {
                Message = "An error occurred.",
                Error = context.Exception.Message
            };

            // Set the result to an ObjectResult with the response object and a 500 status code
            context.Result = new ObjectResult(response)
            {
                StatusCode = 500
            };
        }
    }
}