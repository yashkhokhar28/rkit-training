using System.Data;

namespace EmployeeTaskManager.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DataTable Data { get; set; }
    }
}
