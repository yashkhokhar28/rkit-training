using CSharpAdvanceApp;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter Choice : ");
        Console.WriteLine("1. Class Demo");
        Console.WriteLine("2. Generics Demo");
        Console.WriteLine("3. Dynamic Type Demo");
        Console.WriteLine("4. File System Demo");

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
                DynamicTypeDemo objDynamicTypeDemo = new DynamicTypeDemo();
                objDynamicTypeDemo.RunDynamicTypeDemo();
                break;

            case 4:
                FileSystemDemo objFileSystemDemo = new FileSystemDemo();
                objFileSystemDemo.RunFileSystemDemo();
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