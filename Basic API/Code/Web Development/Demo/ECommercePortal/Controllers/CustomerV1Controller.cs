using System.Web.Http;

namespace ECommercePortal.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CustomersV1Controller : ApiController
    {
        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            return Ok(new[] { "Customer1", "Customer2" });
        }
    }
}