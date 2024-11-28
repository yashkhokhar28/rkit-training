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
        Console.WriteLine("Hello, World!");
        Console.WriteLine("Enter Choice : ");
        Console.WriteLine("1. DataTypesDemo");
        Console.WriteLine("2. OperatorsDemo");
        Console.WriteLine("3. StatementsDemo");
        Console.WriteLine("4. ArraysDemo");
        Console.WriteLine("5. MethodsDemo");
        Console.WriteLine("6. InheritanceDemo");
        Console.WriteLine("7. PolymorphismDemo");
        Console.WriteLine("8. EncapsulationDemo");
        Console.WriteLine("9. AbstractionDemo");
        Console.WriteLine("10. ScopeAndAccessModifierDemo");
        Console.WriteLine("11. Assemblies and Namespaces");
        Console.WriteLine("12. CollectionsDemo");
        Console.WriteLine("13. EnumerationsDemo");
        Console.WriteLine("14. DatatableDemo");
        Console.WriteLine("15. ExceptionHandlingDemo");
        Console.WriteLine("16. StringClassDemo");
        Console.WriteLine("17. DateTimeClassDemo");
        Console.WriteLine("18. FileOperationDemo");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                DataTypesDemo.RunDataTypesDemo();
                break;

            case 2:
                OperatorsDemo.RunOperatorsDemo();
                break;

            case 3:
                StatementsDemo.RunStatementsDemo();
                break;

            case 4:
                ArraysDemo.RunArraysDemo();
                break;

            case 5:
                MethodsDemo.RunMethodsDemo();
                break;

            case 6:
                InheritanceDemo.RunInheritanceDemo();
                break;

            case 7:
                PolymorphismDemo.RunPolymorphismDemo();
                break;
            case 8:
                EncapsulationDemo.RunEncapsulationDemo();
                break;
            case 9:
                AbstractionDemo.RunAbstractionDemo();
                break;

            case 10:
                ScopeAndAccessModifierDemo.RunScopeAndAccessModifierDemo();
                break;

            case 11:
                await AssembliesReferencesDemo.RunAssembliesReferencesDemo();
                break;

            case 12:
                CollectionClassDemo.RunCollectionClassDemo();
                break;

            case 13:
                EnumerationsDemo.RunEnumerationsDemo();
                break;

            case 14:
                DataTableDemo.RunDataTableDemo();
                break;

            case 15:
                ExceptionHandlingDemo.RunExceptionHandlingDemo();
                break;

            case 16:
                StringClassDemo.RunStringClassDemo();
                break;

            case 17:
                DateTimeClassDemo.RunDateTimeClassDemo();
                break;

            case 18:
                FileOperationDemo.RunFileOperationDemo();
                break;

            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}