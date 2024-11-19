namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the use of enumerations in C#.
/// </summary>
public class EnumerationsDemo
{
    #region Public Methods

    /// <summary>
    /// Runs the enumeration demo showing how to define and use enumerations in C#.
    /// </summary>
    /// <remarks>
    /// Demonstrates defining an enum for days of the week and converting between enum values and integers.
    /// It also shows how to use an enum in a switch statement.
    /// </remarks>
    public static void RunEnumerationsDemo()
    {
        // Assign an enumeration value to a variable
        DaysOfWeek today = DaysOfWeek.Wednesday;
        Console.WriteLine("Today is: " + today);

        // Convert an enumeration value to its underlying integer value
        int dayNumber = (int)DaysOfWeek.Wednesday;
        Console.WriteLine("Wednesday is day number: " + dayNumber);

        // Convert an integer to an enumeration value
        DaysOfWeek dayFromNumber = (DaysOfWeek)5;
        Console.WriteLine("Day number 5 is: " + dayFromNumber);

        // Use an enum in a switch statement
        switch (today)
        {
            case DaysOfWeek.Sunday:
                Console.WriteLine("It's Sunday, time to relax!");
                break;
            case DaysOfWeek.Monday:
                Console.WriteLine("Back to work on Monday.");
                break;
            case DaysOfWeek.Wednesday:
                Console.WriteLine("It's hump day, Wednesday!");
                break;
            default:
                Console.WriteLine("Just another weekday.");
                break;
        }
    }

    #endregion

    #region Private Members

    // Define an enumeration named DaysOfWeek
    /// <summary>
    /// Enum representing the days of the week.
    /// </summary>
    public enum DaysOfWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    #endregion
}