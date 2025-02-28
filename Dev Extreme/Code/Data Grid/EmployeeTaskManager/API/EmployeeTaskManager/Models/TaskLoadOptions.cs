namespace EmployeeTaskManager.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskLoadOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Sort { get; set; }
    }
}
