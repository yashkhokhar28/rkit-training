using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates usage of various collection classes in C#.
/// </summary>
public class CollectionClassDemo
{
    #region Public Methods

    /// <summary>
    /// Runs the demonstration for various collection classes.
    /// </summary>
    /// <remarks>
    /// Displays the usage of List<T>, Dictionary<TKey, TValue>, Queue<T>,
    /// Stack<T>, LinkedList<T>, HashSet<T>,
    /// SortedSet<T>, BitArray, ArrayList, Hashtable, and SortedList.
    /// </remarks>
    public static void RunCollectionClassDemo()
    {
        // List<T>: A dynamically sized array for storing elements of a specific type
        var numbers = new List<int> { 10, 20, 30, 40, 50 };
        DisplayCollection(numbers, "List<T>");

        // Dictionary<TKey, TValue>: A key-value pair collection
        var names = new Dictionary<int, string> { { 1, "Alice" }, { 2, "Bob" }, { 3, "Charlie" } };
        foreach (var name in names)
        {
            Console.WriteLine($"names[{name.Key}] = {name.Value}");
        }

        // Queue<T>: A first-in, first-out (FIFO) collection
        var queue = new Queue<int>(new[] { 10, 20, 30 });
        DisplayCollection(queue, "Queue<T>");

        // Stack<T>: A last-in, first-out (LIFO) collection
        var stack = new Stack<int>(new[] { 10, 20, 30 });
        DisplayCollection(stack, "Stack<T>");

        // LinkedList<T>: A doubly-linked list
        var linkedList = new LinkedList<int>(new[] { 10, 20, 30 });
        DisplayCollection(linkedList, "LinkedList<T>");

        // HashSet<T>: A collection of unique elements
        var hashSet = new HashSet<int> { 10, 20, 30 };
        DisplayCollection(hashSet, "HashSet<T>");

        // SortedSet<T>: A sorted collection of unique elements
        var sortedSet = new SortedSet<int> { 30, 20, 10 };
        DisplayCollection(sortedSet, "SortedSet<T>");

        // BitArray: A compact array of Boolean values
        var bitArray = new BitArray(new[] { true, false, true });
        for (int i = 0; i < bitArray.Length; i++)
        {
            Console.WriteLine($"bitArray[{i}] = {bitArray[i]}");
        }

        // ArrayList: A non-generic collection of objects
        var arrayList = new ArrayList { 10, "Hello, World!", true };
        foreach (var item in arrayList)
        {
            Console.WriteLine("item = " + item);
        }

        // Hashtable: A non-generic collection of key-value pairs
        var hashtable = new Hashtable { { 1, 10 }, { 2, "Hello, World!" }, { 3, true } };
        foreach (DictionaryEntry item in hashtable)
        {
            Console.WriteLine($"hashtable[{item.Key}] = {item.Value}");
        }

        // SortedList: A sorted collection of key-value pairs
        var sortedList = new SortedList { { 3, 10 }, { 2, "Hello, World!" }, { 1, true } };
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
        // Prints the collection's name and iterates through each item to display it
        Console.WriteLine($"{name}:");
        foreach (var item in collection)
        {
            Console.WriteLine("  " + item); // Prints each item in the collection
        }
    }

    #endregion
}