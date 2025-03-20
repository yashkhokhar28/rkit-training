namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates various string manipulation techniques in C#,
/// including concatenation, interpolation, comparison, case conversion, and more.
/// </summary>
public class StringClassDemo
{
    /// <summary>
    /// Executes a series of string manipulation examples, showcasing string operations like 
    /// concatenation, interpolation, comparison, case conversion, substring extraction, 
    /// and more.
    /// </summary>
    public static void RunStringClassDemo()
    {
        // Example 1: Concatenating strings
        string firstName = "John";
        string lastName = "Doe";
        // Concatenate first and last name with a space in between
        string fullName = string.Concat(firstName, " ", lastName);
        Console.WriteLine("Full Name: " + fullName);

        // Example 2: String Interpolation
        // Interpolate strings to include variables in the output
        string greeting = $"Hello, {firstName} {lastName}!";
        Console.WriteLine(greeting);

        // Example 3: String Comparison
        string str1 = "apple";
        string str2 = "Apple";
        // Compare strings ignoring case sensitivity
        bool areEqual = string.Equals(str1, str2);
        Console.WriteLine($"Are strings equal (ignoring case)? {areEqual}");

        // Example 4: Changing case (ToUpper / ToLower)
        string lowerCaseString = "hello world";
        // Convert the string to uppercase
        string upperCaseString = lowerCaseString.ToUpper();
        Console.WriteLine("Uppercase: " + upperCaseString);

        // Example 5: Substring Extraction
        string sentence = "Hello, welcome to C# programming!";
        // Extract a substring starting from index 7 with length 7 (i.e., "welcome")
        string subString = sentence.Substring(7, 7);
        Console.WriteLine("Substring: " + subString);

        // Example 6: Checking if a string contains a substring
        // Check if the string contains the word "welcome"
        bool containsWelcome = sentence.Contains("welcome");
        Console.WriteLine($"Contains 'welcome': {containsWelcome}");

        // Example 7: Trimming whitespace
        string messyString = "   Hello World!   ";
        // Trim leading and trailing whitespace from the string
        string trimmedString = messyString.Trim();
        Console.WriteLine("Trimmed: " + trimmedString);

        // Example 8: String Replacement
        string sentenceWithReplacement = sentence.Replace("C#", "CSharp");
        // Replace occurrences of "C#" with "CSharp" in the string
        Console.WriteLine("Replaced: " + sentenceWithReplacement);

        // Example 9: Splitting a string
        string csv = "apple,banana,cherry";
        // Split the string by commas into an array
        string[] fruits = csv.Split(',');
        Console.WriteLine("Fruits:");
        // Loop through the array and print each fruit
        foreach (var fruit in fruits)
        {
            Console.WriteLine(fruit);
        }

        // Example 10: String Length
        // Output the length of the sentence string
        Console.WriteLine("Length of the sentence: " + sentence.Length);
    }
}