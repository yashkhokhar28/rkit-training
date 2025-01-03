using System.Text;
using System.Threading;

namespace CSharpAdvanceApp
{
    /// <summary>
    /// Demonstrates file system operations using various classes in .NET.
    /// </summary>
    public class FileSystemDemo
    {
        /// <summary>
        /// Executes demonstrations for DriveInfo, Directory, and FileStream operations.
        /// </summary>
        public void RunFileSystemDemo()
        {
            // Demonstrating DriveInfo class operations
            DriveInfoClassDemo objDriveInfoClassDemo = new DriveInfoClassDemo();
            objDriveInfoClassDemo.RunDriveInfoClassDemo();

            // Demonstrating Directory class operations
            DirectoryClassDemo objDirectoryClassDemo = new DirectoryClassDemo();
            objDirectoryClassDemo.RunDirectoryClassDemo();

            // Demonstrating FileStream class operations
            FileStreamClassDemo objFileStreamClassDemo = new FileStreamClassDemo();
            objFileStreamClassDemo.RunFileStreamClassDemo();
        }
    }

    /// <summary>
    /// Demonstrates the use of the DriveInfo class to access information about drives.
    /// </summary>
    public class DriveInfoClassDemo
    {
        /// <summary>
        /// Displays details of all available drives and allows querying specific drive details.
        /// </summary>
        public void RunDriveInfoClassDemo()
        {
            Console.WriteLine("============DriveInfoClassDemo================\n");

            // Get all available drives
            DriveInfo[] arrDriveInfo = DriveInfo.GetDrives();
            Console.WriteLine("Total Partitions Of Drive:");

            foreach (DriveInfo driveInfo in arrDriveInfo)
            {
                Console.WriteLine(driveInfo.Name); // Display drive names
            }

            // Query details for a specific drive
            Console.Write("\nEnter the Partition Name To Get Details: ");
            string partitionName = Console.ReadLine();
            DriveInfo objDriveInfo = new DriveInfo(partitionName.ToUpper());
            Console.WriteLine("\n");

            // Display drive information
            Console.WriteLine($"Drive Name: {objDriveInfo.Name}");
            Console.WriteLine($"Total Space: {objDriveInfo.TotalSize}");
            Console.WriteLine($"Free Space: {objDriveInfo.TotalFreeSpace}");
            Console.WriteLine($"Drive Format: {objDriveInfo.DriveFormat}");
            Console.WriteLine($"Volume Label: {objDriveInfo.VolumeLabel}");
            Console.WriteLine($"Drive Type: {objDriveInfo.DriveType}");
            Console.WriteLine($"Root Directory: {objDriveInfo.RootDirectory}");
            Console.WriteLine($"Is Ready: {objDriveInfo.IsReady}");
            Console.WriteLine("\n");
        }
    }

    /// <summary>
    /// Demonstrates the use of the Directory class for managing directories.
    /// </summary>
    public class DirectoryClassDemo
    {
        /// <summary>
        /// Creates and manages directories using the Directory and DirectoryInfo classes.
        /// </summary>
        public void RunDirectoryClassDemo()
        {
            Console.WriteLine("============DirectoryClassDemo================\n");

            // Get the current working directory
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");

            // Create a new directory if it doesn't exist
            string directoryPath = Path.Combine(currentDirectory, "FileSystem");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // Create directory
                Console.WriteLine($"Directory created: {directoryPath}");
            }
            else
            {
                Console.WriteLine($"Directory available: {directoryPath}");
            }

            // Create a subdirectory
            DirectoryInfo objDirectoryInfo = new DirectoryInfo(directoryPath);
            Console.WriteLine($"Newly Created Directory: {objDirectoryInfo.Name}");
            objDirectoryInfo.CreateSubdirectory("DirectoryClassDemo"); // Create subdirectory

