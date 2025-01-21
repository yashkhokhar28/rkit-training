using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace StockPortfolioAPI.BL
{
    public class BLConverter
    {
        /// <summary>
        /// Converts a list of objects of type <typeparamref name="T"/> to a DataTable.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="obj">The list of objects to be converted.</param>
        /// <returns>A DataTable containing the data from the provided list.</returns>
        public DataTable ToDataTable<T>(List<T> obj) where T : class
        {
            // Serialize the object list to JSON
            string json = JsonConvert.SerializeObject(obj);

            // Deserialize JSON to a DataTable
            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(json);

            return dataTable;
        }

        /// <summary>
        /// Converts a single object of type <typeparamref name="T"/> to a DataTable.
        /// </summary>
        /// <typeparam name="T">The type of the object to be converted.</typeparam>
        /// <param name="obj">The object to be converted to a DataTable.</param>
        /// <returns>A DataTable containing a single row with the object's properties as columns.</returns>
        public DataTable ObjectToDataTable<T>(T obj) where T : class
        {
            DataTable dataTable = new DataTable();

            // If the object is null, return an empty DataTable
            if (obj == null)
                return dataTable;

            // Get the type of the object and its properties
            Type objectType = typeof(T);
            PropertyInfo[] properties = objectType.GetProperties();

            // Add columns to the DataTable for each property
            foreach (PropertyInfo property in properties)
            {
                // Use the underlying type if the property is nullable
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            // Create a new row and populate it with the object's property values
            DataRow row = dataTable.NewRow();
            foreach (PropertyInfo property in properties)
            {
                row[property.Name] = property.GetValue(obj) ?? DBNull.Value; // Handle null values
            }

            // Add the row to the DataTable
            dataTable.Rows.Add(row);

            return dataTable;
        }
    }
}