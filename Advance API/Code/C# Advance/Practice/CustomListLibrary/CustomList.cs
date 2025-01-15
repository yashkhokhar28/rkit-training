namespace CustomListLibrary
{
    /// <summary>
    /// A custom implementation of a generic list that extends the functionality of the standard List<T>.
    /// Provides additional features such as removing duplicates and finding the index of an element based on a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class CustomList<T> : List<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomList{T}"/> class.
        /// Inherits all functionality of the base <see cref="List{T}"/> class.
        /// </summary>
        public CustomList() : base() { }

        /// <summary>
        /// Removes duplicate elements from the list, ensuring that each element is unique.
        /// The order of remaining elements is preserved as per their first occurrence.
        /// </summary>
        public void RemoveDuplicates()
        {
            // Create a HashSet to filter out duplicates
            HashSet<T> uniqueItems = new HashSet<T>(this);

            // Clear the current list
            this.Clear();

            // Add back only the unique elements
            this.AddRange(uniqueItems);
        }

        /// <summary>
        /// Finds the index of the first element in the list that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// The zero-based index of the first element that satisfies the condition; 
        /// or -1 if no such element is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
        public int FindIndex(Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            // Iterate through the list and return the index of the first matching element
            for (int i = 0; i < this.Count; i++)
            {
                if (predicate(this[i]))
                    return i; // Return index if the condition is satisfied
            }

            return -1; // Return -1 if no matching element is found
        }
    }
}