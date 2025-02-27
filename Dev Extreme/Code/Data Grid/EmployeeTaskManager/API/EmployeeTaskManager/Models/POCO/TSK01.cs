using EmployeeTaskManager.Models.ENUM;

namespace EmployeeTaskManager.Models.POCO
{
    /// <summary>
    /// 
    /// </summary>
    public class TSK01
    {
        /// <summary>
        /// task_id
        /// </summary>
        public int K01F01 { get; set; }

        /// <summary>
        /// title
        /// </summary>
        public string K01F02 { get; set; }

        /// <summary>
        /// description
        /// </summary>
        public string K01F03 { get; set; }

        /// <summary>
        /// assigned_to
        /// </summary>
        public int K01F04 { get; set; }

        /// <summary>
        /// department_id
        /// </summary>
        public int K01F05 { get; set; }

        /// <summary>
        /// status
        /// </summary>
        public EnmStatus K01F06 { get; set; }

        /// <summary>
        /// priority
        /// </summary>
        public EnmPriority K01F07 { get; set; }

        /// <summary>
        /// due_date
        /// </summary>
        public DateTime K01F08 { get; set; }
    }
}
