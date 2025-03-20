using System.Data;

namespace ContactBookAPI.Models
{
    /// <summary>
    /// Response class to encapsulate API response data.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the response indicates an error.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the response.
        /// </summary>
        public DataTable Data { get; set; }
    }
}