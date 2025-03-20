using System;
using System.Web.Http;

namespace HTTPCachingInWebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for managing product-related operations.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Retrieves the list of products, either from the cache or by fetching from a data source.
        /// </summary>
        /// <returns>An <see cref="IHttpActionResult"/> containing the list of products.</returns>
        [HttpGet]
        [Route("api/products")]
        public IHttpActionResult GetProducts()
        {
            // Define a unique key for caching the products list
            string cacheKey = "ProductsCacheKey";

            // Attempt to retrieve the products from the cache
            var cachedProducts = CacheHelper.Get(cacheKey);

            // If the products are found in the cache, return them as the response
            if (cachedProducts != null)
            {
                return Ok(cachedProducts); // Return cached data
            }

            // If the products are not found in the cache, simulate fetching from a data source (e.g., database)
            var products = new[] { "Product 1", "Product 2", "Product 3" };

            // Cache the fetched products for 10 minutes to improve future response times
            CacheHelper.Set(cacheKey, products, TimeSpan.FromMinutes(10));

            // Return the fetched products as the response
            return Ok(products);
        }
    }
}