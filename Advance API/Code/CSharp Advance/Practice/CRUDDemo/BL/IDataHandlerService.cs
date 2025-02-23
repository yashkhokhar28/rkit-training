using CRUDDemo.Models;
using CRUDDemo.Models.ENUM;
using CRUDDemo.Models.POCO;

namespace CRUDDemo.BL
{
    /// <summary>
    /// Interface defining data handling operations.
    /// </summary>
    /// <typeparam name="T">The type of the data transfer object (DTO).</typeparam>
    public interface IDataHandlerService<T> where T : class
    {
        /// <summary>
        /// Specifies the type of operation (Add, Edit, etc.).
        /// </summary>
        EnmEntryType Type { get; set; }

        /// <summary>
        /// Prepares the data for saving to the database.
        /// </summary>
        /// <param name="objDTO">The data transfer object to prepare.</param>
        void PreSave(T objDTO);

        /// <summary>
        /// Validates the prepared data before saving.
        /// </summary>
        /// <returns>A <see cref="Response"/> indicating the validation result.</returns>
        Response Validation();

        /// <summary>
        /// Saves the prepared data to the database.
        /// </summary>
        /// <returns>A <see cref="Response"/> indicating the save operation result.</returns>
        Response Save();

        /// <summary>
        /// Prepares an object for deletion based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the object to delete.</param>
        /// <returns>The prepared object.</returns>
        EMP01 PreDelete(int id);

        /// <summary>
        /// Validates an object before deletion.
        /// </summary>
        /// <param name="objEMP01">The object to validate.</param>
        /// <returns>A <see cref="Response"/> indicating the validation result.</returns>
        Response ValidationOnDelete(EMP01 objEMP01);

        /// <summary>
        /// Deletes an object from the database based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the object to delete.</param>
        /// <returns>The number of rows affected by the deletion.</returns>
        int Delete(int id);
    }
}