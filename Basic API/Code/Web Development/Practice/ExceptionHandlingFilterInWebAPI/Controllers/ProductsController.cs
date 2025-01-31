using System;
using System.Web.Http;

namespace ExceptionHandlingFilterInWebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for managing product-related APIs.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Retrieves the list of products.
        /// This action simulates an exception to demonstrate the custom exception filter handling.
        /// </summary>
        /// <returns>
        /// A simulated exception is thrown for demonstration purposes.
        /// </returns>
        /// <exception cref="System.Exception">Simulates an error with a custom message.</exception>
        [HttpGet]
        [Route("api/products")]
        public IHttpActionResult GetProducts()
        {
            // Simulating an error to demonstrate the exception handling filter
            throw new Exception("Something went wrong");
        }
    }
}
