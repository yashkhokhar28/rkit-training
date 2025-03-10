using System.Data;

namespace EmployeeTaskManager.Models
{
    /// <summary>
    /// Represents a standardized response object for API operations in the EmployeeTaskManager system.
    /// Used to communicate operation results, including success/failure status, messages, and data.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the message describing the result of the operation.
        /// Provides context about the success or failure of the request (e.g., "User fetched successfully" or "Invalid username").
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether an error occurred during the operation.
        /// True if the operation failed; False if it succeeded.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets the data resulting from the operation, stored as a DataTable.
        /// Contains the operation's output (e.g., user records, task lists) or null if no data is returned.
        /// </summary>
        public DataTable Data { get; set; }
    }
}