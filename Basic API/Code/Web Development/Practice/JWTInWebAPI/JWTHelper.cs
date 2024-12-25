using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace JWTInWebAPI
{
    /// <summary>
    /// Helper class for generating JSON Web Tokens (JWT).
    /// </summary>
    public static class JWTHelper
    {
        /// <summary>
        /// The secret key used for token generation and validation.
        /// </summary>
        private static readonly string _secretKey = "yash@okkoefbhuijmkl@ik#%^jik890#jopjfm8!)(&*ffg@YASH";

        /// <summary>
        /// The issuer of the token.
        /// </summary>
        private static readonly string _issuer = "https://localhost:44318/";

        /// <summary>
        /// The audience for which the token is intended.
        /// </summary>
        private static readonly string _audience = "https://localhost:44318/";

        /// <summary>
        /// Generates a JWT token based on the provided username, role, and expiration duration.
        /// </summary>
        /// <param name="username">The username for which the token is generated.</param>
        /// <param name="role">The role of the user (e.g., admin or user).</param>
        /// <param name="expirationHours">The number of hours after which the token expires. Default is 1 hour.</param>
        /// <returns>An object containing the generated JWT token as a string.</returns>
        public static Object GenerateJWTToken(string username, string role, int expirationHours = 1)
        {
            // Create a symmetric security key using the secret key
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            // Create signing credentials using the security key and HMAC-SHA256 algorithm
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // Create a list of claims (key-value pairs) to include in the token
            List<Claim> lstClaims = new List<Claim>();
            lstClaims.Add(new Claim("username", username)); // Custom claim for username
            lstClaims.Add(new Claim("role", role)); // Custom claim for user role
            lstClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // Unique identifier for the token
            lstClaims.Add(new Claim(JwtRegisteredClaimNames.Iss, _issuer)); // Issuer claim
            lstClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, _audience)); // Audience claim

            // Create the JWT token with the specified claims, expiration, and signing credentials
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                _issuer,
                _audience,
                lstClaims,
                expires: DateTime.UtcNow.AddHours(expirationHours),
                signingCredentials: signingCredentials);

            // Serialize the token to a string
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            string jwtToken = jwtHandler.WriteToken(jwtSecurityToken);

            // Return the token as an object
            return new { Token = jwtToken };
        }
    }
}