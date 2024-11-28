using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToDoApplication;

/// <summary>
/// Manages tasks by adding, updating, deleting, viewing, saving, and loading tasks.
/// Implements the ITaskOperations interface.
/// </summary>
public class TaskManager : ITaskOperations
{
    private List<Task> tasks = new List<Task>();
    private int nextId = 1;

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
        var task = new Task(nextId++, title, description);
        tasks.Add(task);
        Console.WriteLine("Task added successfully.");
    }

    /// <summary>
    /// Updates the status of an existing task.
    /// </summary>
    /// <param name="taskId">The ID of the task to update.</param>
    /// <param name="status">The new status of the task.</param>
    public void UpdateTaskStatus(int taskId, EnmTaskStatus status)
    {
        var task = tasks.FirstOrDefault(t => t.Id == taskId);
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
        var task = tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            tasks.Remove(task);
            Console.WriteLine("Task deleted successfully.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }

    /// <summary>
    /// Displays all tasks in the list.
    /// </summary>
    public void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        Console.WriteLine("Task List:");
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    /// <summary>
    /// Saves the current tasks to a file.
    /// </summary>
    /// <param name="filePath">The file path where tasks should be saved.</param>
    public void SaveToFile(string filePath)
    {
        try
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
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

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    var task = new Task(int.Parse(parts[0]), parts[1], parts[2])
                    {
                        Status = (EnmTaskStatus)Enum.Parse(typeof(EnmTaskStatus), parts[3])
                    };
                    tasks.Add(task);
                }
            }
            Console.WriteLine("Tasks loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from file: {ex.Message}");
        }
    }
}
