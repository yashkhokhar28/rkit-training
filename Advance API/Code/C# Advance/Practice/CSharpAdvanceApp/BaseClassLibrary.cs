using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomListLibrary;

namespace CSharpAdvanceApp
{
    /// <summary>
    /// The BaseClassLibrary class demonstrates the usage of CustomList from the CustomListLibrary.
    /// </summary>
    public class BaseClassLibrary
    {
        /// <summary>
        /// This method demonstrates how to use the CustomList class by performing operations such as removing duplicates 
        /// and finding the index of an item based on a condition.
        /// </summary>
        public void RunBaseClassLibrary()
        {
            // Initialize CustomList with some numbers, including duplicates
            CustomList<int> lstNumbers = new CustomList<int> { 1, 2, 2, 3, 4, 4, 5 };

            // Print the list before removing duplicates
            Console.WriteLine("List before removing duplicates:");
            foreach (var item in lstNumbers)
            {
                Console.WriteLine(item); // Display each item in the list
            }

            // Removing duplicates using the RemoveDuplicates method from CustomList
            lstNumbers.RemoveDuplicates();

            // Print the list after removing duplicates
            Console.WriteLine("List after removing duplicates:");
            foreach (var item in lstNumbers)
            {
                Console.WriteLine(item); // Display each item in the list after duplicates are removed
            }

            // Example of finding the index of the first item greater than 3 using the FindIndex method
            int index = lstNumbers.FindIndex(x => x > 3);
            // Display the index of the first element greater than 3
            Console.WriteLine($"Index of first item greater than 3: {index}");
        }
    }
}