using AuthenticationInWebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthenticationInWebAPI.Controllers
{
    /// <summary>
    /// The ProductsController handles requests related to products.
    /// This controller demonstrates the use of custom authentication filters.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [CustomAuthenticationFilter] // Custom authentication filter applied to the controller
    public class ProductsController : ApiController
    {
        #region Public Actions

        /// <summary>
        /// Retrieves open data that does not require authentication.
        /// </summary>
        /// <returns>An enumerable collection of strings representing open data.</returns>
        [HttpGet]
        [Route("api/open/products")]
        [AllowAnonymous] // Allows anonymous access to this endpoint
        public IEnumerable<string> GetOpenData()
        {
            // Returning a static list of open data values as an example.
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Retrieves secure data that requires authentication.
        /// </summary>
        /// <returns>An enumerable collection of strings representing secure data.</returns>
        [HttpGet]
        [Route("api/secure/products")]
        public IEnumerable<string> GetSecureData()
        {
            // Returning a static list of secure data values as an example.
            return new string[] { "value1", "value2" };
        }

        #endregion
    }
}
