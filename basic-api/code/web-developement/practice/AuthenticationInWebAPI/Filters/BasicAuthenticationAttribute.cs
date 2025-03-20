using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AuthenticationInWebAPI.Filters
{
    /// <summary>
    /// A custom authentication filter that implements Basic Authentication for Web API.
    /// Inherits from <see cref="AuthorizationFilterAttribute"/>.
    /// </summary>
    public class CustomAuthenticationFilter : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Called when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">
        /// The action context, which encapsulates information for using <see cref="AuthorizationFilterAttribute"/>.
        /// </param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the action allows anonymous access
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)
            {
                // Skip authentication if the action has the [AllowAnonymous] attribute
                return;
            }

            // Check if the Authorization header exists and is of type "Basic"
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Basic")
            {
                // Handle cases where the Authorization header is missing or invalid
                HandleUnauthorized(actionContext);
                return;
            }

            try
            {
                // Decode and validate the credentials from the Authorization header
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter));
                var parts = credentials.Split(':');

                if (parts.Length != 2)
                {
                    // Invalid credential format
                    HandleUnauthorized(actionContext);
                    return;
                }

                var username = parts[0];
                var password = parts[1];

                // Validate the user credentials
                if (!IsAuthorizedUser(username, password))
                {
                    HandleUnauthorized(actionContext);
                    return;
                }

                // Set the Principal for authenticated users
                var identity = new GenericIdentity(username);
                var principal = new GenericPrincipal(identity, null); // No roles specified
                Thread.CurrentPrincipal = principal;

                if (System.Web.HttpContext.Current != null)
                {
                    System.Web.HttpContext.Current.User = principal;
                }
            }
            catch (FormatException)
            {
                // Handle invalid Base64 encoding in the Authorization header
                HandleUnauthorized(actionContext);
            }
        }

        /// <summary>
        /// Validates the user credentials.
        /// </summary>
        /// <param name="username">The username provided by the client.</param>
        /// <param name="password">The password provided by the client.</param>
        /// <returns>
        ///   <c>true</c> if the credentials are valid; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAuthorizedUser(string username, string password)
        {
            // Replace this with actual validation logic, such as checking against a database
            return username == "admin" && password == "password";
        }

        /// <summary>
        /// Handles unauthorized requests by sending a 401 Unauthorized response.
        /// </summary>
        /// <param name="actionContext">The action context of the current request.</param>
        private void HandleUnauthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
            // Add the WWW-Authenticate header to prompt for credentials
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"localhost\"");
        }
    }
}
