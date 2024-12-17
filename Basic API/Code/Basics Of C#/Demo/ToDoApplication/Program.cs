using System;
using System.IO;
using ToDoApplication;
using EnmTaskStatus = ToDoApplication.EnmTaskStatus;

/// <summary>
/// Entry point for the To-Do List application.
/// Provides a menu-driven interface for adding, viewing, updating, and deleting tasks.
/// </summary>
class Program
{
    /// <summary>Main method that runs the application.</summary>
    static void Main()
    {
        #region Initialization
        TaskManager objManager = new TaskManager();

        // Get the current directory
        string currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine($"Current Directory: {currentDirectory}");

        // Define the directory and file paths within the current directory
        string directoryPath = Path.Combine(currentDirectory, "ToDoListApplication");

        // Create the directory if it doesn't exist
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine($"Directory created: {directoryPath}");
        }

        string filePath = Path.Combine(directoryPath, "TaskData.txt");

        // Create the file if it doesn't exist
        if (!File.Exists(filePath))
        {
            using (File.Create(filePath)) { }
            Console.WriteLine($"File created: {filePath}");
        }

        // Load tasks from the file at the start of the program
        objManager.LoadFromFile(filePath);
        #endregion

        #region Main Loop
        bool exit = false;
        while (!exit)
        {
            // Displaying the options menu
            Console.WriteLine("\nTo-Do List App");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Update Task Status");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Save & Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                #region Add Task
                case "1":
                    Console.Write("Enter task title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter task description: ");
                    string description = Console.ReadLine();
                    objManager.AddTask(title, description);
                    break;
                #endregion

                #region View Tasks
                case "2":
                    objManager.ViewTasks();
                    break;
                #endregion

                #region Update Task Status
                case "3":
                    Console.Write("Enter task ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int idToUpdate))
                    {
                        Console.Write("Enter new status (0: Pending, 1: Completed): ");
                        if (Enum.TryParse<EnmTaskStatus>(Console.ReadLine(), out EnmTaskStatus status))
                        {
                            objManager.UpdateTaskStatus(idToUpdate, status);
                        }
                        else
                        {
                            Console.WriteLine("Invalid status entered.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid task ID.");
                    }
                    break;
                #endregion

                #region Delete Task
                case "4":
                    Console.Write("Enter task ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int idToDelete))
                    {
                        objManager.DeleteTask(idToDelete);
                    }
                    else
                    {
                        Console.WriteLine("Invalid task ID.");
                    }
                    break;
                #endregion

                #region Save & Exit
                case "5":
                    objManager.SaveToFile(filePath);
                    exit = true;
                    break;
                #endregion

                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
        #endregion
    }
}
