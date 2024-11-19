namespace CSharpBasicsApp;

public class OperatorsDemo
{
    /// <summary>
    /// This method demonstrates the use of various operators in C# including Arithmetic, Relational, and Logical operators.
    /// </summary>
    /// <remarks>
    /// This method showcases different types of operators and their output using two integer values.
    /// </remarks>
    public static void RunOperatorsDemo()
    {
        #region Arithmetic Operators

        // Declare two integer variables for arithmetic operations
        int a = 10;
        int b = 20;

        // Perform addition and display result
        Console.WriteLine("a + b = " + (a + b)); // a + b = 30

        // Perform subtraction and display result
        Console.WriteLine("a - b = " + (a - b)); // a - b = -10

        // Perform multiplication and display result
        Console.WriteLine("a * b = " + (a * b)); // a * b = 200

        // Perform division and display result (integer division)
        Console.WriteLine("a / b = " + (a / b)); // a / b = 0

        // Perform modulus and display result (remainder of division)
        Console.WriteLine("a % b = " + (a % b)); // a % b = 10

        #endregion

        #region Relational Operators

        // Perform equality check and display result
        Console.WriteLine("a == b : " + (a == b)); // a == b : False

        // Perform inequality check and display result
        Console.WriteLine("a != b : " + (a != b)); // a != b : True

        // Perform greater than check and display result
        Console.WriteLine("a > b : " + (a > b)); // a > b : False

        // Perform less than check and display result
        Console.WriteLine("a < b : " + (a < b)); // a < b : True

        // Perform greater than or equal to check and display result
        Console.WriteLine("a >= b : " + (a >= b)); // a >= b : False

        // Perform less than or equal to check and display result
        Console.WriteLine("a <= b : " + (a <= b)); // a <= b : True

        #endregion

        #region Logical Operators

        // Declare two boolean variables for logical operations
        bool x = true;
        bool y = false;

        // Perform logical AND and display result
        Console.WriteLine("x && y : " + (x && y)); // x && y : False

        // Perform logical OR and display result
        Console.WriteLine("x || y : " + (x || y)); // x || y : True

        // Perform logical NOT and display result for x
        Console.WriteLine("!x : " + (!x)); // !x : False

        #endregion
    }
}