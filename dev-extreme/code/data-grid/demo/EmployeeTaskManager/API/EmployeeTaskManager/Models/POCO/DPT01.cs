namespace EmployeeTaskManager.Models.POCO
{
    /// <summary>
    /// Represents a department in the EmployeeTaskManager system.
    /// </summary>
    public class DPT01
    {
        /// <summary>
        /// Department ID (Primary Key)
        /// </summary>
        public int T01F01 { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string T01F02 { get; set; } = null!;

        /// <summary>
        /// Manager ID (Foreign Key to USR01.R01F01)
        /// </summary>
        public int T01F03 { get; set; } 
    }
}