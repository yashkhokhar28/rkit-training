namespace CSharpBasicsApp;

public class ArraysDemo
{
    /// <summary>
    /// This method demonstrates various types of arrays in C# including single-dimensional,
    /// multidimensional, jagged arrays, and arrays of different data types.
    /// </summary>
    public static void RunArraysDemo()
    {
        #region Single-Dimensional Array

        int[] numbers = new int[5];
        numbers[0] = 10;
        numbers[1] = 20;
        numbers[2] = 30;
        numbers[3] = 40;
        numbers[4] = 50;

        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine("numbers[" + i + "] = " + numbers[i]);
        }

        #endregion

        #region Multi-Dimensional Array

        int[,] matrix = new int[2, 2];
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[1, 0] = 3;
        matrix[1, 1] = 4;

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Console.WriteLine("matrix[" + i + "," + j + "] = " + matrix[i, j]);
            }
        }

        #endregion

        #region Jagged Array

        int[][] jagged = new int[2][];
        jagged[0] = new int[2] { 1, 2 };
        jagged[1] = new int[3] { 3, 4, 5 };

        for (int i = 0; i < jagged.Length; i++)
        {
            for (int j = 0; j < jagged[i].Length; j++)
            {
                Console.WriteLine("jagged[" + i + "][" + j + "] = " + jagged[i][j]);
            }
        }

        #endregion

        #region Array of Objects

        object[] objects = new object[3];
        objects[0] = 10;
        objects[1] = "Hello, World!";
        objects[2] = true;

        for (int i = 0; i < objects.Length; i++)
        {
            Console.WriteLine("objects[" + i + "] = " + objects[i]);
        }

        #endregion

        #region Array of Strings

        string[] names = new string[3];
        names[0] = "Alice";
        names[1] = "Bob";
        names[2] = "Charlie";

        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine("names[" + i + "] = " + names[i]);
        }

        #endregion

    }
}