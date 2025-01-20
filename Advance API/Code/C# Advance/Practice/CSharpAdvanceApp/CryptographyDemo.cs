using System.Security.Cryptography;
using System.Text;

namespace CSharpAdvanceApp
{
    /// <summary>
    /// Demonstrates various cryptographic operations such as symmetric encryption, asymmetric encryption, and hashing.
    /// </summary>
    public class CryptographyDemo
    {
        /// <summary>
        /// Runs the cryptography demo by asking the user for input and demonstrating encryption and decryption.
        /// </summary>
        public void RunCryptographyDemo()
        {
            Console.WriteLine("Enter Text: ");
            string message = Console.ReadLine();

            // Symmetric Encryption (AES)
            Console.WriteLine("Symmetric Encryption:");
            byte[] symmetricKey = SymmetricEncryption.GenerateKey(); // Generate a new symmetric key

            var (symmetricCipherText, symmetricIV) = SymmetricEncryption.Encrypt(message, symmetricKey); // Encrypt the message
            Console.WriteLine("Encrypted Message (Symmetric): " + Convert.ToBase64String(symmetricCipherText));

            string symmetricDecryptedMessage = SymmetricEncryption.Decrypt(symmetricCipherText, symmetricKey, symmetricIV); // Decrypt the message
            Console.WriteLine("Decrypted Message (Symmetric): " + symmetricDecryptedMessage);

            // Rijndael Encryption
            Console.WriteLine("\nRijndael Encryption:");
            byte[] rijndaelKey = RijndaelEncryption.GenerateKey(); // Generate a new Rijndael key

            var (rijndaelCipherText, rijndaelIV) = RijndaelEncryption.Encrypt(message, rijndaelKey); // Encrypt the message
            Console.WriteLine("Encrypted Message (Rijndael): " + Convert.ToBase64String(rijndaelCipherText));

            string rijndaelDecryptedMessage = RijndaelEncryption.Decrypt(rijndaelCipherText, rijndaelKey, rijndaelIV); // Decrypt the message
            Console.WriteLine("Decrypted Message (Rijndael): " + rijndaelDecryptedMessage);

            // Asymmetric Encryption (RSA)
            Console.WriteLine("\nAsymmetric Encryption:");
            RSAParameters privateKey;
            RSAParameters publicKey = AsymmetricEncryption.GenerateKeyPair(out privateKey); // Generate RSA key pair

            byte[] encryptedMessage = AsymmetricEncryption.Encrypt(message, publicKey); // Encrypt the message using the public key
            Console.WriteLine("Encrypted Message: " + Convert.ToBase64String(encryptedMessage));

            string decryptedMessage = AsymmetricEncryption.Decrypt(encryptedMessage, privateKey); // Decrypt the message using the private key
            Console.WriteLine("Decrypted Message: " + decryptedMessage);

            // Hashing
            Console.WriteLine("\nHashing Encryption:");
            Console.WriteLine("\nUsing SHA256:");
            string sha256Hash = Hashing.ComputeSHA256Hash(message); // Compute SHA256 hash
            Console.WriteLine("SHA256 Hash: " + sha256Hash);

            Console.WriteLine("\nUsing HMAC-SHA256:");
            byte[] hmacKey = Hashing.GenerateSalt(32); // Generate a random key for HMAC
            string hmacHash = Hashing.ComputeHMACSHA256(message, hmacKey); // Compute HMAC-SHA256 hash
            Console.WriteLine("HMAC-SHA256 Hash: " + hmacHash);

            // Password Hashing with PBKDF2
            Console.WriteLine("\nEnter password to hash: ");
            string password = Console.ReadLine();
            var (passwordHash, salt) = Hashing.HashPassword(password); // Hash the password
            Console.WriteLine($"Password Hash: {passwordHash}\nSalt: {salt}");

            // Verify Password
            Console.WriteLine("\nEnter the password again to verify: ");
            string verifyPassword = Console.ReadLine();
            bool isMatch = Hashing.VerifyPassword(verifyPassword, passwordHash, salt); // Verify the password
            Console.WriteLine("Password Match: " + (isMatch ? "Yes" : "No"));
        }
    }

    /// <summary>
    /// Provides methods for symmetric encryption and decryption using AES algorithm.
    /// </summary>
    public class SymmetricEncryption
    {
        /// <summary>
        /// Generates a new random AES key.
        /// </summary>
        /// <returns>A byte array containing the AES key.</returns>
        public static byte[] GenerateKey()
        {
            using (Aes objAes = Aes.Create())
            {
                objAes.GenerateKey();
                return objAes.Key;
            }
        }

