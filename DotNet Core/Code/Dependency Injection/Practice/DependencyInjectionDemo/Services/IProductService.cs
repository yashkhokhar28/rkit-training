namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Interface for handling product-related operations.
    /// This service is responsible for fetching product details.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves the product name based on the provided product ID.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>The name of the product.</returns>
        string GetProductName(int productId);
    }
}
