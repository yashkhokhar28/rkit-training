namespace LINQDemo.Models
{
    /// <summary>
    /// Represents a category of products in the system.
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the product identifier associated with the category.
        /// </summary>
        public int ProductID { get; set; }
    }
}