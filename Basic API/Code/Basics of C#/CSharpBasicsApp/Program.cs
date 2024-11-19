using System;
using System.Threading.Tasks;
using CSharpBasicsApp;

class Program
{
    /// <summary>
    /// The entry point for the program that displays a menu for the user to select 
    /// different demos and executes the corresponding demo based on the user's choice.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static async Task Main()
    {
        // Display the menu options for the user
        Console.WriteLine("Hello, World!");
        Console.WriteLine("Enter Choice : ");
        Console.WriteLine("1. DataTypesDemo");
        Console.WriteLine("2. OperatorsDemo");
        Console.WriteLine("3. StatementsDemo");
        Console.WriteLine("4. ArraysDemo");
        Console.WriteLine("5. MethodsDemo");
        Console.WriteLine("6. OOPS Concepts");
        Console.WriteLine("7. ScopeAndAccessModifierDemo");
        Console.WriteLine("8. Assemblies and Namespaces");
        Console.WriteLine("9. CollectionsDemo");
        Console.WriteLine("10. EnumerationsDemo");
        Console.WriteLine("11. DatatableDemo");
        Console.WriteLine("12. ExceptionHandlingDemo");
        Console.WriteLine("13. StringClassDemo");
        Console.WriteLine("14. DateTimeClassDemo");
        Console.WriteLine("15. FileOperationDemo");

        // Read and convert the user's choice from the console input
        int choice = Convert.ToInt32(Console.ReadLine());

        // Execute the corresponding demo based on the user's choice using a switch statement
        switch (choice)
        {
            case 1:
                // Run DataTypesDemo when the user selects option 1
                DataTypesDemo.RunDataTypesDemo();
                break;

            case 2:
                // Run OperatorsDemo when the user selects option 2
                OperatorsDemo.RunOperatorsDemo();
                break;

            case 3:
                // Run StatementsDemo when the user selects option 3
                StatementsDemo.RunStatementsDemo();
                break;

            case 4:
                // Run ArraysDemo when the user selects option 4
                ArraysDemo.RunArraysDemo();
                break;

            case 5:
                // Run MethodsDemo when the user selects option 5
                MethodsDemo.RunMethodsDemo();
                break;

            case 6:
                // Run OOPSDemo when the user selects option 6
                OOPSDemo.RunOOPSDemo();
                break;

            case 7:
                // Run ScopeAndAccessModifierDemo when the user selects option 7
                ScopeAndAccessModifierDemo.RunScopeAndAccessModifierDemo();
                break;

            case 8:
                // Run AssembliesReferencesDemo when the user selects option 8
                // The 'await' keyword is used since this method is asynchronous
                await AssembliesReferencesDemo.RunAssembliesReferencesDemo();
                break;

            case 9:
                // Run CollectionClassDemo when the user selects option 9
                CollectionClassDemo.RunCollectionClassDemo();
                break;

            case 10:
                // Run EnumerationsDemo when the user selects option 10
                EnumerationsDemo.RunEnumerationsDemo();
                break;

            case 11:
                // Run DataTableDemo when the user selects option 11
                DataTableDemo.RunDataTableDemo();
                break;

            case 12:
                // Run ExceptionHandlingDemo when the user selects option 12
                ExceptionHandlingDemo.RunExceptionHandlingDemo();
                break;

            case 13:
                // Run StringClassDemo when the user selects option 13
                StringClassDemo.RunStringClassDemo();
                break;

            case 14:
                // Run DateTimeClassDemo when the user selects option 14
                DateTimeClassDemo.RunDateTimeClassDemo();
                break;

            case 15:
                // Run FileOperationDemo when the user selects option 15
                FileOperationDemo.RunFileOperationDemo();
                break;

            default:
                // Display an error message if the user selects an invalid option
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}