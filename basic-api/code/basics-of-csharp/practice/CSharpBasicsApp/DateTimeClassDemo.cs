namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates various functionalities of the DateTime class in C#.
/// </summary>
public class DateTimeClassDemo
{
    /// <summary>
    /// Executes the DateTime operations demonstration.
    /// </summary>
    public static void RunDateTimeClassDemo()
    {
        // Get the current date and time
        DateTime currentDateTime = DateTime.Now;
        Console.WriteLine("Current Date and Time: " + currentDateTime);

        // Get the current date only (without the time)
        DateTime currentDate = DateTime.Today;
        Console.WriteLine("Current Date: " + currentDate);

        // Get the current time only (without the date)
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        Console.WriteLine("Current Time: " + currentTime);

        // Add 1 year to the current date
        DateTime futureDate = currentDateTime.AddYears(1);
        Console.WriteLine("1 Year Later: " + futureDate);

        // Add 3 days to the current date
        DateTime futureDate2 = currentDateTime.AddDays(3);
        Console.WriteLine("3 Days Later: " + futureDate2);

        // Format the date as yyyy-MM-dd HH:mm:ss
        string formattedDate = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        Console.WriteLine("Formatted Date: " + formattedDate);

        // Parse a string to DateTime
        string dateString = "2024-11-20";
        DateTime parsedDate = DateTime.Parse(dateString);
        Console.WriteLine("Parsed Date: " + parsedDate);

        // Compare two dates: current date vs. specific date
        DateTime someDate = new DateTime(2024, 11, 20);
        int comparisonResult = DateTime.Compare(currentDate, someDate);

        // Output the result of comparison
        if (comparisonResult < 0)
        {
            Console.WriteLine("Current date is earlier than " + someDate.ToString("yyyy-MM-dd"));
        }
        else if (comparisonResult == 0)
        {
            Console.WriteLine("Both dates are the same.");
        }
        else
        {
            Console.WriteLine("Current date is later than " + someDate.ToString("yyyy-MM-dd"));
        }

        // Get the day of the week for the current date
        DayOfWeek dayOfWeek = currentDateTime.DayOfWeek;
        Console.WriteLine("Today is: " + dayOfWeek);
    }
}