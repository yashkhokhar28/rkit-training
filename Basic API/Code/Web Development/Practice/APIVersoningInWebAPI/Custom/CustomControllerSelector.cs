using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace APIVersioningInWebAPI.Custom
{
    /// <summary>
    /// Custom controller selector for API versioning.
    /// Determines which controller to invoke based on version information in the request.
    /// </summary>
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// The configuration for the HTTP server.
        /// </summary>
        private readonly HttpConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomControllerSelector"/> class.
        /// </summary>
        /// <param name="config">The HTTP configuration object.</param>
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        /// <summary>
        /// Selects the appropriate controller based on versioning logic.
        /// </summary>
        /// <param name="request">The incoming HTTP request.</param>
        /// <returns>The appropriate <see cref="HttpControllerDescriptor"/> for the request.</returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // Extract route data and controller mappings
            var routeData = request.GetRouteData();
            var controllers = GetControllerMapping();
            var controllerName = routeData.Values["controller"];

            // Default version if none is specified
            string version = "2";

            // Parse query strings and headers to determine the version
            var queryStrings = HttpUtility.ParseQueryString(request.RequestUri.Query);

            if (routeData.Values.ContainsKey("version"))
            {
                version = routeData.Values["version"].ToString();
            }
            else if (queryStrings["v"] != null)
            {
                version = queryStrings["v"].ToString();
            }
            else if (request.Headers.Contains("X-CustomerService-Version"))
            {
                version = request.Headers.GetValues("X-CustomerService-Version").FirstOrDefault();
            }

            // Build the versioned controller name
            string controller = $"{controllerName}V{version}";

            // Return the corresponding controller if it exists
            if (controllers.ContainsKey(controller))
            {
                return controllers[controller];
            }

            // Fallback to the base class's implementation if no versioned controller is found
            return base.SelectController(request);
        }
        
    }
}