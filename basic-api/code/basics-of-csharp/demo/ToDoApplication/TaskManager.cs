namespace ToDoApplication;

/// <summary>
/// Manages tasks by adding, updating, deleting, viewing, saving, and loading tasks.
/// Implements the ITaskOperations interface.
/// </summary>
public class TaskManager : DisplayTasks, ITaskOperations
{
    #region Fields

    private List<Task> lstTasks = new List<Task>();
    private int nextId = 1;

    #endregion

    #region Task Management Methods

    /// <summary>
    /// Adds a new task to the list.
    /// </summary>
    /// <param name="title">The title of the task.</param>
    /// <param name="description">The description of the task.</param>
    public void AddTask(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            return;
        }
        Task task = new Task(nextId++, title, description);
        lstTasks.Add(task);
        Console.WriteLine("Task added successfully.");
    }

    /// <summary>
    /// Updates the status of an existing task.
    /// </summary>
    /// <param name="taskId">The ID of the task to update.</param>
    /// <param name="status">The new status of the task.</param>
    public void UpdateTaskStatus(int taskId, EnmTaskStatus status)
    {
        Task task = lstTasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            task.Status = status;
            Console.WriteLine("Task status updated successfully.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }

    /// <summary>
    /// Deletes a task by its ID.
    /// </summary>
    /// <param name="taskId">The ID of the task to delete.</param>
    public void DeleteTask(int taskId)
    {
        Task task = lstTasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            lstTasks.Remove(task);
            Console.WriteLine("Task deleted successfully.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }

    #endregion

    #region Display Methods

    /// <summary>
    /// Displays all tasks in the list.
    /// </summary>
    public override void ViewTasks()
    {
        if (lstTasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        Console.WriteLine("Task List:");
        foreach (Task task in lstTasks)
        {
            Console.WriteLine(task);
        }
    }

    #endregion

    #region File Operations

    /// <summary>
    /// Saves the current tasks to a file.
    /// </summary>
    /// <param name="filePath">The file path where tasks should be saved.</param>
    public void SaveToFile(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Task task in lstTasks)
                {
                    writer.WriteLine($"{task.Id}|{task.Title}|{task.Description}|{task.Status}");
                }
            }
            Console.WriteLine("Tasks saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to file: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads tasks from a file.
    /// </summary>
    /// <param name="filePath">The file path from which tasks should be loaded.</param>
    public void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }
        else
        {
            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine($"Full Name: {fileInfo.FullName}");
            Console.WriteLine($"Name: {fileInfo.Name}");
            Console.WriteLine($"Length: {fileInfo.Length} bytes");
            Console.WriteLine($"Extension: {fileInfo.Extension}");
            Console.WriteLine($"Created: {fileInfo.CreationTime}");
            Console.WriteLine($"Last Accessed: {fileInfo.LastAccessTime}");
            Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
        }

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    Task task = new Task(int.Parse(parts[0]), parts[1], parts[2])
                    {
                        Status = (EnmTaskStatus)Enum.Parse(typeof(EnmTaskStatus), parts[3])
                    };
                    lstTasks.Add(task);
                }
            }
            Console.WriteLine("Tasks loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from file: {ex.Message}");
        }
    }

    #endregion
}
