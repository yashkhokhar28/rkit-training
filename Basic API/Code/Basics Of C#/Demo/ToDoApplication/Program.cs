using System;
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
        TaskManager manager = new TaskManager();
        string filePath = "TaskData.txt";

        // Load tasks from the file at the start of the program
        manager.LoadFromFile(filePath);

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
                case "1":
                    // Add a new task
                    Console.Write("Enter task title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter task description: ");
                    string description = Console.ReadLine();
                    manager.AddTask(title, description);
                    break;
                case "2":
                    // View all tasks
                    manager.ViewTasks();
                    break;
                case "3":
                    // Update task status (Pending/Completed)
                    Console.Write("Enter task ID to update: ");
                    int idToUpdate = int.Parse(Console.ReadLine());
                    Console.Write("Enter new status (0: Pending, 1: Completed): ");
                    EnmTaskStatus status = (EnmTaskStatus)Enum.Parse(typeof(EnmTaskStatus), Console.ReadLine());
                    manager.UpdateTaskStatus(idToUpdate, status);
                    break;
                case "4":
                    // Delete a task
                    Console.Write("Enter task ID to delete: ");
                    int idToDelete = int.Parse(Console.ReadLine());
                    manager.DeleteTask(idToDelete);
                    break;
                case "5":
                    // Save tasks to file and exit
                    manager.SaveToFile(filePath);
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
}

