namespace LoggingDemo.Services
{
    /// <summary>
    /// Service class for performing sample tasks with logging.
    /// </summary>
    public class SampleService
    {
        /// <summary>
        /// Logger instance for logging messages.
        /// </summary>
        private readonly ILogger<SampleService> _logger;

        /// <summary>
        /// Constructor to initialize the logger instance.
        /// </summary>
        /// <param name="logger">Logger instance injected via dependency injection.</param>
        public SampleService(ILogger<SampleService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method to perform a task and log information and errors.
        /// </summary>
        public void PerformTask()
        {
            // Log an information message indicating the task is being performed.
            _logger.LogInformation("SampleService: Performing a task.");

            try
            {
                // Simulate an error occurring during the task.
                throw new System.Exception("An error occurred in SampleService.");
            }
            catch (System.Exception ex)
            {
                // Log the exception with an error message.
                _logger.LogError(ex, "Exception in SampleService.");
            }
        }
    }
}