        /// <summary>
        /// Encrypts the given plain text using the specified key.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <param name="key">The encryption key.</param>
        /// <returns>A tuple containing the cipher text and initialization vector (IV).</returns>
        public static (byte[] CipherText, byte[] IV) Encrypt(string plainText, byte[] key)
        {
            using (Aes objAes = Aes.Create())
            {
                objAes.Key = key;
                objAes.GenerateIV(); // Generate a new IV for each encryption
                using (ICryptoTransform objICryptoTransform = objAes.CreateEncryptor(objAes.Key, objAes.IV))
                {
                    byte[] arrPlainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] arrCipherBytes = objICryptoTransform.TransformFinalBlock(arrPlainBytes, 0, arrPlainBytes.Length);
                    return (arrCipherBytes, objAes.IV);
                }
            }
        }

        /// <summary>
        /// Decrypts the given cipher text using the specified key and IV.
        /// </summary>
        /// <param name="cipherText">The cipher text to decrypt.</param>
        /// <param name="key">The decryption key.</param>
        /// <param name="iv">The initialization vector (IV).</param>
        /// <returns>The decrypted plain text.</returns>
        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Aes objAes = Aes.Create())
            {
                objAes.Key = key;
                objAes.IV = iv;
                using (ICryptoTransform objICryptoTransform = objAes.CreateDecryptor(objAes.Key, objAes.IV))
                {
                    byte[] arrPlainBytes = objICryptoTransform.TransformFinalBlock(cipherText, 0, cipherText.Length);
                    return Encoding.UTF8.GetString(arrPlainBytes);
                }
            }
        }
    }

    /// <summary>
    /// Provides methods for symmetric encryption and decryption using Rijndael algorithm.
    /// </summary>
    public class RijndaelEncryption
    {
        /// <summary>
        /// Encrypts the given plain text using the specified key.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <param name="key">The encryption key.</param>
        /// <returns>A tuple containing the cipher text and initialization vector (IV).</returns>
        public static (byte[], byte[]) Encrypt(string plainText, byte[] key)
        {
            using (Rijndael objRijndael = Rijndael.Create())
            {
                objRijndael.Key = key;
                objRijndael.GenerateIV();
                using (ICryptoTransform objICryptoTransform = objRijndael.CreateEncryptor(objRijndael.Key, objRijndael.IV))
                {
                    byte[] arrPlainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] arrCipherBytes = objICryptoTransform.TransformFinalBlock(arrPlainBytes, 0, arrPlainBytes.Length);
                    return (arrCipherBytes, objRijndael.IV);
                }
            }
        }

        /// <summary>
        /// Decrypts the given cipher text using the specified key and IV.
        /// </summary>
        /// <param name="cipherText">The cipher text to decrypt.</param>
        /// <param name="key">The decryption key.</param>
        /// <param name="iv">The initialization vector (IV).</param>
        /// <returns>The decrypted plain text.</returns>
        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Rijndael objRijndael = Rijndael.Create())
            {
                objRijndael.Key = key;
                objRijndael.IV = iv;
                using (ICryptoTransform objICryptoTransform = objRijndael.CreateDecryptor(objRijndael.Key, objRijndael.IV))
                {
                    byte[] arrPlainBytes = objICryptoTransform.TransformFinalBlock(cipherText, 0, cipherText.Length);
                    return Encoding.UTF8.GetString(arrPlainBytes);
                }
            }
        }

        /// <summary>
        /// Generates a new random Rijndael key.
        /// </summary>
        /// <returns>A byte array containing the Rijndael key.</returns>
        public static byte[] GenerateKey()
        {
            using (var rijndael = Rijndael.Create())
            {
                rijndael.KeySize = 256; // 256-bit key
                rijndael.GenerateKey();
                return rijndael.Key;
            }
        }
    }

    /// <summary>
    /// Provides methods for asymmetric encryption and decryption using RSA algorithm.
    /// </summary>
    public class AsymmetricEncryption
    {
        /// <summary>
        /// Generates an RSA key pair.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <returns>The public key.</returns>
        public static RSAParameters GenerateKeyPair(out RSAParameters privateKey)
        {
            using (RSA objRSA = RSA.Create())
            {
                objRSA.KeySize = 2048;
                privateKey = objRSA.ExportParameters(true); // Private key
                return objRSA.ExportParameters(false);      // Public key
            }
        }

        /// <summary>
        /// Encrypts the given plain text using the specified public key.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns>The encrypted data.</returns>
        public static byte[] Encrypt(string plainText, RSAParameters publicKey)
        {
            using (RSA objRSA = RSA.Create())
            {
                objRSA.ImportParameters(publicKey);
                byte[] arrPlainBytes = Encoding.UTF8.GetBytes(plainText);
                return objRSA.Encrypt(arrPlainBytes, RSAEncryptionPadding.Pkcs1);
            }
        }

        /// <summary>
        /// Decrypts the given cipher text using the specified private key.
        /// </summary>
        /// <param name="cipherText">The cipher text to decrypt.</param>
        /// <param name="privateKey">The private key.</param>
        /// <returns>The decrypted plain text.</returns>
        public static string Decrypt(byte[] cipherText, RSAParameters privateKey)
        {
            using (RSA objRSA = RSA.Create())
            {
                objRSA.ImportParameters(privateKey);
                byte[] arrPlainBytes = objRSA.Decrypt(cipherText, RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(arrPlainBytes);
            }
        }
    }

    /// <summary>
    /// Provides methods for hashing and verifying hashes using SHA-256 and HMAC-SHA256 algorithms.
    /// </summary>
    public class Hashing
    {
        /// <summary>
        /// Computes a SHA256 hash for the given input.
        /// </summary>
        /// <param name="input">The input to hash.</param>
        /// <returns>The computed hash.</returns>
        public static string ComputeSHA256Hash(string input)
        {
            using (SHA256 objSHA256 = SHA256.Create())
            {
                byte[] arrInputBytes = Encoding.UTF8.GetBytes(input);
                byte[] arrHashBytes = objSHA256.ComputeHash(arrInputBytes);
                return Convert.ToBase64String(arrHashBytes);
            }
        }

        /// <summary>
        /// Computes a HMAC-SHA256 hash for the given input using the specified key.
        /// </summary>
        /// <param name="input">The input to hash.</param>
        /// <param name="key">The key to use for HMAC.</param>
        /// <returns>The computed hash.</returns>
        public static string ComputeHMACSHA256(string input, byte[] key)
        {
            using (HMACSHA256 objHMACSHA256 = new HMACSHA256(key))
            {
                byte[] arrInputBytes = Encoding.UTF8.GetBytes(input);
                byte[] arrHashBytes = objHMACSHA256.ComputeHash(arrInputBytes);
                return Convert.ToBase64String(arrHashBytes);
            }
        }

        /// <summary>
        /// Hashes a password using PBKDF2.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="iterations">The number of iterations to use.</param>
        /// <returns>A tuple containing the hashed password and the salt.</returns>
        public static (string Hash, string Salt) HashPassword(string password, int iterations = 10000)
        {
            byte[] arrSalt = GenerateSalt();
            using (Rfc2898DeriveBytes objRfc2898DeriveBytes = new Rfc2898DeriveBytes(password, arrSalt, iterations))
            {
                byte[] arrHash = objRfc2898DeriveBytes.GetBytes(32); // 256-bit hash
                return (Convert.ToBase64String(arrHash), Convert.ToBase64String(arrSalt));
            }
        }

        /// <summary>
        /// Verifies a password against a given hash and salt.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="hash">The hash to verify against.</param>
        /// <param name="salt">The salt used in the hash.</param>
        /// <param name="iterations">The number of iterations used in the hash.</param>
        /// <returns>True if the password matches the hash; otherwise, false.</returns>
        public static bool VerifyPassword(string password, string hash, string salt, int iterations = 10000)
        {
            byte[] arrSaltBytes = Convert.FromBase64String(salt);
            using (Rfc2898DeriveBytes objRfc2898DeriveBytes = new Rfc2898DeriveBytes(password, arrSaltBytes, iterations))
            {
                byte[] arrComputedHash = objRfc2898DeriveBytes.GetBytes(32);
                return Convert.ToBase64String(arrComputedHash) == hash;
            }
        }

        /// <summary>
        /// Generates a random salt.
        /// </summary>
        /// <param name="size">The size of the salt.</param>
        /// <returns>A byte array containing the salt.</returns>
        public static byte[] GenerateSalt(int size = 16)
        {
            byte[] arrSalt = new byte[size];
            using (RandomNumberGenerator objRandomNumberGenerator = RandomNumberGenerator.Create())
            {
                objRandomNumberGenerator.GetBytes(arrSalt);
            }
            return arrSalt;
        }
    }
}