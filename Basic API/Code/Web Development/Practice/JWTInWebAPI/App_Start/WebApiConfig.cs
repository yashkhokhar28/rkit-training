using Microsoft.Owin.Security.OAuth;
using Swashbuckle.Application;
using System.Web.Http;

namespace JWTInWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Suppress default host authentication to avoid conflicting authentication mechanisms.
            // This ensures that only the specified authentication (e.g., OAuth or JWT) is used.
            config.SuppressDefaultHostAuthentication();

            // Add a filter to enforce authentication for requests using the specified authentication type (OAuth).
            // This ensures that the API endpoints are protected by the configured authentication mechanism.
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Enable attribute routing for more control over API routes.
            // This allows you to define routes directly at the controller or action level using attributes.
            config.MapHttpAttributeRoutes();

            // Define a default route for the Web API.
            // This is used as a fallback for requests that do not match attribute-defined routes.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Add a route to redirect users to the Swagger UI when they access the root URL.
            // This is useful for providing a visual interface for testing and exploring the API.
            config.Routes.MapHttpRoute(
                name: "SwaggerRedirect",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(message => message.RequestUri.ToString(), "swagger")
            );
        }
    }
}