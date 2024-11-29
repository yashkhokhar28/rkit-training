namespace ToDoApplication;
/// <summary>
/// Defines the operations for managing tasks, such as adding, updating, deleting, viewing,
/// saving, and loading tasks.
/// </summary>
public interface ITaskOperations
{

    /// <summary>Adds the task.</summary>
    /// <param name="title">The title.</param>
    /// <param name="description">The description.</param>
    void AddTask(string title, string description);

    /// <summary>
    /// Updates the status of an existing task identified by its unique ID.
    /// </summary>
    /// <param name="taskId">The unique identifier of the task to be updated.</param>
    /// <param name="status">The new status of the task (e.g., Pending, Completed).</param>
    void UpdateTaskStatus(int taskId, EnmTaskStatus status);

    /// <summary>
    /// Deletes a task identified by its unique ID.
    /// </summary>
    /// <param name="taskId">The unique identifier of the task to be deleted.</param>
    void DeleteTask(int taskId);

    /// <summary>
    /// Displays all tasks, including their ID, title, description, and status.
    /// </summary>
    void ViewTasks();

    /// <summary>
    /// Saves all tasks to a specified file in a structured format.
    /// </summary>
    /// <param name="filePath">The file path where tasks will be saved.</param>
    void SaveToFile(string filePath);

    /// <summary>
    /// Loads tasks from a specified file and populates the task list.
    /// </summary>
    /// <param name="filePath">The file path from where tasks will be loaded.</param>
    void LoadFromFile(string filePath);
}