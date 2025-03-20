namespace ECommercePortal.Models
{
    /// <summary>
    /// Represents the structure of the error response sent to the client.
    /// </summary>
    public class ErrorResponseModel
    {
        /// <summary>
        /// Gets or sets the user-friendly error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the technical details of the error (e.g., exception message).
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the technical details of the error (e.g., exception message).
        /// </summary>
        public string ExceptionType { get; set; }
    }
}