using System;
using System.Security.Cryptography;
using System.Text;

namespace StockPortfolioAPI.Security
{
    /// <summary>
    /// Provides methods for hashing and verifying passwords using SHA-256.
    /// </summary>
    public class HashHelper
    {
        /// <summary>
        /// Computes the SHA-256 hash of the given password.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>The base64-encoded SHA-256 hash of the password.</returns>
        public static string ComputeSHA256Hash(string password)
        {
            using (SHA256 objSHA256 = SHA256.Create())
            {
                var hashedBytes = objSHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        /// <summary>
        /// Verifies if the entered password matches the stored password hash.
        /// </summary>
        /// <param name="enteredPassword">The password entered by the user.</param>
        /// <param name="storedPasswordHash">The stored hash of the password.</param>
        /// <returns>True if the entered password matches the stored password hash, otherwise false.</returns>
        public static bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            var enteredPasswordHash = ComputeSHA256Hash(enteredPassword);
            return enteredPasswordHash == storedPasswordHash;
        }
    }
}