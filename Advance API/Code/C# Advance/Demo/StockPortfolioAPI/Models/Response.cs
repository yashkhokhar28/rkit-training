using System.Data;

namespace StockPortfolioAPI.Models
{
    /// <summary>
    /// Represents a response object for API calls.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets a value indicating whether the response indicates an error.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the response, if any.
        /// </summary>
        public DataTable Data { get; set; }
    }
}