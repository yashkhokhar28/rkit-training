namespace ExceptionHandlingDemo.Models
{
    /// <summary>
    /// Represents the structure of an error response returned by the application.
    /// Used to provide consistent and structured error information to clients.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets a user-friendly error message.
        /// This message is typically a general description of the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets detailed information about the error.
        /// This may include specific exception details or debugging information.
        /// </summary>
        public string Details { get; set; }
    }
}
