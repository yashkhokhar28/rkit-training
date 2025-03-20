using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Text;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(JWTInWebAPI.Startup))]

namespace JWTInWebAPI
{
    /// <summary>
    /// Configures the OWIN pipeline for the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the OWIN pipeline to use JWT Bearer Authentication.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        public void Configuration(IAppBuilder app)
        {
            // Secret key for signing and validating JWT tokens
            string secretKey = "yash@okkoefbhuijmkl@ik#%^jik890#jopjfm8!)(&*ffg@YASH";

            // The issuer of the token
            string issuer = "https://localhost:44318/";

            // The audience for which the token is intended
            string audience = "https://localhost:44318/";

            // Create a symmetric security key using the secret key
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Configure the application to use JWT Bearer Authentication
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                // Set the authentication mode to active
                AuthenticationMode = AuthenticationMode.Active,

                // Define the parameters for token validation
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Ensure the issuer of the token is valid
                    ValidateAudience = true, // Ensure the audience of the token is valid
                    ValidateIssuerSigningKey = true, // Validate the signing key used to issue the token

                    ValidIssuer = issuer, // Specify the valid issuer
                    ValidAudience = audience, // Specify the valid audience
                    IssuerSigningKey = symmetricSecurityKey // Provide the signing key for validation
                }
            });
        }
    }
}