namespace EFWebAPIProject.Models.ENUM
{
    /// <summary>
    /// The EntryType enum defines the types of entries that can be used within the application.
    /// The values represent different operations: Add, Edit, and Delete.
    /// </summary>
    public enum EntryType
    {
        /// <summary>
        /// Represents the Add operation.
        /// This is used when a new entry is being added.
        /// </summary>
        A,

        /// <summary>
        /// Represents the Edit operation.
        /// This is used when an existing entry is being modified.
        /// </summary>
        E,

        /// <summary>
        /// Represents the Delete operation.
        /// This is used when an entry is being removed.
        /// </summary>
        D
    }
}