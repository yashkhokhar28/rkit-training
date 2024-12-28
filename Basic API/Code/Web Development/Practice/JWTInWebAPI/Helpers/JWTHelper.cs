using Newtonsoft.Json;
using System;

namespace JWTInWebAPI.Helpers
{
    /// <summary>
    /// Helper class for generating and validating JWT tokens.
    /// </summary>
    public static class JWTHelper
    {
        /// <summary>
        /// Generates a JWT token with encrypted payload containing username, role, and expiration time.
        /// </summary>
        /// <param name="username">The username for which the token is generated.</param>
        /// <param name="role">The role of the user (e.g., admin, user).</param>
        /// <param name="expirationHours">The expiration time in hours (default is 1 hour).</param>
        /// <returns>An anonymous object containing the encrypted JWT token.</returns>
        public static object GenerateJWTToken(string username, string role, int expirationHours = 1)
        {
            // Creating a payload with the necessary claims: username, role, and expiration time
            var payload = new
            {
                username = username,
                role = role,
                exp = DateTime.UtcNow.AddHours(expirationHours).ToString("o") // ISO 8601 format for expiration time
            };

            // Serializing the payload into JSON format
            string jsonPayload = JsonConvert.SerializeObject(payload);

            // Encrypting the JSON payload using AES encryption
            string encryptedToken = AESHelper.Encrypt(jsonPayload);

            // Returning the encrypted token in an anonymous object
            return new { Token = encryptedToken };
        }

        /// <summary>
        /// Validates a given JWT token by decrypting it and checking its expiration time.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <param name="payload">The decrypted payload if the token is valid.</param>
        /// <returns>True if the token is valid, otherwise false.</returns>
        public static bool ValidateJWTToken(string token, out string payload)
        {
            payload = null;

            try
            {
                // Decrypting the token to retrieve the payload
                payload = AESHelper.Decrypt(token);

                // Deserializing the decrypted payload to extract the expiration time and other data
                var tokenData = JsonConvert.DeserializeObject<dynamic>(payload);
                DateTime expiration = DateTime.Parse(tokenData.exp.ToString());

                // Checking if the token has expired
                if (DateTime.UtcNow > expiration)
                {
                    return false; // Token is expired
                }

                return true; // Token is valid
            }
            catch
            {
                // If any error occurs (e.g., decryption failure or malformed token), return false
                return false; // Token is invalid
            }
        }
    }
}