            // Verify subdirectory creation
            string directoryPath1 = Path.Combine(objDirectoryInfo.Name, "DirectoryClassDemo");
            if (!Directory.Exists(directoryPath1))
            {
                Directory.CreateDirectory(directoryPath1); // Ensure directory exists
                Console.WriteLine($"Directory created: {directoryPath1}");
            }
            else
            {
                Console.WriteLine($"Directory available: {directoryPath1}");
            }
            Console.WriteLine("\n");
        }
    }

    /// <summary>
    /// Demonstrates the use of the FileStream class for file operations.
    /// </summary>
    public class FileStreamClassDemo
    {
        /// <summary>
        /// Performs read and write operations using the FileStream class.
        /// </summary>
        public void RunFileStreamClassDemo()
        {
            Console.WriteLine("============FileStreamClassDemo================\n");

            // Get the current working directory
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");

            // Create a directory for file operations
            string directoryPath = Path.Combine(currentDirectory, "FileSystem");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // Create directory if not exists
                Console.WriteLine($"Directory created: {directoryPath}");
            }
            else
            {
                Console.WriteLine($"Directory available: {directoryPath}");
            }

            // Create a subdirectory for FileStreamDemo
            DirectoryInfo objDirectoryInfo = new DirectoryInfo(directoryPath);
            Console.WriteLine($"Newly Created Directory: {objDirectoryInfo.Name}");
            objDirectoryInfo.CreateSubdirectory("FileStreamClassDemo"); // Create subdirectory

            string directoryPath1 = Path.Combine(objDirectoryInfo.Name, "FileStreamClassDemo");
            if (!Directory.Exists(directoryPath1))
            {
                Directory.CreateDirectory(directoryPath1); // Ensure subdirectory exists
                Console.WriteLine($"Directory created: {directoryPath1}");
            }
            else
            {
                Console.WriteLine($"Directory available: {directoryPath1}");
            }

            // Create and write to a file
            string filePath = Path.Combine(directoryPath1, "FileStreamClassDemo.txt");
            FileInfoClassDemo objFileInfoClassDemo = new FileInfoClassDemo();
            objFileInfoClassDemo.RunFileInfoClassDemo(filePath);

            using (FileStream objFileStream1 = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Writing text to file
                string text = "This is some text written to the textfile";
                byte[] arrText = Encoding.UTF8.GetBytes(text);
                objFileStream1.Write(arrText, 0, arrText.Length); // Write to file
                Console.WriteLine("File Written Successfully");
            }

            // Demonstrate reading the file
            StreamClassDemo objStreamClassDemo = new StreamClassDemo();
            objStreamClassDemo.RunStreamClassDemo(filePath);

            // Read from the file using FileStream
            using (FileStream objFileStream2 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] arrRead = new byte[1024];
                int count;
                Console.WriteLine("============Reading Using FileStream================\n");
                while ((count = objFileStream2.Read(arrRead, 0, arrRead.Length)) > 0)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(arrRead, 0, count)); // Output file content
                }
                Console.WriteLine("File Read Successfully");
            }
            Console.WriteLine("\n");
        }
    }

    /// <summary>
    /// Demonstrates the use of the FileInfo class for accessing file metadata.
    /// </summary>
    public class FileInfoClassDemo
    {
        /// <summary>
        /// Displays file information such as name, size, and timestamps.
        /// </summary>
        /// <param name="filePath">Path of the file to analyze.</param>
        public void RunFileInfoClassDemo(string filePath)
        {
            Console.WriteLine("============FileInfoClassDemo================\n");

            // Display file details
            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine($"Full Name: {fileInfo.FullName}");
            Console.WriteLine($"Name: {fileInfo.Name}");
            Console.WriteLine($"Length: {fileInfo.Length} bytes");
            Console.WriteLine($"Extension: {fileInfo.Extension}");
            Console.WriteLine($"Created: {fileInfo.CreationTime}");
            Console.WriteLine($"Last Accessed: {fileInfo.LastAccessTime}");
            Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
        }
    }

    /// <summary>
    /// Demonstrates the use of the StreamReader class for reading text files.
    /// </summary>
    public class StreamClassDemo
    {
        /// <summary>
        /// Reads the contents of a file using StreamReader.
        /// </summary>
        /// <param name="filePath">Path of the file to read.</param>
        public void RunStreamClassDemo(string filePath)
        {
            Console.WriteLine("============StreamClassDemo================\n");
            Console.WriteLine("============Reading Using StreamReader================\n");

            // Read the file using StreamReader
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line); // Output each line
                }
            }
        }
    }
}