using System.Collections.Generic;
using System.Web.Http;

namespace ECommercePortal.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CustomersV2Controller : ApiController
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1 from V2", "value2 from V2" };
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public string GetById(int id)
        {
            return $"value{id} from V2";
        }
    }
}