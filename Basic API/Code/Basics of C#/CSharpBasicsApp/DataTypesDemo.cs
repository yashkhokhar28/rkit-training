namespace CSharpBasicsApp;

public class DataTypesDemo
{
    /// <summary>
    /// This method demonstrates the use of value types and reference types in C#.
    /// </summary>
    /// <remarks>
    /// This method shows how value types (int, double, char, bool) and reference types (string, object) are declared and their values are displayed.
    /// </remarks>
    public static void RunDataTypesDemo()
    {
        #region Value Types

        // Declare a value type variable of type int and assign it a value
        int i = 10; // Integer type, holds the value 10

        // Declare a value type variable of type double and assign it a value
        double d = 10.5; // Double type, holds the value 10.5

        // Declare a value type variable of type char and assign it a value
        char c = 'A'; // Char type, holds a single character 'A'

        // Declare a value type variable of type bool and assign it a value
        bool isTrue = true; // Boolean type, holds the value true

        #endregion

        #region Reference Types

        // Declare a reference type variable of type string and assign it a value
        string s = "Hello, World!"; // String type, holds the value "Hello, World!"

        // Declare a reference type variable of type object and instantiate a new object
        object
            o = new object(); // Object type, a base type for all other types, here an instance of an object is created

        #endregion

        #region Display Values

        // Display the value of the integer variable
        Console.WriteLine("i = " + i); // Output: i = 10

        // Display the value of the double variable
        Console.WriteLine("d = " + d); // Output: d = 10.5

        // Display the value of the char variable
        Console.WriteLine("c = " + c); // Output: c = A

        // Display the value of the boolean variable
        Console.WriteLine("isTrue = " + isTrue); // Output: isTrue = True

        // Display the value of the string variable
        Console.WriteLine("s = " + s); // Output: s = Hello, World!

        // Display the value of the object variable (which will print the type of the object)
        Console.WriteLine("o = " + o); // Output: o = System.Object

        #endregion
    }
}