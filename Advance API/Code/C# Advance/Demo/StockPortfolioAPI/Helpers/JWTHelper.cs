using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockPortfolioAPI.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class JWTHelper
    {
        private static string SecretKey = "795a6fdc7536b8ba885d3140066d7a2fb85d836b4c66b641f3b0480dbe08961d";

        public static object GenerateJWTToken(string username, string role, int expirationHours = 1)
        {
            // Define the claims for the token
            Claim[] arrClaims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddHours(expirationHours).ToString())
            };

            // Create the security key and credentials
            SymmetricSecurityKey objSymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            SigningCredentials objSigningCredentials = new SigningCredentials(objSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // Create the token descriptor with claims and signing credentials
            SecurityTokenDescriptor objSecurityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(arrClaims),
                Expires = DateTime.UtcNow.AddHours(expirationHours),
                SigningCredentials = objSigningCredentials
            };

            // Create the JWT token
            JwtSecurityTokenHandler objJwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken objSecurityToken = objJwtSecurityTokenHandler.CreateToken(objSecurityTokenDescriptor);

            // Return the serialized JWT token as a string
            return new { Token = objJwtSecurityTokenHandler.WriteToken(objSecurityToken) };
        }

        public static bool ValidateJWTToken(string token, out string payload)
        {
            payload = null;

            try
            {
                // Create the token handler
                JwtSecurityTokenHandler objJwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                SymmetricSecurityKey objSymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

                // Set the validation parameters
                TokenValidationParameters objTokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // Do not validate the issuer in this example
                    ValidateAudience = false, // Do not validate the audience in this example
                    ValidateLifetime = true, // Validate expiration
                    IssuerSigningKey = objSymmetricSecurityKey // Use the signing key
                };

                // Validate the token and extract the claims
                ClaimsPrincipal objClaimsPrincipal = objJwtSecurityTokenHandler.ValidateToken(token, objTokenValidationParameters, out var validatedToken);

                // If token is valid, return the claims
                payload = objClaimsPrincipal.Identity.Name;
                return true;
            }
            catch
            {
                // If the token is invalid (signature mismatch, expired, etc.), return false
                return false;
            }
        }
    }
}