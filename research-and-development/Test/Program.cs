using System;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // MySQL server connection information
            string mysqlServer = "localhost";
            string mysqlUserId = "Admin";
            string mysqlPassword = "gs@123";

            // PostgreSQL server connection information
            string postgresServer = "localhost";
            string postgresUserId = "postgres";
            string postgresPassword = "root";

            // MS SQL server connection information
            string msSqlServer = "SRVTRAINING\\SQLEXPRESS";

            // Path to CSV file for data insertion
            var csvFilePath = @"F:\Yash Khokhar\R&D\Dataset\ASR.csv";

            Console.WriteLine("=== Welcome to the Database Management System ===");

            while (true)
            {
                // Display the menu options
                DisplayMenu();

                // Get and validate the user's choice
                int choice = GetUserChoice();

                if (choice == 0)
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                }

                try
                {
                    Console.WriteLine($"Processing choice {choice}...");

                    // Ensure all operations are properly awaited
                    await ProcessChoiceAsync(choice, mysqlServer, mysqlUserId, mysqlPassword, postgresServer, postgresUserId, postgresPassword, msSqlServer, csvFilePath);

                    Console.WriteLine($"Choice {choice} processed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while processing your request: {ex.Message}");
                }

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
            Console.WriteLine("7. Create Databases (MS SQL)");
            Console.WriteLine("8. Drop Databases (MS SQL)");
            Console.WriteLine("9. Insert Data (MS SQL)");
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
        static async Task ProcessChoiceAsync(
            int choice,
            string mysqlServer,
            string mysqlUserId,
            string mysqlPassword,
            string postgresServer,
            string postgresUserId,
            string postgresPassword,
            string msSqlServer,
            string csvFilePath)
        {
            switch (choice)
            {
                case 1:
                    await HandleCreateDatabasesMySQL(mysqlServer, mysqlUserId, mysqlPassword);
                    break;
                case 2:
                    await Task.Run(() => HandleDropDatabasesMySQL(mysqlServer, mysqlUserId, mysqlPassword));
                    break;
                case 3:
                    await Task.Run(() => HandleInsertDataMySQL(csvFilePath, mysqlServer, mysqlUserId, mysqlPassword));
                    break;
                case 4:
                    await Task.Run(() => HandleCreateDatabasesPostgreSQL(postgresServer, postgresUserId, postgresPassword));
                    break;
                case 5:
                    await Task.Run(() => HandleDropDatabasesPostgreSQL(postgresServer, postgresUserId, postgresPassword));
                    break;
                case 6:
                    await Task.Run(() => HandleInsertDataPostgreSQL(csvFilePath, postgresServer, postgresUserId, postgresPassword));
                    break;
                case 7:
                    await Task.Run(() => HandleCreateDatabasesMSSQL(msSqlServer));
                    break;
                case 8:
                    await Task.Run(() => HandleDropDatabasesMSSQL(msSqlServer));
                    break;
                case 9:
                    await Task.Run(() => HandleInsertDataMSSQL(csvFilePath, msSqlServer));
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        // === MySQL Operations ===
        static async Task HandleCreateDatabasesMySQL(string server, string userId, string password)
        {
            Console.WriteLine("[MySQL] Enter the starting index of databases to create: ");
            int from = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MySQL] Enter the ending index of databases to create: ");
            int to = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MySQL] Enter the starting index of tables to create: ");
            int fromTable = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MySQL] Enter the ending index of tables to create: ");
            int toTable = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"[MySQL] Creating databases from {from} to {to}, with tables from {fromTable} to {toTable}...");
            await CreateDatabaseMySQL.CreateDatabasesAsync(from, to, fromTable, toTable, Query.CreateTableQueryMySQL, server, userId, password);
            Console.WriteLine("[MySQL] Databases and tables created successfully.");
        }


        static void HandleDropDatabasesMySQL(string server, string userId, string password)
        {
            Console.WriteLine("[MySQL] Enter the starting index of databases to drop: ");
            int from = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MySQL] Enter the ending index of databases to drop: ");
            int to = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"[MySQL] Dropping databases from {from} to {to}...");
            DropDatabaseMySQL.DropDatabases(from, to, server, userId, password);
            Console.WriteLine("[MySQL] Databases dropped successfully.");
        }

        static void HandleInsertDataMySQL(string csvFilePath, string server, string userId, string password)
        {
            Console.WriteLine("[MySQL] Enter the starting index of databases to insert data into: ");
            int from = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MySQL] Enter the ending index of databases to insert data into: ");
            int to = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MySQL] Enter the starting index of tables to insert data into: ");
            int fromTable = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MySQL] Enter the ending index of tables to insert data into: ");
            int toTable = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"[MySQL] Inserting data into databases from {from} to {to}, across tables {fromTable} to {toTable}...");
            InsertDataMySQL.InsertDataFromCsv(from, to, csvFilePath, server, userId, password, fromTable, toTable);
            Console.WriteLine("[MySQL] Data inserted successfully.");
        }



        // === PostgreSQL Operations ===

        static void HandleCreateDatabasesPostgreSQL(string server, string userId, string password)
        {
            Console.WriteLine("[PostgreSQL] Enter the starting index of databases to create: ");
            int from = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[PostgreSQL] Enter the ending index of databases to create: ");
            int to = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"[PostgreSQL] Creating databases from {from} to {to}...");
            CreateDatabasePostgreSQL.CreateDatabases(from, to, Query.CreateTableQueryPostgreSQL, server, userId, password);
            Console.WriteLine("[PostgreSQL] Databases created successfully.");
        }

        static void HandleDropDatabasesPostgreSQL(string server, string userId, string password)
        {
            Console.WriteLine("[PostgreSQL] Enter the starting index of databases to drop: ");
            int from = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[PostgreSQL] Enter the ending index of databases to drop: ");
            int to = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"[PostgreSQL] Dropping databases from {from} to {to}...");
            DropDatabasePostgreSQL.DropDatabases(from, to, server, userId, password);
            Console.WriteLine("[PostgreSQL] Databases dropped successfully.");
        }

        static void HandleInsertDataPostgreSQL(string csvFilePath, string server, string userId, string password)
        {
            Console.WriteLine("[PostgreSQL] Enter the starting index of databases to insert data into: ");
            int from = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[PostgreSQL] Enter the ending index of databases to insert data into: ");
            int to = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"[PostgreSQL] Inserting data into databases from {from} to {to}...");
            InsertDataPostgreSQL.InsertDataFromCsvPostgres(from, to, csvFilePath, server, userId, password);
            Console.WriteLine("[PostgreSQL] Data inserted successfully.");
        }

        // === MS SQL Operations ===

        static void HandleCreateDatabasesMSSQL(string server)
        {
            Console.WriteLine("[MS SQL] Enter the starting index of databases to create: ");
            int from = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MS SQL] Enter the ending index of databases to create: ");
            int to = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"[MS SQL] Creating databases from {from} to {to}...");
            CreateDatabaseMSSQL.CreateDatabases(from, to, Query.CreateTableQueryMSSQL, server);
            Console.WriteLine("[MS SQL] Databases created successfully.");
        }

        static void HandleDropDatabasesMSSQL(string server)
        {
            Console.WriteLine("[MS SQL] Enter the starting index of databases to drop: ");
            int from = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MS SQL] Enter the ending index of databases to drop: ");
            int to = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"[MS SQL] Dropping databases from {from} to {to}...");
            DropDatabaseMSSQL.DropDatabases(from, to, server);
            Console.WriteLine("[MS SQL] Databases dropped successfully.");
        }

        static void HandleInsertDataMSSQL(string csvFilePath, string server)
        {
            Console.WriteLine("[MS SQL] Enter the starting index of databases to insert data into: ");
            int from = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("[MS SQL] Enter the ending index of databases to insert data into: ");
            int to = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"[MS SQL] Inserting data into databases from {from} to {to}...");
            InsertDataMSSQL.InsertDataFromCsv(from, to, csvFilePath, server);
            Console.WriteLine("[MS SQL] Data inserted successfully.");
        }
    }
}