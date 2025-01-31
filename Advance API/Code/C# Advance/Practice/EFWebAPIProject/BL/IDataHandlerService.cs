using EFWebAPIProject.Models;
using EFWebAPIProject.Models.ENUM;

namespace EFWebAPIProject.BL
{
    /// <summary>
    /// Interface for handling data operations for entities of type T.
    /// This interface defines methods for pre-saving, validation, and saving data.
    /// </summary>
    /// <typeparam name="T">The type of the object (typically a DTO or POCO) for which data operations are handled.</typeparam>
    public interface IDataHandlerService<T> where T : class
    {
        /// <summary>
        /// Gets or sets the type of operation (e.g., Add, Edit, Delete) to be performed.
        /// </summary>
        EntryType Type { get; set; }

        /// <summary>
        /// Prepares the data before saving it.
        /// This method is invoked to handle any necessary preparation steps (e.g., mapping or transformation) before saving.
        /// </summary>
        /// <param name="objDTO">The data object (DTO or POCO) to be processed.</param>
        void PreSave(T objDTO);

        /// <summary>
        /// Validates the data before performing the save operation.
        /// This method checks if the data is in a valid state for saving, ensuring that no errors occur during the save.
        /// </summary>
        /// <returns>A response indicating whether the validation was successful or if errors were found.</returns>
        Response Validation();

        /// <summary>
        /// Saves the data after validation and preparation.
        /// This method handles the actual saving process, such as adding, updating, or deleting data in the database.
        /// </summary>
        /// <returns>A response indicating whether the save operation was successful or if an error occurred.</returns>
        Response Save();
    }
}