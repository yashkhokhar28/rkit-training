using Microsoft.Owin.Security.OAuth;
using Swashbuckle.Application;
using System.Web.Http;

namespace JWTInWebAPI
{
    /// <summary>
    /// Configures the Web API routes and settings for the application.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the Web API configuration, including routes and other global settings.
        /// </summary>
        /// <param name="config">The configuration object used to set up the Web API.</param>
        public static void Register(HttpConfiguration config)
        {
            // Enable attribute routing, allowing routes to be defined directly on controllers and actions.
            config.MapHttpAttributeRoutes();

            // Define the default route for the Web API.
            // This is used for endpoints that do not use attribute-based routing.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional } // The "id" parameter is optional.
            );

            // Add a route to redirect users to the Swagger UI when accessing the root URL.
            // Swagger provides an interface for testing and exploring the API.
            config.Routes.MapHttpRoute(
                name: "SwaggerRedirect",
                routeTemplate: "", // Matches the root URL of the application.
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(
                    message => message.RequestUri.ToString(),
                    "swagger" // Redirects to the Swagger UI.
                )
            );
        }
    }
}