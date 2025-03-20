using ECommercePortal.Filters;
using ECommercePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ECommercePortal.Helpers;

namespace ECommercePortal.Controllers
{
    /// <summary>
    /// Controller responsible for handling product-related operations (CRUD).
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ProductsController : ApiController
    {
        #region Data
        // Dummy list of products for demonstration
        private static List<ProductModel> lstProducts = new List<ProductModel>
        {
            new ProductModel { ProductID = 1, ProductName = "Product 1", ProductDescription = "Description 1", ProductCode = "P001", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
            new ProductModel { ProductID = 2, ProductName = "Product 2", ProductDescription = "Description 2", ProductCode = "P002", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
            new ProductModel { ProductID = 3, ProductName = "Product 3", ProductDescription = "Description 3", ProductCode = "P003", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
            new ProductModel { ProductID = 4, ProductName = "Product 4", ProductDescription = "Description 4", ProductCode = "P004", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
            new ProductModel { ProductID = 5, ProductName = "Product 5", ProductDescription = "Description 5", ProductCode = "P005", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
            new ProductModel { ProductID = 6, ProductName = "Product 6", ProductDescription = "Description 6", ProductCode = "P006", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
            new ProductModel { ProductID = 7, ProductName = "Product 7", ProductDescription = "Description 7", ProductCode = "P007", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
            new ProductModel { ProductID = 8, ProductName = "Product 8", ProductDescription = "Description 8", ProductCode = "P008", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
            new ProductModel { ProductID = 9, ProductName = "Product 9", ProductDescription = "Description 9", ProductCode = "P009", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
            new ProductModel { ProductID = 10, ProductName = "Product 10", ProductDescription = "Description 10", ProductCode = "P010", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
        };

        private const string CacheKey = "productList"; // Cache key for storing products list
        #endregion

        #region GetAllProducts
        /// <summary>
        /// Retrieves the list of all products.
        /// </summary>
        /// <returns>An enumerable collection of products.</returns>
        /// <response code="200">Returns the list of products.</response>
        [HttpGet]
        [Route("api/products")]
        [JWTAuthorizationFilter]
        public IEnumerable<ProductModel> Get()
        {
            // Try fetching the product list from cache
            var cachedProductList = CacheHelper.Get(CacheKey);

            if (cachedProductList != null)
            {
                return (IEnumerable<ProductModel>)cachedProductList; // Return cached products if available
            }

            // If not in cache, return the list of products and cache it
            CacheHelper.Set(CacheKey, lstProducts, TimeSpan.FromMinutes(30)); // Cache for 30 minutes
            return lstProducts;
        }
        #endregion

        #region GetProductsByID
        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID, or a 404 if not found.</returns>
        /// <response code="200">Returns the product details.</response>
        /// <response code="404">Product not found.</response>
        [HttpGet]
        [Route("api/products/{id}")]
        [JWTAuthorizationFilter]
        public IHttpActionResult Get(int id)
        {
            // Try fetching the product from cache by ID
            var cachedProductList = CacheHelper.Get(CacheKey);

            if (cachedProductList == null)
            {
                // If no cache, return the product list and cache it
                CacheHelper.Set(CacheKey, lstProducts, TimeSpan.FromMinutes(30));
            }

            ProductModel productModel = lstProducts.FirstOrDefault(p => p.ProductID == id);

            if (productModel == null)
            {
                return NotFound(); // Return 404 if product is not found
            }
            return Ok(productModel); // Return 200 if the product is found
        }
        #endregion

        #region PostProduct
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product with a 201 status code.</returns>
        /// <response code="201">Returns the created product.</response>
        /// <response code="400">If the model is invalid.</response>
        [HttpPost]
        [Route("api/products")]
        [JWTAuthorizationFilter("Admin")]
        public IHttpActionResult Post([FromBody] ProductModel productModel)
        {
            if (productModel == null)
            {
                return BadRequest("Invalid product data."); // Return 400 for invalid product data
            }

            // Assign a new ProductID for the new product
            productModel.ProductID = lstProducts.Max(p => p.ProductID) + 1;
            productModel.CreatedDate = DateTime.Now;
            productModel.ModifiedDate = DateTime.Now;

            lstProducts.Add(productModel);

            // Update the cache with the new product list
            CacheHelper.Set(CacheKey, lstProducts, TimeSpan.FromMinutes(30));

            return Created($"api/Products/{lstProducts.Count - 1}", productModel); // Returns 201 Created
        }
        #endregion

        #region PutProduct
        /// <summary>
        /// Updates an existing product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>A response indicating success or failure.</returns>
        /// <response code="200">If the product was updated successfully.</response>
        /// <response code="400">If the model is invalid or if the ProductID is changed.</response>
        /// <response code="404">If the product with the specified ID was not found.</response>
        [HttpPut]
        [Route("api/products/{id}")]
        [JWTAuthorizationFilter("Admin")]
        public IHttpActionResult Put(int id, [FromBody] ProductModel productModel)
        {
            // Check if the productModel is valid
            if (productModel == null)
            {
                return BadRequest("Invalid product data."); // Return 400 for invalid data
            }

            // Find the product in the list based on the ID
            ProductModel existingProduct = lstProducts.FirstOrDefault(p => p.ProductID == id);
            if (existingProduct == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            // Update the product details (excluding ProductID)
            existingProduct.ProductName = productModel.ProductName;
            existingProduct.ProductDescription = productModel.ProductDescription;
            existingProduct.ProductCode = productModel.ProductCode;
            existingProduct.ModifiedDate = DateTime.Now;

            // Update the cache after modifying the product
            CacheHelper.Set(CacheKey, lstProducts, TimeSpan.FromMinutes(30));

            return Ok(existingProduct); // Return the updated product
        }
        #endregion

        #region DeleteProductByID
        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        /// <response code="200">If the product was deleted successfully.</response>
        /// <response code="404">If the product with the specified ID was not found.</response>
        [HttpDelete]
        [Route("api/products/{id}")]
        [JWTAuthorizationFilter("Admin")]
        public IHttpActionResult Delete(int id)
        {
            ProductModel productModel = lstProducts.FirstOrDefault(p => p.ProductID == id);

            if (productModel == null)
            {
                return NotFound(); // Return 404 if product not found
            }

            lstProducts.Remove(productModel); // Remove the product from the list

            // Update the cache after removing the product
            CacheHelper.Set(CacheKey, lstProducts, TimeSpan.FromMinutes(30));

            return Ok(); // Return 200 OK to indicate success
        }
        #endregion
    }
}