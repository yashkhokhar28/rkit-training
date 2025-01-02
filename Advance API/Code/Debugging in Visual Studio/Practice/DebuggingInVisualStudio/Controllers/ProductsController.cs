using DebuggingInVisualStudio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace DebuggingInVisualStudio.Controllers
{
    /// <summary>
    /// Controller responsible for handling product-related operations (CRUD).
    /// </summary>
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
        #endregion

        #region GetAllProducts
        /// <summary>
        /// Retrieves the list of all products.
        /// </summary>
        /// <returns>An enumerable collection of products.</returns>
        [HttpGet]
        [Route("api/products")]
        public IEnumerable<ProductModel> Get()
        {
            return lstProducts;
        }
        #endregion

        #region GetProductsByID
        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID, or a 404 if not found.</returns>
        [HttpGet]
        [Route("api/products/{id}")]
        public IHttpActionResult Get(int id)
        {
            #if DEBUG
            ProductModel productModel = lstProducts.FirstOrDefault(p => p.ProductID == id);
            Debug.WriteLine($"Getting product with ID: {productModel.ProductID}");
            if (productModel == null)
            {
                return NotFound(); // Return 404 if product is not found
            }
            #endif
            return Ok(productModel); // Return 200 if the product is found
        }
#endregion

        #region PostProduct
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productModel">The product to create.</param>
        /// <returns>The created product with a 201 status code.</returns>
        [HttpPost]
        [Route("api/products")]
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

            return Created($"api/products/{productModel.ProductID}", productModel); // Returns 201 Created
        }
        #endregion

        #region PutProduct
        /// <summary>
        /// Updates an existing product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productModel">The updated product data.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPut]
        [Route("api/products/{id}")]
        public IHttpActionResult Put(int id, [FromBody] ProductModel productModel)
        {
            if (productModel == null)
            {
                return BadRequest("Invalid product data."); // Return 400 for invalid data
            }

            ProductModel existingProduct = lstProducts.FirstOrDefault(p => p.ProductID == id);
            if (existingProduct == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            existingProduct.ProductName = productModel.ProductName;
            existingProduct.ProductDescription = productModel.ProductDescription;
            existingProduct.ProductCode = productModel.ProductCode;
            existingProduct.ModifiedDate = DateTime.Now;

            return Ok(existingProduct); // Return the updated product
        }
        #endregion

        #region DeleteProductByID
        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpDelete]
        [Route("api/products/{id}")]
        public IHttpActionResult Delete(int id)
        {
            ProductModel productModel = lstProducts.FirstOrDefault(p => p.ProductID == id);

            if (productModel == null)
            {
                return NotFound(); // Return 404 if product not found
            }

            lstProducts.Remove(productModel); // Remove the product from the list

            return Ok(); // Return 200 OK to indicate success
        }
        #endregion
    }
}