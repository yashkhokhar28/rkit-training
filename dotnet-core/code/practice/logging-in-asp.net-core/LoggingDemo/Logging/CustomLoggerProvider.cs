namespace LoggingDemo.Logging
{
    /// <summary>
    /// Custom logger provider for creating instances of CustomLogger.
    /// </summary>
    public class CustomLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// Creates an instance of CustomLogger.
        /// </summary>
        /// <param name="categoryName">The category name for the logger (not used in this implementation).</param>
        /// <returns>An instance of CustomLogger.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        /// <summary>
        /// Disposes resources used by the logger provider.
        /// </summary>
        public void Dispose()
        {
            // No resources to dispose in this implementation.
        }
    }

    /// <summary>
    /// Custom logger implementation.
    /// </summary>
    public class CustomLogger : ILogger
    {
        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The type of the state to begin scope with.</typeparam>
        /// <param name="state">The state to begin scope with.</param>
        /// <returns>A disposable object that ends the logical operation scope on dispose (returns null in this implementation).</returns>
        public IDisposable BeginScope<TState>(TState state) => null;

        /// <summary>
        /// Checks if the given log level is enabled.
        /// </summary>
        /// <param name="logLevel">The log level to check.</param>
        /// <returns>Always returns true in this implementation.</returns>
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>
        /// Logs a message with the specified log level, event ID, state, and exception.
        /// </summary>
        /// <typeparam name="TState">The type of the state to log.</typeparam>
        /// <param name="logLevel">The log level to use.</param>
        /// <param name="eventId">The event ID to associate with the log message.</param>
        /// <param name="state">The state to log.</param>
        /// <param name="exception">The exception to log (if any).</param>
        /// <param name="formatter">The function to create a log message from the state and exception.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var logMessage = formatter(state, exception);
            Console.WriteLine($"[CustomLogger] {logLevel}: {logMessage}"); // Ensure it logs to console
        }
    }
}