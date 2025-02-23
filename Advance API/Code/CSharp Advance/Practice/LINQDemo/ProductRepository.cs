using LINQDemo.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;

namespace LINQDemo
{
    /// <summary>
    /// Repository class for interacting with the Product table in the MySQL database.
    /// </summary>
    public class ProductRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes the ProductRepository with the connection string from the configuration.
        /// </summary>
        public ProductRepository()
        {
            // Retrieve connection string from configuration
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>An IEnumerable of ProductModel objects representing all products.</returns>
        public IEnumerable<ProductModel> GetAllProducts()
        {
            // List to store all the products fetched from the database
            List<ProductModel> lstProducts = new List<ProductModel>();

            // Establish a connection to the MySQL database using the connection string
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                // Open the database connection
                objMySqlConnection.Open();

                // SQL query to fetch ProductID, ProductName, ProductDescription, ProductCode, CreatedAt, and ModifiedAt from the Product table
                using (MySqlCommand objMySqlCommand = new MySqlCommand("SELECT ProductID,ProductName,ProductDescription,ProductCode,CreatedAt,ModifiedAt FROM Product", objMySqlConnection))
                {
                    // Execute the query and read the results using a MySqlDataReader
                    using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                    {
                        // Read the data row by row
                        while (objMySqlDataReader.Read())
                        {
                            // Create a new ProductModel object and populate it with the data from the reader
                            lstProducts.Add(new ProductModel
                            {
                                ProductID = objMySqlDataReader.GetInt32("ProductID"),
                                ProductName = objMySqlDataReader.GetString("ProductName"),
                                ProductDescription = objMySqlDataReader.GetString("ProductDescription"),
                                ProductCode = objMySqlDataReader.GetString("ProductCode"),
                                CreatedDate = objMySqlDataReader.GetDateTime("CreatedAt"),
                                ModifiedDate = objMySqlDataReader.GetDateTime("ModifiedAt")
                            });
                        }
                    }
                }
            }

            // Return the list of products
            return lstProducts;
        }
    }
}