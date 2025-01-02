using CSharpAdvanceApp;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter Choice : ");
        Console.WriteLine("1. Simple Class");
        Console.WriteLine("2. Abstract Class");
        Console.WriteLine("3. Partial Class");
        Console.WriteLine("4. Sealed Class");
        Console.WriteLine("5. Static Class");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                ClassDemo objClassDemo = new ClassDemo();
                objClassDemo.RunClassDemo();
                break;

            case 2:
                GenericClassDemo objGenericClassDemo = new GenericClassDemo();
                objGenericClassDemo.RunGenericClassDemo();
                break;

            case 3:
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                break;

            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}