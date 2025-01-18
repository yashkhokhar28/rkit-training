using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace CSharpAdvanceApp
{
    /// <summary>
    /// This class demonstrates serialization and deserialization of a ProductModel object 
    /// into both JSON and XML formats.
    /// </summary>
    public class DataSerializationDemo
    {
        /// <summary>
        /// The main method to run data serialization and deserialization processes.
        /// It demonstrates JSON and XML serialization/deserialization using a sample ProductModel object.
        /// </summary>
        public void RunDataSerializationDemo()
        {
            // Creating a sample ProductModel object
            ProductModel objProductModel = new ProductModel
            {
                ProductID = 11,
                ProductName = "Product 11",
                ProductDescription = "Description 11",
                ProductCode = "P011",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            // JSON Serialization and Deserialization
            Console.WriteLine("=== JSON Serialization ===");
            string productJson = SerializationHelper.ConvertObjectToJson(objProductModel);
            Console.WriteLine(productJson); // Output the JSON string

            // Deserialize the JSON string back to ProductModel object
            ProductModel deserializedProductJson = DeSerializationHelper.ConvertJsonToObject(productJson);
            Console.WriteLine("\nDeserialized from JSON:");
            Console.WriteLine(deserializedProductJson); // Output the deserialized object

            // XML Serialization and Deserialization
            Console.WriteLine("\n=== XML Serialization ===");
            string productXml = SerializationHelper.ConvertObjectToXml(objProductModel);
            Console.WriteLine(productXml); // Output the XML string

            // Deserialize the XML string back to ProductModel object
            ProductModel deserializedProductXml = DeSerializationHelper.ConvertXmlToObject(productXml);
            Console.WriteLine("\nDeserialized from XML:");
            Console.WriteLine(deserializedProductXml); // Output the deserialized object
        }
    }

    public static class SerializationHelper
    {
        /// <summary>
        /// Serializes a ProductModel object to a JSON string.
        /// </summary>
        /// <param name="objProductModel">The ProductModel object to serialize.</param>
        /// <returns>A JSON string representing the ProductModel object.</returns>
        public static string ConvertObjectToJson(ProductModel objProductModel)
        {
            return JsonConvert.SerializeObject(objProductModel, Formatting.Indented);
        }

        /// <summary>
        /// Serializes a ProductModel object to an XML string.
        /// </summary>
        /// <param name="objProductModel">The ProductModel object to serialize.</param>
        /// <returns>An XML string representing the ProductModel object.</returns>
        public static string ConvertObjectToXml(ProductModel objProductModel)
        {
            try
            {
                XmlSerializer objXmlSerializer = new XmlSerializer(typeof(ProductModel));
                using StringWriter objStringWriter = new StringWriter();
                objXmlSerializer.Serialize(objStringWriter, objProductModel);
                return objStringWriter.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during XML serialization: {ex.Message}");
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class DeSerializationHelper
    {
        /// <summary>
        /// Deserializes a JSON string back into a ProductModel object.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>A ProductModel object represented by the JSON string.</returns>
        public static ProductModel ConvertJsonToObject(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<ProductModel>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during JSON deserialization: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Deserializes an XML string back into a ProductModel object.
        /// </summary>
        /// <param name="xml">The XML string to deserialize.</param>
        /// <returns>A ProductModel object represented by the XML string.</returns>
        public static ProductModel ConvertXmlToObject(string xml)
        {
            try
            {
                XmlSerializer objXmlSerializer = new XmlSerializer(typeof(ProductModel));
                using StringReader objStringReader = new StringReader(xml);
                return (ProductModel)objXmlSerializer.Deserialize(objStringReader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during XML deserialization: {ex.Message}");
                return null;
            }
        }
    }
}
