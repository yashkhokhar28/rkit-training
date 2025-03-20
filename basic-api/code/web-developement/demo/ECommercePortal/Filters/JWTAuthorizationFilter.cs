using ECommercePortal.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ECommercePortal.Filters
{
    /// <summary>
    /// JWT Authorization filter for role-based access control.
    /// </summary>
    public class JWTAuthorizationFilter : AuthorizationFilterAttribute
    {
        private readonly string[] _allowedRoles;

        /// <summary>
        /// Constructor for specifying allowed roles.
        /// </summary>
        /// <param name="roles">Comma-separated roles allowed to access the endpoint.</param>
        public JWTAuthorizationFilter(params string[] roles)
        {
            _allowedRoles = roles;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the Authorization header is present
            if (actionContext.Request.Headers.Authorization == null ||
                string.IsNullOrWhiteSpace(actionContext.Request.Headers.Authorization.Parameter))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Missing or invalid token.");
                return;
            }

            string token = actionContext.Request.Headers.Authorization.Parameter;

            try
            {
                // Validate the JWT token
                if (!JWTHelper.ValidateJWTToken(token, out string payload))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid or expired token.");
                    return;
                }

                // Deserialize the token payload
                var tokenData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(payload);
                if (tokenData == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token payload.");
                    return;
                }

                // Extract role from the token
                string userRole = tokenData.role?.ToString();
                if (string.IsNullOrEmpty(userRole))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Role information missing in token.");
                    return;
                }

                // Check if the user's role is allowed
                if (_allowedRoles != null && _allowedRoles.Length > 0 && !_allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Access denied.");
                    return;
                }
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, $"Invalid token. Error: {ex.Message}");
            }
        }
    }
}