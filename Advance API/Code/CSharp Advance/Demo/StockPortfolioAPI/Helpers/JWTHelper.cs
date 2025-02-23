using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace StockPortfolioAPI.Helpers
{
    /// <summary>
    /// Provides methods to generate, validate, and extract information from JWT tokens.
    /// </summary>
    public class JWTHelper
    {
        private static string SecretKey = "795a6fdc7536b8ba885d3140066d7a2fb85d836b4c66b641f3b0480dbe08961d";

        /// <summary>
        /// Generates a JWT token for a given username, userID, and role.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="userID">The ID of the user.</param>
        /// <param name="role">The role of the user.</param>
        /// <param name="expirationHours">The number of hours the token is valid for.</param>
        /// <returns>An object containing the generated JWT token.</returns>
        public static object GenerateJWTToken(string username, int userID, string role, int expirationHours = 1)
        {
            // Define the claims for the token
            Claim[] arrClaims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, userID.ToString()),
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

        /// <summary>
        /// Validates a JWT token and extracts the payload.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <param name="payload">The extracted payload from the token if valid.</param>
        /// <returns>A boolean indicating whether the token is valid.</returns>
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

                // Extract claims from the validated token
                var usernameClaim = objClaimsPrincipal.FindFirst(ClaimTypes.Name);
                var roleClaim = objClaimsPrincipal.FindFirst(ClaimTypes.Role);

                if (usernameClaim != null && roleClaim != null)
                {
                    // Return the username and role as a combined payload
                    payload = $"{{\"username\": \"{usernameClaim.Value}\", \"role\": \"{roleClaim.Value}\"}}";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                // If the token is invalid (signature mismatch, expired, etc.), return false
                return false;
            }
        }

        /// <summary>
        /// Extracts the user ID from a JWT token.
        /// </summary>
        /// <param name="token">The JWT token to extract the user ID from.</param>
        /// <returns>The user ID extracted from the token.</returns>
        public static int GetUserIdFromToken(string token)
        {
            JwtSecurityTokenHandler objJwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = objJwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null)
            {
                throw new UnauthorizedAccessException("Invalid JWT token.");
            }

            // Get the userId claim from the token
            var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "nameid");

            if (userIdClaim != null)
            {
                return int.Parse(userIdClaim.Value);  // Return the UserId as an integer
            }

            throw new UnauthorizedAccessException("User ID not found in the token.");
        }
    }
}