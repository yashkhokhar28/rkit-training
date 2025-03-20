namespace CSharpBasicsApp;

public class OperatorsDemo
{
    /// <summary>
    /// This method demonstrates the use of various operators in C# including Arithmetic, Relational, and Logical operators.
    /// </summary>
    public static void RunOperatorsDemo()
    {
        #region Arithmetic Operators

        int a = 10;
        int b = 20;

        Console.WriteLine("a + b = " + (a + b)); // a + b = 30

        Console.WriteLine("a - b = " + (a - b)); // a - b = -10

        Console.WriteLine("a * b = " + (a * b)); // a * b = 200

        Console.WriteLine("a / b = " + (a / b)); // a / b = 0

        Console.WriteLine("a % b = " + (a % b)); // a % b = 10

        #endregion

        #region Relational Operators

        Console.WriteLine("a == b : " + (a == b)); // a == b : False

        Console.WriteLine("a != b : " + (a != b)); // a != b : True

        Console.WriteLine("a > b : " + (a > b)); // a > b : False

        Console.WriteLine("a < b : " + (a < b)); // a < b : True

        Console.WriteLine("a >= b : " + (a >= b)); // a >= b : False

        Console.WriteLine("a <= b : " + (a <= b)); // a <= b : True

        #endregion

        #region Logical Operators

        bool x = true;
        bool y = false;

        Console.WriteLine("x && y : " + (x && y)); // x && y : False

        Console.WriteLine("x || y : " + (x || y)); // x || y : True

        Console.WriteLine("!x : " + (!x)); // !x : False

        #endregion
    }
}