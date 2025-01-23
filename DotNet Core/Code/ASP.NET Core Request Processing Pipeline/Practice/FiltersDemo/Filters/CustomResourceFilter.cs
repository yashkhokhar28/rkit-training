using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersDemo.Filters
{
    /// <summary>
    /// CustomResourceFilter class implements the IResourceFilter interface to perform actions before and after the execution of a resource.
    /// </summary>
    public class CustomResourceFilter : IResourceFilter
    {
        /// <summary>
        /// Logger instance for logging information.
        /// </summary>
        private readonly ILogger<CustomResourceFilter> _logger;

        /// <summary>
        /// Constructor to initialize the CustomResourceFilter with a logger instance.
        /// </summary>
        /// <param name="logger">An instance of ILogger<CustomResourceFilter>.</param>
        public CustomResourceFilter(ILogger<CustomResourceFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This method is called before the resource executes.
        /// </summary>
        /// <param name="context">The context for the resource executing.</param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation("Resource filter: Executing");
        }

        /// <summary>
        /// This method is called after the resource has executed.
        /// </summary>
        /// <param name="context">The context for the resource executed.</param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation("Resource filter: Executed");
        }
    }
}