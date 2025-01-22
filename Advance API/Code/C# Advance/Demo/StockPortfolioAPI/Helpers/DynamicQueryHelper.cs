using System.Collections.Generic;
using System.Linq;

namespace StockPortfolioAPI.Helpers
{
    /// <summary>
    /// Provides helper methods to generate dynamic SQL queries.
    /// </summary>
    public static class DynamicQueryHelper
    {
        /// <summary>
        /// Generates an INSERT SQL query for the specified table and columns.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="columns">A dictionary containing column names and their corresponding values.</param>
        /// <returns>A string representing the generated INSERT SQL query.</returns>
        public static string GenerateInsertQuery(string tableName, Dictionary<string, object> columns)
        {
            string columnNames = string.Join(", ", columns.Keys);
            string valuePlaceholders = string.Join(", ", columns.Keys.Select(k => "@" + k));
            return $"INSERT INTO {tableName} ({columnNames}) VALUES ({valuePlaceholders})";
        }

        /// <summary>
        /// Generates an UPDATE SQL query for the specified table and columns.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="columns">A dictionary containing column names and their corresponding values.</param>
        /// <param name="idColumn">The name of the column used to identify the row to update.</param>
        /// <returns>A string representing the generated UPDATE SQL query.</returns>
        public static string GenerateUpdateQuery(string tableName, Dictionary<string, object> columns, string idColumn)
        {
            string setClause = string.Join(", ", columns.Keys.Select(k => $"{k} = @{k}"));
            return $"UPDATE {tableName} SET {setClause} WHERE {idColumn} = @{idColumn}";
        }

        /// <summary>
        /// Generates a SELECT SQL query for the specified table with an optional WHERE clause.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="whereClause">The optional WHERE clause to filter the results.</param>
        /// <returns>A string representing the generated SELECT SQL query.</returns>
        public static string GenerateSelectQuery(string tableName, string whereClause = "")
        {
            return string.IsNullOrEmpty(whereClause)
                ? $"SELECT * FROM {tableName}"
                : $"SELECT * FROM {tableName} WHERE {whereClause}";
        }

        /// <summary>
        /// Generates a DELETE SQL query for the specified table with a WHERE clause.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="whereClause">The WHERE clause to identify the rows to delete.</param>
        /// <returns>A string representing the generated DELETE SQL query.</returns>
        public static string GenerateDeleteQuery(string tableName, string whereClause)
        {
            return $"DELETE FROM {tableName} WHERE {whereClause}";
        }

        /// <summary>
        /// Constructs a dictionary of parameters for SQL queries.
        /// </summary>
        /// <param name="columns">A dictionary containing column names and their corresponding values.</param>
        /// <param name="id">An optional ID value to be included in the parameters.</param>
        /// <returns>A dictionary of parameters for the SQL query.</returns>
        public static Dictionary<string, object> GetParameters(Dictionary<string, object> columns, int? id = null)
        {
            var parameters = new Dictionary<string, object>(columns);
            if (id.HasValue)
            {
                parameters.Add($"@{columns.Keys.First()}", id.Value);
            }
            return parameters;
        }
    }
}