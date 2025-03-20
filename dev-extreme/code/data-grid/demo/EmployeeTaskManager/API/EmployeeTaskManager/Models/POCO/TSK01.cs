using EmployeeTaskManager.Models.ENUM;

namespace EmployeeTaskManager.Models.POCO
{
    /// <summary>
    /// Represents a task in the EmployeeTaskManager system.
    /// </summary>
    public class TSK01
    {
        /// <summary>
        /// Task ID (Primary Key)
        /// </summary>
        public int K01F01 { get; set; }

        /// <summary>
        /// Task Title
        /// </summary>
        public string K01F02 { get; set; } = null!; 

        /// <summary>
        /// Task Description
        /// </summary>
        public string? K01F03 { get; set; } 

        /// <summary>
        /// Assigned To (Foreign Key to USR01.R01F01)
        /// </summary>
        public int K01F04 { get; set; }

        /// <summary>
        /// Department ID (Foreign Key to DPT01.T01F01)
        /// </summary>
        public int K01F05 { get; set; } 

        /// <summary>
        /// Task Status
        /// </summary>
        public EnmStatus K01F06 { get; set; } = EnmStatus.Pending; 

        /// <summary>
        /// Task Priority
        /// </summary>
        public EnmPriority K01F07 { get; set; } = EnmPriority.Medium; 

        /// <summary>
        /// Due Date
        /// </summary>
        public DateTime? K01F08 { get; set; } 
    }
}