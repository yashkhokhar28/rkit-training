using System;

namespace CSharpBasicsApp;

public class MethodsDemo
{
    /// <summary>
    /// Demonstrates various types of methods in C#, including methods with parameters, return values,
    /// 'out' and 'ref' parameters, default parameters, named parameters, and methods with variable arguments.
    /// </summary>
    public static void RunMethodsDemo()
    {
        // Call a method with no parameters
        DisplayMessage();

        // Call a method with a single parameter
        DisplayMessage("Welcome to C# programming!");

        // Call a method with a return value
        int sum = Add(10, 20);
        Console.WriteLine("Sum = " + sum);

        // Call a method using an 'out' parameter
        int quotient;
        Divide(10, 3, out quotient);
        Console.WriteLine("Quotient = " + quotient);

        // Call a method with a 'ref' parameter
        int number = 10;
        Increment(ref number);
        Console.WriteLine("Number after increment = " + number);

        // Call a method with a default parameter
        DisplayMessage();

        // Call a method using named parameters
        DisplayMessage(message: "Using named parameters!");

        // Call a method with a variable number of arguments
        DisplayMessages("This", "is", "a", "demo", "of", "params");
    }

    /// <summary>
    /// Displays a message. If no message is provided, a default message is shown.
    /// </summary>
    /// <param name="message">The message to display. Defaults to "Hello, default message!".</param>
    static void DisplayMessage(string message = "Hello, default message!")
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Adds two integers and returns the result.
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer.</param>
    /// <returns>The sum of the two integers.</returns>
    static int Add(int a, int b)
    {
        return a + b;
    }

    /// <summary>
    /// Divides the dividend by the divisor and outputs the quotient using an 'out' parameter.
    /// </summary>
    /// <param name="dividend">The dividend.</param>
    /// <param name="divisor">The divisor.</param>
    /// <param name="quotient">The quotient, returned as an 'out' parameter.</param>
    static void Divide(int dividend, int divisor, out int quotient)
    {
        quotient = dividend / divisor;
    }

    /// <summary>
    /// Increments the provided number by one using a 'ref' parameter.
    /// </summary>
    /// <param name="number">The number to increment, passed by reference.</param>
    static void Increment(ref int number)
    {
        number++;
    }

    /// <summary>
    /// Displays multiple messages, accepting a variable number of string arguments.
    /// </summary>
    /// <param name="messages">An array of messages to display.</param>
    static void DisplayMessages(params string[] messages)
    {
        foreach (string message in messages)
        {
            Console.WriteLine(message);
        }
    }
}