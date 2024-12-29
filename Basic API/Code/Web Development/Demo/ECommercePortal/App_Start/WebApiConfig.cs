using ECommercePortal.Filters;
using Swashbuckle.Application;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ECommercePortal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable CORS globally
            EnableCorsAttribute cors = new EnableCorsAttribute("http://127.0.0.1:5500", "*", "*"); // Allow all origins, headers, and methods
            config.EnableCors(cors);

            // Add global filters
            config.Filters.Add(new ExceptionHandlingFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SwaggerRedirect",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(
                    message => message.RequestUri.ToString(),
                    "swagger"
                )
            );
        }
    }
}
