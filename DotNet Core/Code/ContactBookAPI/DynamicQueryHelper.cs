namespace ContactBookAPI
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
        /// Generates an UPDATE SQL query for the specified table and columns with dynamic WHERE conditions.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="columns">A dictionary containing column names and their corresponding values.</param>
        /// <param name="whereConditions">A dictionary containing column names and their corresponding values for the WHERE clause.</param>
        /// <returns>A string representing the generated UPDATE SQL query.</returns>
        public static string GenerateUpdateQuery(string tableName, Dictionary<string, object> columns, Dictionary<string, object> whereConditions)
        {
            string setClause = string.Join(", ", columns.Keys.Select(k => $"{k} = @{k}"));
            string whereClause = string.Join(" AND ", whereConditions.Keys.Select(k => $"{k} = @{k}"));
            return $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";
        }

        /// <summary>
        /// Generates a SELECT SQL query for the specified table with dynamic WHERE conditions.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="whereConditions">A dictionary containing column names and their corresponding values for the WHERE clause.</param>
        /// <returns>A string representing the generated SELECT SQL query.</returns>
        public static string GenerateSelectQuery(string tableName, Dictionary<string, object> whereConditions = null)
        {
            if (whereConditions == null || !whereConditions.Any())
            {
                return $"SELECT * FROM {tableName}";
            }

            string whereClause = string.Join(" AND ", whereConditions.Keys.Select(k => $"{k} = @{k}"));
            return $"SELECT * FROM {tableName} WHERE {whereClause}";
        }

        /// <summary>
        /// Generates a DELETE SQL query for the specified table with dynamic WHERE conditions.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="whereConditions">A dictionary containing column names and their corresponding values for the WHERE clause.</param>
        /// <returns>A string representing the generated DELETE SQL query.</returns>
        public static string GenerateDeleteQuery(string tableName, Dictionary<string, object> whereConditions)
        {
            string whereClause = string.Join(" AND ", whereConditions.Keys.Select(k => $"{k} = @{k}"));
            return $"DELETE FROM {tableName} WHERE {whereClause}";
        }

        /// <summary>
        /// Constructs a dictionary of parameters for SQL queries.
        /// </summary>
        /// <param name="columns">A dictionary containing column names and their corresponding values.</param>
        /// <param name="whereConditions">A dictionary containing column names and their corresponding values for the WHERE clause.</param>
        /// <returns>A dictionary of parameters for the SQL query.</returns>
        public static Dictionary<string, object> GetParameters(Dictionary<string, object> columns, Dictionary<string, object> whereConditions = null)
        {
            var parameters = new Dictionary<string, object>(columns);

            if (whereConditions != null)
            {
                foreach (var condition in whereConditions)
                {
                    parameters.Add($"@{condition.Key}", condition.Value);
                }
            }

            return parameters;
        }
    }
}
