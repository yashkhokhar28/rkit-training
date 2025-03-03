using EmployeeTaskManager.Models.ENUM; // Assuming this namespace contains EnmRole

namespace EmployeeTaskManager.Models.POCO
{
    /// <summary>
    /// Represents a user/employee in the EmployeeTaskManager system.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User/Employee ID (Primary Key)
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// Username (Unique)
        /// </summary>
        public string R01F02 { get; set; } = null!; 

        /// <summary>
        /// Password Hash
        /// </summary>
        public string R01F03 { get; set; } = null!; 

        /// <summary>
        /// Role (Admin, Manager, Employee)
        /// </summary>
        public EnmRole R01F04 { get; set; } = EnmRole.Employee; 

        /// <summary>
        /// First Name
        /// </summary>
        public string R01F05 { get; set; } = null!; 

        /// <summary>
        /// Last Name
        /// </summary>
        public string R01F06 { get; set; } = null!; 

        /// <summary>
        /// Email (Unique)
        /// </summary>
        public string R01F07 { get; set; } = null!; 

        /// <summary>
        /// Department ID (Foreign Key to DPT01.T01F01)
        /// </summary>
        public int R01F08 { get; set; }

        /// <summary>
        /// Hire Date
        /// </summary>
        public DateTime R01F09 { get; set; } 
    }
}