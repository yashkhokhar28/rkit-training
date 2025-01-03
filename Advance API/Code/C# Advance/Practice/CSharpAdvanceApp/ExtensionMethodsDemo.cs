using Newtonsoft.Json;
using System;

namespace CSharpAdvanceApp
{
    /// <summary>
    /// Demonstrates the usage of extension methods for various data types.
    /// </summary>
    public class ExtensionMethodsDemo
    {
        /// <summary>
        /// Executes the demo for extension methods.
        /// </summary>
        public void RunExtensionMethodsDemo()
        {
            // String extension method example
            string sentence = "Hello, how are you doing today?";
            int count = sentence.WordCount();
            Console.WriteLine($"The sentence contains {count} words.");

            // Integer extension methods example
            int number = 10;

            // Check if the number is even
            bool isEvenNumber = number.IsEven();
            if (isEvenNumber)
            {
                Console.WriteLine($"Number {number} is an even number.");
            }
            else
            {
                Console.WriteLine($"Number {number} is an odd number.");
            }

            // Calculate the factorial of the number
            int factorial = number.Factorial();
            Console.WriteLine($"Factorial of number {number} is {factorial}.");

            // String to JSON extension method example
            string strValue = "Hello World";
            Console.WriteLine($"Serialized string: {strValue.ToJson()}");

            // DateTime extension method example
            DateTime todaysDate = DateTime.Now;
            Console.WriteLine($"Next business day: {todaysDate.NextBusinessDay():yyyy-MM-dd}");
        }
    }

    /// <summary>
    /// Extension methods for string data type.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Counts the number of words in a string.
        /// </summary>
        /// <param name="str">The input string.</param>
        /// <returns>The number of words in the string.</returns>
        public static int WordCount(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;

            // Split the string using delimiters and count non-empty entries
            return str.Split(new char[] { ' ', '.', '?', '!', ',' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }

    /// <summary>
    /// Extension methods for integer data type.
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// Checks if the integer is even.
        /// </summary>
        /// <param name="number">The input integer.</param>
        /// <returns>True if the number is even, false otherwise.</returns>
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }

        /// <summary>
        /// Calculates the factorial of an integer.
        /// </summary>
        /// <param name="number">The input integer.</param>
        /// <returns>The factorial of the number.</returns>
        /// <exception cref="ArgumentException">Thrown when the number is negative.</exception>
        public static int Factorial(this int number)
        {
            if (number < 0) throw new ArgumentException("Number must be non-negative.");
            if (number == 0) return 1;

            int result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }
    }

    /// <summary>
    /// Extension methods for object data type.
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Serializes an object to its JSON representation.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <returns>The JSON string representation of the object.</returns>
        public static string ToJson(this object obj)
        {
            if (obj == null)
                return "null"; // Handle null case gracefully
            return JsonConvert.SerializeObject(obj, Formatting.Indented); // Indented for readability
        }
    }

    /// <summary>
    /// Extension methods for DateTime data type.
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Calculates the next business day (skips weekends).
        /// </summary>
        /// <param name="date">The input date.</param>
        /// <returns>The next business day.</returns>
        public static DateTime NextBusinessDay(this DateTime date)
        {
            do
            {
                date = date.AddDays(1); // Increment the date by one day
            } while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
            return date;
        }
    }
}