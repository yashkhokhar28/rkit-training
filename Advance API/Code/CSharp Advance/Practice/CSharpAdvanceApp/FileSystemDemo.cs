using OfficeOpenXml;
using System.Text;

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

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            EPPlusDemo objEPPlusDemo = new EPPlusDemo();
            objEPPlusDemo.RunEPPlusDemo();
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

    public class EPPlusDemo
    {
        public void RunEPPlusDemo()
        {
            Console.WriteLine("============EPPlusDemo================\n");

            // Get the current working directory
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");

            // Create a directory for file operations if it doesn't exist
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

            // Create a subdirectory for the EPPlusDemo
            DirectoryInfo objDirectoryInfo = new DirectoryInfo(directoryPath);
            Console.WriteLine($"Newly Created Directory: {objDirectoryInfo.Name}");
            objDirectoryInfo.CreateSubdirectory("EPPlusDemo"); // Create subdirectory for the demo

            string directoryPath1 = Path.Combine(objDirectoryInfo.Name, "EPPlusDemo");
            if (!Directory.Exists(directoryPath1))
            {
                Directory.CreateDirectory(directoryPath1); // Ensure subdirectory exists
                Console.WriteLine($"Directory created: {directoryPath1}");
            }
            else
            {
                Console.WriteLine($"Directory available: {directoryPath1}");
            }

            // Define the path for the Excel file
            string filePath = Path.Combine(directoryPath1, "EPPlusDemo.xlsx");

            // Method to create a new Excel file
            void CreateExcelFile()
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    // Add a worksheet to the Excel file
                    var worksheet = package.Workbook.Worksheets.Add("MySheet");

                    // Add data to specific cells
                    worksheet.Cells[1, 1].Value = "Name";
                    worksheet.Cells[1, 2].Value = "Age";
                    worksheet.Cells[2, 1].Value = "Yash Khokhar";
                    worksheet.Cells[2, 2].Value = 20;
                    worksheet.Cells[3, 1].Value = "Maulik Bhatt";
                    worksheet.Cells[3, 2].Value = 20;

                    // Save the file to disk
                    package.Save();
                    Console.WriteLine("Excel file created successfully.");
                }
            }

            // Method to read data from the Excel file
            void ReadExcelFile()
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Get the first worksheet
                    int rowCount = worksheet.Dimension.Rows; // Get number of rows

                    Console.WriteLine("Reading data from the Excel file:");
                    // Loop through each row and print the values of Name and Age columns
                    for (int row = 1; row <= rowCount; row++)
                    {
                        string name = worksheet.Cells[row, 1].Text;
                        string age = worksheet.Cells[row, 2].Text;
                        Console.WriteLine($"Name: {name}, Age: {age}");
                    }
                }
            }

            // Method to update data in the Excel file
            void UpdateExcelFile()
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Get the first worksheet

                    // Update the values in existing rows
                    worksheet.Cells[2, 2].Value = 31; // Update Yash's age
                    worksheet.Cells[3, 2].Value = 26; // Update Maulik's age

                    // Add a new row with data
                    worksheet.Cells[4, 1].Value = "Iron Man";
                    worksheet.Cells[4, 2].Value = 220;

                    // Save the changes to the file
                    package.Save();
                    Console.WriteLine("Excel file updated successfully.");
                }
            }

            // Execute the methods
            CreateExcelFile(); // Create the Excel file
            ReadExcelFile();   // Read and display data from the file
            UpdateExcelFile(); // Update the file with new data
        }
    }
}