using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.Controllers
{
    /// <summary>
    /// Controller responsible for handling product-related operations.
    /// Provides endpoints to retrieve product details based on product ID.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Service to handle product operations.
        /// This service is used to fetch product details.
        /// </summary>
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The product service used to fetch product details.</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;  // Dependency Injection of IProductService
        }

        /// <summary>
        /// Retrieves the details of a product based on the product ID.
        /// This endpoint fetches the product name using the product service.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>Returns the product name associated with the provided product ID.</returns>
        [HttpGet("product/{id}")]
        public IActionResult GetProduct(int id)
        {
            // Call the product service to get the product name by ID
            string productName = _productService.GetProductName(id);

            // Return the product name in the response
            return Ok(new
            {
                Product = productName
            });
        }
    }
}
