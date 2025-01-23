using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
    /// <summary>
    /// The HomeController class handles HTTP requests related to "Home" resources.
    /// It demonstrates various routing techniques including attribute routing, 
    /// route parameters, optional parameters, and route constraints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Handles GET requests to 'api/home' and returns a collection of string values.
        /// </summary>
        /// <returns>A collection of string values.</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Handles GET requests to 'api/home/{id}' and returns a string value for the given id.
        /// </summary>
        /// <param name="id">The ID of the resource to retrieve.</param>
        /// <returns>A string value corresponding to the given id.</returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Handles POST requests to 'api/home' and accepts a value from the request body.
        /// This action can be used to create or modify resources.
        /// </summary>
        /// <param name="value">The value to be posted in the request body.</param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Handles PUT requests to 'api/home/{id}' with an optional route parameter (id?).
        /// This action can be used to update an existing resource.
        /// </summary>
        /// <param name="id">The ID of the resource to update. This parameter is optional.</param>
        [HttpPut("{id?}")]
        public void Put(int id)
        {
        }

        /// <summary>
        /// Handles DELETE requests to 'api/home/{id}' and enforces a route constraint 
        /// to ensure the id parameter is an integer.
        /// </summary>
        /// <param name="id">The ID of the resource to delete, which must be an integer.</param>
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
        }
    }
}
