using CSharpBasicsApp;

public class ArraysDemo
{
    /// <summary>
    /// This method demonstrates various types of arrays in C# including single-dimensional,
    /// multidimensional, arrJagged arrays, and arrays of different data types.
    /// </summary>
    public static void RunArraysDemo()
    {
        #region Single-Dimensional Array

        int[] arrNumbers = new int[5];
        arrNumbers[0] = 10;
        arrNumbers[1] = 20;
        arrNumbers[2] = 30;
        arrNumbers[3] = 40;
        arrNumbers[4] = 50;

        for (int i = 0; i < arrNumbers.Length; i++)
        {
            Console.WriteLine("arrNumbers[" + i + "] = " + arrNumbers[i]);
        }

        #endregion

        #region Multi-Dimensional Array

        int[,] arrMatrix = new int[2, 2];
        arrMatrix[0, 0] = 1;
        arrMatrix[0, 1] = 2;
        arrMatrix[1, 0] = 3;
        arrMatrix[1, 1] = 4;

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Console.WriteLine("arrMatrix[" + i + "," + j + "] = " + arrMatrix[i, j]);
            }
        }

        #endregion

        #region Jagged Array

        int[][] arrJagged = new int[2][];
        arrJagged[0] = new int[2] { 1, 2 };
        arrJagged[1] = new int[3] { 3, 4, 5 };

        for (int i = 0; i < arrJagged.Length; i++)
        {
            for (int j = 0; j < arrJagged[i].Length; j++)
            {
                Console.WriteLine("arrJagged[" + i + "][" + j + "] = " + arrJagged[i][j]);
            }
        }

        #endregion

        #region Array of Objects

        object[] arrObjects = new object[3];
        arrObjects[0] = 10;
        arrObjects[1] = "Hello, World!";
        arrObjects[2] = true;

        for (int i = 0; i < arrObjects.Length; i++)
        {
            Console.WriteLine("arrObjects[" + i + "] = " + arrObjects[i]);
        }

        #endregion

        #region Array of Strings

        string[] arrNames = new string[3];
        arrNames[0] = "Alice";
        arrNames[1] = "Bob";
        arrNames[2] = "Charlie";

        for (int i = 0; i < arrNames.Length; i++)
        {
            Console.WriteLine("arrNames[" + i + "] = " + arrNames[i]);
        }

        #endregion

    }
}