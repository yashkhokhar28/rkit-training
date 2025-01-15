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

            // Path to CSV file
            var csvFilePath = @"C:\Users\yash.k\Downloads\R&D\ASR.csv";

            while (true) // Infinite loop for repeated operations
            {
                // Display available choices for the user to select
                DisplayMenu();

                // Get the user's choice
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                // Exit the program if the user chooses 0
                if (choice == 0)
                {
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;
                }

                // Process based on user input
                try
                {
                    ProcessChoice(choice, mysqlServer, mysqlUserId, mysqlPassword, postgresServer, postgresUserId, postgresPassword, csvFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                // Separator for readability
                Console.WriteLine("\n----------------------------------------");
            }
        }

        // Display available choices for the user to select
        static void DisplayMenu()
        {
            Console.WriteLine("Enter Choice : ");
            Console.WriteLine("1. Create Databases (MySQL)");
            Console.WriteLine("2. Drop Databases (MySQL)");
            Console.WriteLine("3. Insert Data (MySQL)");
            Console.WriteLine("4. Create Databases (PostgreSQL)");
            Console.WriteLine("5. Drop Databases (PostgreSQL)");
            Console.WriteLine("6. Insert Data (PostgreSQL)");
            Console.WriteLine("0. Exit");
        }

        // Process the user's choice and call the relevant methods
        static void ProcessChoice(int choice, string mysqlServer, string mysqlUserId, string mysqlPassword, string postgresServer, string postgresUserId, string postgresPassword, string csvFilePath)
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
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }

        // MySQL Operations
        static void HandleCreateDatabasesMySQL(string server, string userId, string password)
        {
            Console.WriteLine("Enter Number Of Databases to Create: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            CreateDatabaseMySQL.CreateDatabases(numberOfDatabases, Query.CreateTableQueryMySQL, server, userId, password);
        }

        static void HandleDropDatabasesMySQL(string server, string userId, string password)
        {
            Console.WriteLine("Enter Number Of Databases to Drop: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            DropDatabaseMySQL.DropDatabases(numberOfDatabases, server, userId, password);
        }

        static void HandleInsertDataMySQL(string csvFilePath, string server, string userId, string password)
        {
            Console.WriteLine("Enter Number Of Databases to Insert Data into: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            InsertDataMySQL.InsertDataFromCsv(numberOfDatabases, csvFilePath, server, userId, password);
        }

        // PostgreSQL Operations
        static void HandleCreateDatabasesPostgreSQL(string server, string userId, string password)
        {
            Console.WriteLine("Enter Number Of Databases to Create: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            CreateDatabasePostgreSQL.CreateDatabases(numberOfDatabases, Query.CreateTableQueryPostgreSQL, server, userId, password);
        }

        static void HandleDropDatabasesPostgreSQL(string server, string userId, string password)
        {
            Console.WriteLine("Enter Number Of Databases to Drop: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            DropDatabasePostgreSQL.DropDatabases(numberOfDatabases, server, userId, password);
        }

        static void HandleInsertDataPostgreSQL(string csvFilePath, string server, string userId, string password)
        {
            Console.WriteLine("Enter Number Of Databases to Insert Data into: ");
            int numberOfDatabases = Convert.ToInt32(Console.ReadLine());
            InsertDataPostgreSQL.InsertDataFromCsvPostgres(numberOfDatabases, csvFilePath, server, userId, password);
        }
    }
}