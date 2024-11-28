namespace CSharpBasicsApp;

public class DataTypesDemo
{
    /// <summary>
    /// This method demonstrates the use of value types and reference types in C#.
    /// </summary>
    public static void RunDataTypesDemo()
    {
        #region Value Types

        int i = 10;

        double d = 10.5;

        char c = 'A';

        bool isTrue = true;

        #endregion

        #region Reference Types

        string s = "Hello, World!";

        object o = new object();

        #endregion

        #region Display Values

        Console.WriteLine("i = " + i);

        Console.WriteLine("d = " + d);

        Console.WriteLine("c = " + c);

        Console.WriteLine("isTrue = " + isTrue);

        Console.WriteLine("s = " + s);

        Console.WriteLine("o = " + o);

        #endregion
    }
}