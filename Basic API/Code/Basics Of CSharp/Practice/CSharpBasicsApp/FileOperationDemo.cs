using System;
using System.IO;

namespace CSharpBasicsApp;
public class FileOperationDemo
{
    /// <summary>
    /// Runs the file operation demo.
    /// </summary>
    public static void RunFileOperationDemo()
    {
        // Get the current directory
        string currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine($"Current Directory: {currentDirectory}");

        // Define the directory and file paths within the current directory
        string directoryPath = Path.Combine(currentDirectory, "SampleDir");
        string filePath = Path.Combine(directoryPath, "sample.txt");

        try
        {
            // Create a directory within the current directory
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"Directory created: {directoryPath}");
            }

            // Perform file operations
            string content = "Hello, World!";
            File.WriteAllText(filePath, content);
            Console.WriteLine("Content written to file.");

            string readContent = File.ReadAllText(filePath);
            Console.WriteLine("File content: " + readContent);

            File.AppendAllText(filePath, " Appending more text.");
            Console.WriteLine("Content appended to file.");

            string appendedContent = File.ReadAllText(filePath);
            Console.WriteLine("Appended content: " + appendedContent);

            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine($"Full Name: {fileInfo.FullName}");
            Console.WriteLine($"Name: {fileInfo.Name}");
            Console.WriteLine($"Length: {fileInfo.Length} bytes");
            Console.WriteLine($"Extension: {fileInfo.Extension}");
            Console.WriteLine($"Created: {fileInfo.CreationTime}");
            Console.WriteLine($"Last Accessed: {fileInfo.LastAccessTime}");
            Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");


            // Clean up
            //if (File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //    Console.WriteLine($"File deleted: {filePath}");
            //}

            //if (Directory.Exists(directoryPath))
            //{
            //    Directory.Delete(directoryPath, recursive: true);
            //    Console.WriteLine($"Directory deleted: {directoryPath}");
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}