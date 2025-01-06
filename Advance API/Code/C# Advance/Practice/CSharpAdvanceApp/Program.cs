using CSharpAdvanceApp;

/// <summary>
/// This is the entry point of the application where different demo choices are provided to the user.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method that drives the program. It displays the demo choices and handles user input.
    /// Based on the input, it runs the respective demo classes.
    /// </summary>
    public static void Main()
    {
        // Display available choices for the user to select
        Console.WriteLine("Enter Choice : ");
        Console.WriteLine("1. Class Demo");
        Console.WriteLine("2. Generics Demo");
        Console.WriteLine("3. Dynamic Type Demo");
        Console.WriteLine("4. File System Demo");
        Console.WriteLine("5. Data Serialization Demo");
        Console.WriteLine("7. Lambda Expression Demo");
        Console.WriteLine("8. Extension Methods Demo");
        Console.WriteLine("9. Linq Demo");

        // Get the user's choice
        int choice = Convert.ToInt32(Console.ReadLine());

        // Switch case to execute the corresponding demo based on the user's choice
        switch (choice)
        {
            // Case 1: Run Class Demo
            case 1:
                ClassDemo objClassDemo = new ClassDemo();
                objClassDemo.RunClassDemo();
                break;

            // Case 2: Run Generics Demo
            case 2:
                GenericClassDemo objGenericClassDemo = new GenericClassDemo();
                objGenericClassDemo.RunGenericClassDemo();
                break;

            // Case 3: Run Dynamic Type Demo
            case 3:
                DynamicTypeDemo objDynamicTypeDemo = new DynamicTypeDemo();
                objDynamicTypeDemo.RunDynamicTypeDemo();
                break;

            // Case 4: Run File System Demo
            case 4:
                FileSystemDemo objFileSystemDemo = new FileSystemDemo();
                objFileSystemDemo.RunFileSystemDemo();
                break;

            // Case 5: Run Data Serialization Demo
            case 5:
                DataSerializationDemo objDataSerializationDemo = new DataSerializationDemo();
                objDataSerializationDemo.RunDataSerializationDemo();
                break;

            // Case 6:
            case 6:
                break;

            // Case 7:
            case 7:
                LambdaExpressionDemo objLambdaExpressionDemo = new LambdaExpressionDemo();
                objLambdaExpressionDemo.RunLambdaExpressionDemo();
                break;

            // Case 8:
            case 8:
                ExtensionMethodsDemo objExtensionMethodsDemo = new ExtensionMethodsDemo();
                objExtensionMethodsDemo.RunExtensionMethodsDemo();
                break;

            // Case 9:
            case 9:
                LinqDemo objLinqDemo = new LinqDemo();
                objLinqDemo.RunLinqDemo();
                break;

            // Default case: If the user enters an invalid choice
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}