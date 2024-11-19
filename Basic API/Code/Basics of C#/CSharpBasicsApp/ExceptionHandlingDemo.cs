using System;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates various exception handling techniques in C#, including basic exception handling,
/// custom exceptions, exception filters, and logging.
/// </summary>
public class ExceptionHandlingDemo
{
    /// <summary>
    /// Executes different exception handling scenarios, demonstrating basic try-catch-finally,
    /// custom exceptions, exception properties, and other advanced features like inner exceptions,
    /// filters, and logging.
    /// </summary>
    public static void RunExceptionHandlingDemo()
    {
        // Basic Exception Handling with Finally Block
        try
        {
            int a = 10;
            int b = 0;
            int result = a / b; // This will cause DivideByZeroException
            Console.WriteLine("Result: " + result);
        }
        catch (DivideByZeroException ex)
        {
            // Handle DivideByZeroException and print the error message
            Console.WriteLine("Exception: " + ex.Message);
        }
        finally
        {
            // The finally block will execute regardless of whether an exception occurred or not
            Console.WriteLine("Finally block executed.");
        }

        // Custom Exception Demonstration
        try
        {
            // Throwing a custom exception
            throw new CustomException("This is a custom exception message.");
        }
        catch (CustomException ex)
        {
            // Handle custom exceptions and print the message
            Console.WriteLine("Custom Exception: " + ex.Message);
        }

        // Exception Properties Example
        try
        {
            int[] numbers = { 1, 2, 3 };
            Console.WriteLine(numbers[5]); // This will cause IndexOutOfRangeException
        }
        catch (IndexOutOfRangeException ex)
        {
            // Handle IndexOutOfRangeException and print additional exception properties
            Console.WriteLine("Exception: " + ex.Message);
            Console.WriteLine("Source: " + ex.Source);
            Console.WriteLine("StackTrace: " + ex.StackTrace);
            Console.WriteLine("TargetSite: " + ex.TargetSite);
        }

        // Inner Exception Demonstration
        try
        {
            try
            {
                int a = 10;
                int b = 0;
                int result = a / b; // This will cause DivideByZeroException
                Console.WriteLine("Result: " + result);
            }
            catch (DivideByZeroException ex)
            {
                // Rethrow the exception as an inner exception in a new exception
                throw new Exception("An error occurred in the inner block.", ex);
            }
        }
        catch (Exception ex)
        {
            // Catch the outer exception and display inner exception details
            Console.WriteLine("Outer Exception: " + ex.Message);
            Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
        }

        // Exception Filters Example
        try
        {
            int a = 10;
            int b = 0;
            int result = a / b; // This will cause DivideByZeroException
            Console.WriteLine("Result: " + result);
        }
        catch (DivideByZeroException ex) when (ex.Message.Contains("zero"))
        {
            // Handle the exception only if the message contains the word "zero"
            Console.WriteLine("Filtered Divide by Zero Exception: " + ex.Message);
        }

        // Multiple Catch Blocks Example
        try
        {
            int[] numbers = { 1, 2, 3 };
            Console.WriteLine(numbers[5]); // IndexOutOfRangeException
        }
        catch (IndexOutOfRangeException ex)
        {
            // Handle IndexOutOfRangeException specifically
            Console.WriteLine("Index out of range exception: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Catch any other general exceptions
            Console.WriteLine("General Exception: " + ex.Message);
        }
    }

    /// <summary>
    /// Custom Exception class to demonstrate throwing and handling user-defined exceptions.
    /// </summary>
    private class CustomException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the CustomException class with a specified message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CustomException(string message) : base(message)
        {
        }
    }
}