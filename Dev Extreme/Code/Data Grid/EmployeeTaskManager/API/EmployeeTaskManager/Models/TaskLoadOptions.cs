namespace EmployeeTaskManager.Models
{
    /// <summary>
    /// Represents options for loading tasks with filtering, sorting, and pagination capabilities.
    /// Used to customize task retrieval operations in the EmployeeTaskManager system.
    /// </summary>
    public class TaskLoadOptions
    {
        /// <summary>
        /// Gets or sets the number of records to skip for pagination.
        /// Optional; used to implement infinite scrolling or paged results (e.g., Skip = 10 to start from 11th record).
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Gets or sets the number of records to take for pagination.
        /// Optional; limits the number of tasks returned in a single request (e.g., Take = 10 for 10 records per page).
        /// </summary>
        public int? Take { get; set; }

        /// <summary>
        /// Gets or sets a JSON string representing filtering criteria.
        /// Optional; typically in the format [field, operator, value] (e.g., ["Title", "contains", "Report"]).
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// Gets or sets a JSON string representing sorting criteria.
        /// Optional; typically an array of objects with 'selector' and 'desc' properties (e.g., [{"selector":"DueDate","desc":true}]).
        /// </summary>
        public string? Sort { get; set; }
    }
}