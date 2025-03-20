using LINQDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Xml.Linq;

namespace LINQDemo.Controllers
{
    /// <summary>
    /// Controller to demonstrate various LINQ operations including LINQ to Objects, LINQ to SQL, LINQ to XML, and LINQ to DataTable.
    /// </summary>
    public class ProductsController : ApiController
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        /// <summary>
        /// Initializes the controller with a ProductRepository and CategoryRepository.
        /// </summary>
        public ProductsController()
        {
            _productRepository = new ProductRepository();
            _categoryRepository = new CategoryRepository();
        }

        /// <summary>
        /// Demonstrates LINQ to Objects with different LINQ operations such as filtering.
        /// </summary>
        /// <returns>A list of filtered and projected names from an array.</returns>
        [HttpGet]
        [Route("api/linq-to-object")]
        public IHttpActionResult LinqToObject()
        {
            string[] arrNames = { "Iron Man", "Iron Man", "Captain America", "Hulk", "Thor", "Loki", "Dr. Strange", "Spider Man" };

            IEnumerable<string> filteredNames = from name in arrNames
                                                where name != "Hulk"
                                                select name;

            return Ok(filteredNames);
        }

        /// <summary>
        /// Demonstrates LINQ to SQL with filtered product data fetched from the repository.
        /// </summary>
        /// <returns>Filtered product data from the repository excluding specific products.</returns>
        [HttpGet]
        [Route("api/linq-to-sql")]
        public IHttpActionResult LinqToSQL()
        {

            var products = _productRepository.GetAllProducts();

            var categories = _categoryRepository.GetAllCategories();


            if (products == null || !products.Any())
                return NotFound();


            IEnumerable<ProductModel> filteredProducts = from product in products
                                                         where product.ProductName != "Smartphone"
                                                         select product;

            return Ok(filteredProducts);
        }

        /// <summary>
        /// Demonstrates LINQ to XML to extract authors from an XML document.
        /// </summary>
        /// <returns>List of book authors extracted from the XML document.</returns>
        [HttpGet]
        [Route("api/linq-to-xml")]
        public IHttpActionResult LinqToXML()
        {

            XElement objXElement = XElement.Load(@"F:\Yash Khokhar\Advance API\Code\C# Advance\Practice\books.xml");


            IEnumerable<string> books = from book in objXElement.Descendants("book")
                                        select book.Element("author").Value;

            return Ok(books);
        }

        /// <summary>
        /// Demonstrates LINQ to DataTable with filtering product data rows based on specific conditions.
        /// </summary>
        /// <returns>Filtered product data rows created after a specific date.</returns>
        [HttpGet]
        [Route("api/linq-to-datatable")]
        public IHttpActionResult LinqToDataTable()
        {
            DataTable objDataTable = new DataTable("Products");


            objDataTable.Columns.Add(new DataColumn("ProductID", typeof(int))
            {
                AllowDBNull = false,
                Unique = true,
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            });
            objDataTable.Columns.Add(new DataColumn("ProductName", typeof(string)) { MaxLength = 50 });
            objDataTable.Columns.Add(new DataColumn("ProductDescription", typeof(string)) { MaxLength = 50 });
            objDataTable.Columns.Add(new DataColumn("ProductCode", typeof(string)) { MaxLength = 7 });
            objDataTable.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
            objDataTable.Columns.Add(new DataColumn("ModifiedAt", typeof(DateTime)));

            objDataTable.Rows.Add(null, "Laptop", "High-performance laptop", "PRD001", DateTime.Now, DateTime.Now);
            objDataTable.Rows.Add(null, "Smartphone", "Latest model smartphone", "PRD002", DateTime.Now, DateTime.Now);
            objDataTable.Rows.Add(null, "Headphones", "Noise-cancelling headphones", "PRD003", DateTime.Now, DateTime.Now);
            objDataTable.Rows.Add(null, "Monitor", "4K resolution monitor", "PRD004", DateTime.Now, DateTime.Now);
            objDataTable.Rows.Add(null, "Keyboard", "Mechanical keyboard", "PRD005", DateTime.Now, DateTime.Now);
            objDataTable.Rows.Add(null, "Mouse", "Ergonomic wireless mouse", "PRD006", DateTime.Now, DateTime.Now);
            objDataTable.Rows.Add(null, "Tablet", "10-inch display tablet", "PRD007", DateTime.Now, DateTime.Now);
            objDataTable.Rows.Add(null, "Smartwatch", "Fitness tracking smartwatch", "PRD008", DateTime.Now, DateTime.Now);
            objDataTable.Rows.Add(null, "Printer", "All-in-one printer", "PRD009", DateTime.Now, DateTime.Now);
            objDataTable.Rows.Add(null, "Router", "Dual-band Wi-Fi router", "PRD010", DateTime.Now, DateTime.Now);

            EnumerableRowCollection<DataRow> query = from product in objDataTable.AsEnumerable()
                                                     where product.Field<DateTime>("CreatedAt") > new DateTime(2001, 8, 1)
                                                     select product;

            return Ok(query);
        }
    }
}