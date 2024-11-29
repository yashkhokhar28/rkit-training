namespace CSharpBasicsApp;

public class StatementsDemo
{
    /// <summary>
    /// This method demonstrates various control flow statements in C#.
    /// </summary>
    public static void RunStatementsDemo()
    {
        #region If Statements

        int i = 10;
        if (i > 0)
        {
            Console.WriteLine("i is positive");
        }

        #endregion

        #region If-Else Statement

        if (i % 2 == 0)
        {
            Console.WriteLine("i is even");
        }
        else
        {
            Console.WriteLine("i is odd");
        }

        #endregion

        #region If-Else-If Statement

        if (i > 0)
        {
            Console.WriteLine("i is positive");
        }
        else if (i < 0)
        {
            Console.WriteLine("i is negative");
        }
        else
        {
            Console.WriteLine("i is zero");
        }

        #endregion

        #region Switch Statement

        switch (i)
        {
            case 1:
                Console.WriteLine("i is 1");
                break;
            case 2:
                Console.WriteLine("i is 2");
                break;
            default:
                Console.WriteLine("i is neither 1 nor 2");
                break;
        }

        #endregion

        #region Loops

        int j = 0;
        // While loop: prints j from 0 to 4
        while (j < 5)
        {
            Console.WriteLine("j = " + j);
            j++;
        }

        // Do-while loop: prints k from 0 to 4
        int k = 0;
        do
        {
            Console.WriteLine("k = " + k);
            k++;
        } while (k < 5);

        // For loop: prints l from 0 to 4
        for (int l = 0; l < 5; l++)
        {
            Console.WriteLine("l = " + l);
        }

        // Foreach loop: prints each element in the array arr
        int[] arr = { 10, 20, 30, 40, 50 };
        foreach (int m in arr)
        {
            Console.WriteLine("m = " + m);
        }

        #endregion

        #region Control Flow Statements

        // Break statement: exits the loop when n is 3
        for (int n = 0; n < 5; n++)
        {
            if (n == 3)
            {
                break;
            }

            Console.WriteLine("n = " + n);
        }

        // Continue statement: skips the iteration when o is 3
        for (int o = 0; o < 5; o++)
        {
            if (o == 3)
            {
                continue;
            }

            Console.WriteLine("o = " + o);
        }

        Console.WriteLine("Sum of 10 and 20 is " + Add(10, 20));

        #endregion

        #region Goto Statement

        goto MyLabel; // Jump to MyLabel label
        Console.WriteLine("This statement will not be executed"); // This will be skipped
        MyLabel: // Label to jump to
        Console.WriteLine("This statement will be executed"); // Output: This statement will be executed

        #endregion
    }

    /// <summary>
    /// This method adds two integers and returns the result.
    /// </summary>
    /// <param name="a">The first integer to add.</param>
    /// <param name="b">The second integer to add.</param>
    /// <returns>The sum of the two integers.</returns>
    static int Add(int a, int b)
    {
        return a + b;
    }
}