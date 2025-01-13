using Test;

class Program
{
    static void Main(string[] args)
    {
        // MySQL server connection string (replace with your server info)
        string server = "localhost";
        string userID = "Admin";
        string password = "gs@123";
        string csvFilePath = @"C:\Users\yash.k\Downloads\R&D\ASR.csv";

        // Display available choices for the user to select
        Console.WriteLine("Enter Choice : ");
        Console.WriteLine("1. Create Databases");
        Console.WriteLine("2. Drop Databases");
        Console.WriteLine("3. Insert Data");

        // Get the user's choice
        int choice = Convert.ToInt32(Console.ReadLine());

        // Switch case to execute the corresponding demo based on the user's choice
        switch (choice)
        {
            // Case 1: 
            case 1:
                Console.WriteLine("Enter Number Of Database  :");
                int num1 = Convert.ToInt32(Console.ReadLine());
                CreateDatabase.CreateDatabases(num1, Query.createTableQuery, server, userID, password);
                break;

            // Case 2: 
            case 2:
                Console.WriteLine("Enter Number Of Database  :");
                int num2 = Convert.ToInt32(Console.ReadLine());
                DropDatabase.DropDatabases(num2, server, userID, password);
                break;

            // Case 3: 
            case 3:
                Console.WriteLine("Enter Number Of Database  :");
                int num3 = Convert.ToInt32(Console.ReadLine());
                InsertData.InsertDataFromCsv(num3, csvFilePath, server, userID, password);
                break;

            // Default case: If the user enters an invalid choice
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}
