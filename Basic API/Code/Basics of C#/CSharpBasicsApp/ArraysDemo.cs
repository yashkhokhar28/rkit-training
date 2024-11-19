namespace CSharpBasicsApp;

public class ArraysDemo
{
    /// <summary>
    /// This method demonstrates various types of arrays in C# including single-dimensional,
    /// multidimensional, jagged arrays, and arrays of different data types.
    /// </summary>
    /// <remarks>
    /// The method showcases how to declare, initialize, and access elements of different types of arrays:
    /// - Single-dimensional arrays
    /// - Multi-dimensional arrays
    /// - Jagged arrays
    /// - Arrays of objects, strings, characters, and booleans
    /// </remarks>
    public static void RunArraysDemo()
    {
        #region Single-Dimensional Array

        // Single-dimensional array initialization
        int[] numbers = new int[5];
        numbers[0] = 10;
        numbers[1] = 20;
        numbers[2] = 30;
        numbers[3] = 40;
        numbers[4] = 50;

        // Display the values of the single-dimensional array
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine("numbers[" + i + "] = " + numbers[i]);
        }

        #endregion

        #region Multi-Dimensional Array

        // Multi-dimensional array initialization
        int[,] matrix = new int[2, 2];
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[1, 0] = 3;
        matrix[1, 1] = 4;

        // Display the values of the multi-dimensional array
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Console.WriteLine("matrix[" + i + "," + j + "] = " + matrix[i, j]);
            }
        }

        #endregion

        #region Jagged Array

        // Jagged array initialization
        int[][] jagged = new int[2][];
        jagged[0] = new int[2] { 1, 2 };
        jagged[1] = new int[3] { 3, 4, 5 };

        // Display the values of the jagged array
        for (int i = 0; i < jagged.Length; i++)
        {
            for (int j = 0; j < jagged[i].Length; j++)
            {
                Console.WriteLine("jagged[" + i + "][" + j + "] = " + jagged[i][j]);
            }
        }

        #endregion

        #region Array of Objects

        // Array of objects initialization
        object[] objects = new object[3];
        objects[0] = 10;
        objects[1] = "Hello, World!";
        objects[2] = true;

        // Display the values of the array of objects
        for (int i = 0; i < objects.Length; i++)
        {
            Console.WriteLine("objects[" + i + "] = " + objects[i]);
        }

        #endregion

        #region Array of Strings

        // Array of strings initialization
        string[] names = new string[3];
        names[0] = "Alice";
        names[1] = "Bob";
        names[2] = "Charlie";

        // Display the values of the array of strings
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine("names[" + i + "] = " + names[i]);
        }

        #endregion

        #region Array of Characters

        // Array of characters initialization
        char[] characters = new char[3];
        characters[0] = 'A';
        characters[1] = 'B';
        characters[2] = 'C';

        // Display the values of the array of characters
        for (int i = 0; i < characters.Length; i++)
        {
            Console.WriteLine("characters[" + i + "] = " + characters[i]);
        }

        #endregion

        #region Array of Booleans

        // Array of booleans initialization
        bool[] flags = new bool[3];
        flags[0] = true;
        flags[1] = false;
        flags[2] = true;

        // Display the values of the array of booleans
        for (int i = 0; i < flags.Length; i++)
        {
            Console.WriteLine("flags[" + i + "] = " + flags[i]);
        }

        #endregion
    }
}