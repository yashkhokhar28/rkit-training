namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Implementation of the IProductService interface.
    /// This service handles the logic for retrieving product details.
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Retrieves the product name based on the provided product ID.
        /// This method simulates a product lookup based on the product ID.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <returns>The name of the product corresponding to the given product ID.</returns>
        public string GetProductName(int productId)
        {
            // Example logic: If product ID is 1, return "Laptop"; otherwise, return "Unknown Product"
            return productId == 1 ? "Laptop" : "Unknown Product";
        }
    }
}
