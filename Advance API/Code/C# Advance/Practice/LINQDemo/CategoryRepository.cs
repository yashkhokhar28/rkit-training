using LINQDemo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LINQDemo
{
    /// <summary>
    /// Repository class for interacting with the Category table in the MySQL database.
    /// </summary>
    public class CategoryRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes the CategoryRepository with the connection string from configuration.
        /// </summary>
        public CategoryRepository()
        {
            // Retrieve connection string from configuration
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>An IEnumerable of CategoryModel objects representing all categories.</returns>
        public IEnumerable<CategoryModel> GetAllCategories()
        {
            // List to store all the categories fetched from the database
            List<CategoryModel> lstCategories = new List<CategoryModel>();

            // Establish a connection to the MySQL database using the connection string
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                // Open the database connection
                objMySqlConnection.Open();

                // SQL query to fetch CategoryID, CategoryName, and ProductID from the Category table
                using (MySqlCommand objMySqlCommand = new MySqlCommand("SELECT CategoryID,CategoryName,ProductID FROM Category", objMySqlConnection))
                {
                    // Execute the query and read the results using a MySqlDataReader
                    using (MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader())
                    {
                        // Read the data row by row
                        while (objMySqlDataReader.Read())
                        {
                            // Create a new CategoryModel object and populate it with the data from the reader
                            lstCategories.Add(new CategoryModel
                            {
                                CategoryID = objMySqlDataReader.GetInt32("CategoryID"),
                                CategoryName = objMySqlDataReader.GetString("CategoryName"),
                                ProductID = objMySqlDataReader.GetInt32("ProductID")
                            });
                        }
                    }
                }
            }

            // Return the list of categories
            return lstCategories;
        }
    }
}