using System;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // MySQL server connection information
            string mysqlServer = "localhost";
            string mysqlUserId = "Admin";
            string mysqlPassword = "gs@123";

            // PostgreSQL server connection information
            string postgresServer = "localhost";
            string postgresUserId = "srvuser";
            string postgresPassword = "Miracle@123";

            // Path to CSV file for data insertion
            var csvFilePath = @"C:\Users\yash.k\Downloads\R&D\ASR.csv";

            Console.WriteLine("=== Welcome to the Database Management System ===");

            // Main program loop for user input
            while (true)
            {
                // Display the menu options
                DisplayMenu();

                // Get and validate the user's choice
                int choice = GetUserChoice();

                // Exit the program if the user chooses 0
                if (choice == 0)
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                }

                // Process the user's choice with threading
                try
                {
                    Console.WriteLine($"Processing choice {choice}...");
                    Thread operationThread = new Thread(() =>
                    {
                        ProcessChoice(choice, mysqlServer, mysqlUserId, mysqlPassword, postgresServer, postgresUserId, postgresPassword, csvFilePath);
                    });

                    // Start the thread and wait for it to complete
                    // Assign a name based on the operation
                    operationThread.Name = $"OperationThread-{choice}";
                    operationThread.Start();
                    operationThread.Join();

                    Console.WriteLine($"Choice {choice} processed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while processing your request: {ex.Message}");
                }

                // Print a separator for better readability
                Console.WriteLine("\n========================================");
            }
        }

        /// <summary>
        /// Displays the menu options for the user.
        /// </summary>
        static void DisplayMenu()
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Create Databases (MySQL)");
            Console.WriteLine("2. Drop Databases (MySQL)");
            Console.WriteLine("3. Insert Data (MySQL)");
            Console.WriteLine("4. Create Databases (PostgreSQL)");
            Console.WriteLine("5. Drop Databases (PostgreSQL)");
            Console.WriteLine("6. Insert Data (PostgreSQL)");
            Console.WriteLine("0. Exit");
        }

        /// <summary>
        /// Gets and validates the user's menu choice.
        /// </summary>
        /// <returns>The selected choice as an integer.</returns>
        static int GetUserChoice()
        {
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return choice;
            }
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return -1; // Invalid choice
        }

        /// <summary>
        /// Processes the user's choice and calls the corresponding method.
        /// </summary>
        static void ProcessChoice(
            int choice,
            string mysqlServer,
            string mysqlUserId,
            string mysqlPassword,
            string postgresServer,
            string postgresUserId,
            string postgresPassword,
            string csvFilePath)
        {
            switch (choice)
            {
                case 1:
                    HandleCreateDatabasesMySQL(mysqlServer, mysqlUserId, mysqlPassword);
                    break;
                case 2:
                    HandleDropDatabasesMySQL(mysqlServer, mysqlUserId, mysqlPassword);
                    break;
                case 3:
                    HandleInsertDataMySQL(csvFilePath, mysqlServer, mysqlUserId, mysqlPassword);
                    break;
                case 4:
                    HandleCreateDatabasesPostgreSQL(postgresServer, postgresUserId, postgresPassword);
                    break;
                case 5:
                    HandleDropDatabasesPostgreSQL(postgresServer, postgresUserId, postgresPassword);
                    break;
                case 6:
                    HandleInsertDataPostgreSQL(csvFilePath, postgresServer, postgresUserId, postgresPassword);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        // === MySQL Operations ===

        static void HandleCreateDatabasesMySQL(string server, string userId, string password)
        {
            Console.WriteLine("[MySQL] Enter the number of databases to create: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"[MySQL] Creating {numberOfDatabases} databases...");
            CreateDatabaseMySQL.CreateDatabases(numberOfDatabases, Query.CreateTableQueryMySQL, server, userId, password);
            Console.WriteLine("[MySQL] Databases created successfully.");
        }

        static void HandleDropDatabasesMySQL(string server, string userId, string password)
        {
            Console.WriteLine("[MySQL] Enter the number of databases to drop: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"[MySQL] Dropping {numberOfDatabases} databases...");
            DropDatabaseMySQL.DropDatabases(numberOfDatabases, server, userId, password);
            Console.WriteLine("[MySQL] Databases dropped successfully.");
        }

        static void HandleInsertDataMySQL(string csvFilePath, string server, string userId, string password)
        {
            Console.WriteLine("[MySQL] Enter the number of databases to insert data into: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"[MySQL] Inserting data into {numberOfDatabases} databases...");
            InsertDataMySQL.InsertDataFromCsv(numberOfDatabases, csvFilePath, server, userId, password);
            Console.WriteLine("[MySQL] Data inserted successfully.");
        }

        // === PostgreSQL Operations ===

        static void HandleCreateDatabasesPostgreSQL(string server, string userId, string password)
        {
            Console.WriteLine("[PostgreSQL] Enter the number of databases to create: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"[PostgreSQL] Creating {numberOfDatabases} databases...");
            CreateDatabasePostgreSQL.CreateDatabases(numberOfDatabases, Query.CreateTableQueryPostgreSQL, server, userId, password);
            Console.WriteLine("[PostgreSQL] Databases created successfully.");
        }

        static void HandleDropDatabasesPostgreSQL(string server, string userId, string password)
        {
            Console.WriteLine("[PostgreSQL] Enter the number of databases to drop: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"[PostgreSQL] Dropping {numberOfDatabases} databases...");
            DropDatabasePostgreSQL.DropDatabases(numberOfDatabases, server, userId, password);
            Console.WriteLine("[PostgreSQL] Databases dropped successfully.");
        }

        static void HandleInsertDataPostgreSQL(string csvFilePath, string server, string userId, string password)
        {
            Console.WriteLine("[PostgreSQL] Enter the number of databases to insert data into: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"[PostgreSQL] Inserting data into {numberOfDatabases} databases...");
            InsertDataPostgreSQL.InsertDataFromCsvPostgres(numberOfDatabases, csvFilePath, server, userId, password);
            Console.WriteLine("[PostgreSQL] Data inserted successfully.");
        }
    }
}