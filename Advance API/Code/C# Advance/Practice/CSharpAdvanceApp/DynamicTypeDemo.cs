using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSharpAdvanceApp
{
    /// <summary>
    /// Demonstrates the use of dynamic types, JSON deserialization, COM Interop, and Reflection in C#.
    /// </summary>
    public class DynamicTypeDemo
    {
        /// <summary>
        /// Entry point to demonstrate dynamic type usage and runtime operations.
        /// </summary>
        public void RunDynamicTypeDemo()
        {
            // Dynamic variable holding an integer value.
            dynamic value = 5;
            Console.WriteLine(value + 10); // Output: 15

            // Dynamic variable holding a string value.
            value = "Hello";
            Console.WriteLine(value.ToUpper()); // Output: HELLO

            // Demonstrating JSON deserialization to a dynamic object.
            string jsonString = "{\"Name\":\"Alice\",\"Age\":25}";
            dynamic person = JsonConvert.DeserializeObject(jsonString);
            Console.WriteLine(person.Name); // Output: Alice
            Console.WriteLine(person.Age);  // Output: 25

            // Demonstrating COM Interop with Excel Application using dynamic.
            dynamic excel = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
            excel.Visible = true; // Makes the Excel application visible.
            excel.Workbooks.Add(); // Adds a new workbook.
            dynamic workbook = excel.Workbooks[1];
            dynamic sheet = workbook.Sheets[1];
            sheet.Cells[1, 1].Value = "Hiii"; // Sets value to a cell.

            // Using Reflection to retrieve and invoke methods from a type.
            Type type = typeof(DemoClass);
            MethodInfo[] arrMethods = type.GetMethods(); // Retrieve all methods in DemoClass.
            object instance = Activator.CreateInstance(type); // Create an instance of DemoClass.

            Console.WriteLine("Methods in DemoClass:");
            foreach (MethodInfo methodInfo in arrMethods)
            {
                Console.WriteLine(methodInfo.Name); // List method names.
            }

            // Invoking a specific method (Greet) dynamically using Reflection.
            MethodInfo objMethodInfo = type.GetMethod("Greet");
            objMethodInfo.Invoke(instance, new object[] { "Alice" }); // Output: Hello, Alice!
        }
    }

    /// <summary>
    /// Sample class with methods for Reflection demonstration.
    /// </summary>
    public class DemoClass
    {
        /// <summary>
        /// Prints a static message to the console.
        /// </summary>
        public void PrintMessage() => Console.WriteLine("Hello, World!");

        /// <summary>
        /// Adds two integer numbers and returns the result.
        /// </summary>
        /// <param name="a">First integer.</param>
        /// <param name="b">Second integer.</param>
        /// <returns>Sum of the two integers.</returns>
        public int Add(int a, int b) => a + b;

        /// <summary>
        /// Greets a user by name.
        /// </summary>
        /// <param name="name">Name of the user to greet.</param>
        public void Greet(string name) => Console.WriteLine($"Hello, {name}!");
    }
}