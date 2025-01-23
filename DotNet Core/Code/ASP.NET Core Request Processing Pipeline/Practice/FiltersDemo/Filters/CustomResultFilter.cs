using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersDemo.Filters
{
    /// <summary>
    /// CustomResultFilter class implements the IResultFilter interface to perform actions before and after the execution of a result.
    /// </summary>
    public class CustomResultFilter : IResultFilter
    {
        /// <summary>
        /// Logger instance for logging information.
        /// </summary>
        private readonly ILogger<CustomResultFilter> _logger;

        /// <summary>
        /// Constructor to initialize the CustomResultFilter with a logger instance.
        /// </summary>
        /// <param name="logger">An instance of ILogger<CustomResultFilter>.</param>
        public CustomResultFilter(ILogger<CustomResultFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This method is called before the result executes.
        /// </summary>
        /// <param name="context">The context for the result executing.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("Result filter: Before result execution.");
        }

        /// <summary>
        /// This method is called after the result has executed.
        /// </summary>
        /// <param name="context">The context for the result executed.</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("Result filter: After result execution.");
        }
    }
}