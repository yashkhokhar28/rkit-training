namespace EmployeeTaskManager.Models.ENUM
{
    /// <summary>
    /// Defines the type of entry or action in the system.
    /// </summary>
    public enum EnmEntryType
    {
        /// <summary>
        /// Represents an addition or creation action.
        /// </summary>
        A,

        /// <summary>
        /// Represents an edit or modification action.
        /// </summary>
        E,

        /// <summary>
        /// Represents a deletion action.
        /// </summary>
        D
    }

    /// <summary>
    /// Defines the status of a task in the EmployeeTaskManager system.
    /// </summary>
    public enum EnmStatus
    {
        /// <summary>
        /// Indicates the task is pending and has not yet started.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Indicates the task is currently in progress.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Indicates the task has been completed.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// Indicates the task is past its due date and overdue.
        /// </summary>
        Overdue = 3
    }

    /// <summary>
    /// Defines the priority level of a task in the EmployeeTaskManager system.
    /// </summary>
    public enum EnmPriority
    {
        /// <summary>
        /// Indicates a low-priority task.
        /// </summary>
        Low = 0,

        /// <summary>
        /// Indicates a medium-priority task.
        /// </summary>
        Medium = 1,

        /// <summary>
        /// Indicates a high-priority task.
        /// </summary>
        High = 2
    }

    /// <summary>
    /// Defines the roles a user can have in the EmployeeTaskManager system.
    /// </summary>
    public enum EnmRole
    {
        /// <summary>
        /// Represents an administrator with full system privileges.
        /// </summary>
        Admin = 0,

        /// <summary>
        /// Represents a standard employee with limited privileges.
        /// </summary>
        Employee = 1,

        /// <summary>
        /// Represents a manager with intermediate privileges, such as task assignment.
        /// </summary>
        Manager = 2
    }
}