namespace CustomListLibrary
{
    public class CustomList<T> : List<T>
    {
        public CustomList() : base() { }

        // Example: Add a method to remove all duplicates from the list
        public void RemoveDuplicates()
        {
            HashSet<T> uniqueItems = new HashSet<T>(this);
            this.Clear();
            this.AddRange(uniqueItems);
        }

        // Example: Add a method to get the index of the first element matching a condition
        public int FindIndex(Func<T, bool> predicate)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (predicate(this[i]))
                    return i;
            }
            return -1; // Not found
        }
    }
}