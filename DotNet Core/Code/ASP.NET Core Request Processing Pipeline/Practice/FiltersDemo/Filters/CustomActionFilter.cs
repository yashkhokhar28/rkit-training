using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersDemo.Filters
{
    /// <summary>
    /// CustomActionFilter class implements the IActionFilter interface to log information before and after the execution of an action method.
    /// </summary>
    public class CustomActionFilter : IActionFilter
    {
        private readonly ILogger<CustomActionFilter> _logger;

        /// <summary>
        /// Constructor to initialize the CustomActionFilter with a logger instance.
        /// </summary>
        /// <param name="logger">An instance of ILogger<CustomActionFilter>.</param>
        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This method is called before an action method executes.
        /// </summary>
        /// <param name="context">The context for the action executing.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Action filter: Before action method execution.");
        }

        /// <summary>
        /// This method is called after an action method executes.
        /// </summary>
        /// <param name="context">The context for the action executed.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Action filter: After action method execution.");
        }
    }
}