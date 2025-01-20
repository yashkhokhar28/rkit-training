using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace StockPortfolioAPI.Security
{
    public class HashHelper
    {
        public static string ComputeSHA256Hash(string password)
        {
            using (SHA256 objSHA256 = SHA256.Create())
            {
                var hashedBytes = objSHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            var enteredPasswordHash = ComputeSHA256Hash(enteredPassword);
            return enteredPasswordHash == storedPasswordHash;
        }
    }
}