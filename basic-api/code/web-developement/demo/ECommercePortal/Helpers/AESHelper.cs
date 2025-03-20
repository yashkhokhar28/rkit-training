using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ECommercePortal.Helpers
{
    /// <summary>
    /// Helper class for AES encryption and decryption using a predefined key and IV.
    /// </summary>
    public static class AESHelper
    {
        // Predefined Key (32 bytes for AES-256) and IV (16 bytes for AES) used for encryption and decryption.
        private static readonly string Key = "12345678901234567890123456789012"; // 32-byte key for AES-256
        private static readonly string IV = "1234567890123456"; // 16-byte IV for AES

        /// <summary>
        /// Encrypts the given plaintext using AES encryption.
        /// </summary>
        /// <param name="plainText">The plaintext to encrypt.</param>
        /// <returns>The encrypted ciphertext in Base64 format.</returns>
        public static string Encrypt(string plainText)
        {
            // Create a new AES encryption algorithm instance
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);  // Set the encryption key
                aes.IV = Encoding.UTF8.GetBytes(IV);    // Set the initialization vector
                aes.Mode = CipherMode.CBC;              // Set the cipher mode to CBC (Cipher Block Chaining)
                aes.Padding = PaddingMode.PKCS7;        // Set padding mode to PKCS7

                // Create a memory stream to hold the encrypted data
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Create a CryptoStream to perform the encryption and write to memory stream
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText); // Convert plain text to byte array
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length); // Write encrypted data to stream
                        cryptoStream.FlushFinalBlock();  // Finish encryption and finalize the data
                    }

                    // Convert the encrypted byte array to Base64 and return it
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts the given ciphertext using AES decryption.
        /// </summary>
        /// <param name="cipherText">The Base64-encoded ciphertext to decrypt.</param>
        /// <returns>The decrypted plaintext.</returns>
        public static string Decrypt(string cipherText)
        {
            // Create a new AES decryption algorithm instance
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);  // Set the decryption key
                aes.IV = Encoding.UTF8.GetBytes(IV);    // Set the initialization vector
                aes.Mode = CipherMode.CBC;              // Set the cipher mode to CBC
                aes.Padding = PaddingMode.PKCS7;        // Set padding mode to PKCS7

                // Convert the Base64-encoded ciphertext to a byte array
                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    // Create a CryptoStream to perform the decryption and read from memory stream
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        // Create a StreamReader to read the decrypted data
                        using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
                        {
                            // Return the decrypted plaintext
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}