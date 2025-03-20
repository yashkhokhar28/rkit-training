namespace ToDoApplication;

/// <summary>
/// Represents the status of a task.
/// </summary>
#region Enums
public enum EnmTaskStatus
{
    Pending,   // Task is pending
    Completed  // Task is completed
}
#endregion

/// <summary>
/// Represents a task with an ID, title, description, and status.
/// </summary>
#region Classes
public class Task
{
    #region Properties

    /// <summary>
    /// Unique identifier for the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The title of the task.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The description of the task.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the current status of the task.
    /// Defaults to <see cref="EnmTaskStatus.Pending"/>.
    /// </summary>
    public EnmTaskStatus Status { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Task"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the task.</param>
    /// <param name="title">The title of the task.</param>
    /// <param name="description">The detailed description of the task.</param>
    public Task(int id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = EnmTaskStatus.Pending;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Returns a string representation of the task, including its ID, title, description, and status.
    /// </summary>
    /// <returns>A formatted string containing task details.</returns>
    public override string ToString()
    {
        return $"{Id}. {Title} - {Description} [{Status}]";
    }

    #endregion
}
#endregion