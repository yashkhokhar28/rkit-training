using System;
using System.IO;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates basic file operations in C# including writing to a file, 
/// reading from a file, appending to a file, and handling common exceptions.
/// </summary>
public class FileOperationDemo
{
    /// <summary>
    /// Executes a series of file operations such as writing, reading, 
    /// and appending content to a file, along with exception handling for 
    /// common file-related errors.
    /// </summary>
    public static void RunFileOperationDemo()
    {
        // Define the file path for the operations
        string filePath = "sample.txt";

        try
        {
            // Example 1: Writing to a file
            string content = "Hello, World!";
            // Write the string content to the file (overwrites if the file exists)
            File.WriteAllText(filePath, content);
            Console.WriteLine("Content written to file."); // Notify the user about the operation

            // Example 2: Reading from a file
            // Read the content of the file
            string readContent = File.ReadAllText(filePath);
            Console.WriteLine("File content: " + readContent); // Output the content read from the file

            // Example 3: Appending to a file
            string appendContent = "Appending some more text.";
            // Append the additional content to the file
            File.AppendAllText(filePath, appendContent);
            Console.WriteLine("Content appended to file."); // Notify the user about the append operation

            // Example 4: Reading the file after appending
            // Read the content of the file again to show the updated content
            string appendedContent = File.ReadAllText(filePath);
            Console.WriteLine("Appended content: " + appendedContent); // Output the appended content
        }
        catch (UnauthorizedAccessException ex)
        {
            // Handle exception when access to the file is denied
            Console.WriteLine("Error: Unauthorized access to the file.");
            Console.WriteLine(ex.Message); // Output the exception message
        }
        catch (FileNotFoundException ex)
        {
            // Handle exception when the file is not found
            Console.WriteLine("Error: The file could not be found.");
            Console.WriteLine(ex.Message); // Output the exception message
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}