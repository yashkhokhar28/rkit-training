using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanceApp
{
    /// <summary>
    /// Demonstrates the usage of Lambda Expressions with LINQ in C#.
    /// </summary>
    public class LambdaExpressionDemo
    {
        /// <summary>
        /// Runs a demonstration of Lambda Expressions for squaring and cubing numbers in a list.
        /// </summary>
        public void RunLambdaExpressionDemo()
        {
            // Initialize a list of integers
            List<int> lstNumbers = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7
            };

            // Display the original list
            Console.Write("The list : ");
            foreach (int number in lstNumbers)
            {
                Console.Write("{0} ", number);
            }
            Console.WriteLine("\n");

            // Apply a Lambda Expression to square each number in the list
            IEnumerable<int> square = lstNumbers.Select(x => x * x);

            // Display the squared numbers
            Console.Write("The squared list : ");
            foreach (int number in square)
            {
                Console.Write("{0} ", number);
            }
            Console.WriteLine("\n");

            // Apply a Lambda Expression to cube each number in the list
            IEnumerable<int> cubes = lstNumbers.Select((x) =>
            {
                // Calculate and return the cube of the number
                return x * x * x;
            });

            // Display the cubed numbers
            Console.Write("The cubed list : ");
            foreach (int number in cubes)
            {
                Console.Write("{0} ", number);
            }
            Console.WriteLine("\n");
        }
    }
}