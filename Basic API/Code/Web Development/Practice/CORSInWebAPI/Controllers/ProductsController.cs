using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CORSInWebAPI.Controllers
{
    /// <summary>
    /// The ProductsController handles requests related to products.
    /// </summary>
    //[EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")] // Uncomment this to enable CORS for the entire controller
    public class ProductsController : ApiController
    {
        #region Actions

        /// <summary>
        /// Retrieves a list of product names.
        /// </summary>
        /// <returns>An enumerable collection of product names as strings.</returns>
        // GET api/products
        //[EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")] // Uncomment this to enable CORS for this specific action
        [Route("api/products")]
        public IEnumerable<string> Get()
        {
            // Returning a static list of product values as an example.
            return new string[] { "value1", "value2" };
        }

        #endregion
    }
}
