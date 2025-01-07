using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EFWebAPIProject.Models
{
    /// <summary>
    /// The Response class is used to represent the structure of the response returned by the API.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the Data property that holds the data to be returned in the response.
        /// Typically, this will be a DataTable containing the results of a query.
        /// </summary>
        public DataTable Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there was an error in processing the request.
        /// A value of true indicates an error occurred, while false means the operation was successful.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets a message providing more details about the response.
        /// This could be an error message or a success message.
        /// </summary>
        public string Message { get; set; }
    }
}