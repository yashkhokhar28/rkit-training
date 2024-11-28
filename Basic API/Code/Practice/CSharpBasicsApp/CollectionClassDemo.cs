using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates usage of various collection classes in C# with in-built methods.
/// </summary>
public class CollectionClassDemo
{
    #region Public Methods

    /// <summary>
    /// Runs the demonstration for various collection classes using in-built methods.
    /// </summary>
    public static void RunCollectionClassDemo()
    {
        // List<T>: Demonstrate adding, inserting, and removing elements
        var numbers = new List<int>();
        numbers.AddRange(new[] { 10, 20, 30, 40, 50 });
        numbers.Remove(20);
        numbers.Insert(1, 15);
        DisplayCollection(numbers, "List<T>");

        // Dictionary<TKey, TValue>: Demonstrate adding, removing, and checking keys
        var names = new Dictionary<int, string>();
        names.Add(1, "Alice");
        names.Add(2, "Bob");
        names[3] = "Charlie"; // Update or add
        if (names.ContainsKey(2)) names.Remove(2);
        foreach (var kvp in names)
        {
            Console.WriteLine($"names[{kvp.Key}] = {kvp.Value}");
        }

        // Queue<T>: Demonstrate enqueueing and dequeueing
        var queue = new Queue<int>();
        queue.Enqueue(10);
        queue.Enqueue(20);
        queue.Enqueue(30);
        queue.Dequeue(); // Removes the first element
        DisplayCollection(queue, "Queue<T>");

        // Stack<T>: Demonstrate pushing and popping elements
        var stack = new Stack<int>();
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);
        stack.Pop(); // Removes the top element
        DisplayCollection(stack, "Stack<T>");

        // LinkedList<T>: Demonstrate adding at the beginning and end
        var linkedList = new LinkedList<int>();
        linkedList.AddFirst(30);
        linkedList.AddLast(10);
        linkedList.AddAfter(linkedList.First, 20);
        DisplayCollection(linkedList, "LinkedList<T>");

        // HashSet<T>: Demonstrate adding and checking for uniqueness
        var hashSet = new HashSet<int> { 10, 20, 30 };
        hashSet.Add(20); // No duplicate added
        DisplayCollection(hashSet, "HashSet<T>");

        // SortedSet<T>: Demonstrate sorted addition of unique elements
        var sortedSet = new SortedSet<int>();
        sortedSet.Add(30);
        sortedSet.Add(10);
        sortedSet.Add(20);
        DisplayCollection(sortedSet, "SortedSet<T>");

        // BitArray: Demonstrate setting and toggling bits
        var bitArray = new BitArray(3, false);
        bitArray.Set(0, true);
        bitArray.Set(2, true);
        for (int i = 0; i < bitArray.Length; i++)
        {
            Console.WriteLine($"bitArray[{i}] = {bitArray[i]}");
        }

        // ArrayList: Demonstrate dynamic addition of mixed data types
        var arrayList = new ArrayList();
        arrayList.Add(10);
        arrayList.Add("Hello, World!");
        arrayList.Add(true);
        foreach (var item in arrayList)
        {
            Console.WriteLine("item = " + item);
        }

        // Hashtable: Demonstrate adding, updating, and iterating over key-value pairs
        var hashtable = new Hashtable();
        hashtable[1] = 10;
        hashtable[2] = "Hello, World!";
        hashtable[3] = true;
        foreach (DictionaryEntry item in hashtable)
        {
            Console.WriteLine($"hashtable[{item.Key}] = {item.Value}");
        }

        // SortedList: Demonstrate adding and accessing sorted key-value pairs
        var sortedList = new SortedList();
        sortedList.Add(3, 10);
        sortedList[2] = "Hello, World!"; // Update or add
        sortedList.Add(1, true);
        foreach (DictionaryEntry item in sortedList)
        {
            Console.WriteLine($"sortedList[{item.Key}] = {item.Value}");
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Displays the items of any given collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to display.</param>
    /// <param name="name">The name of the collection type.</param>
    private static void DisplayCollection<T>(IEnumerable<T> collection, string name)
    {
        Console.WriteLine($"{name}:");
        foreach (var item in collection)
        {
            Console.WriteLine("  " + item);
        }
    }

    #endregion
}
