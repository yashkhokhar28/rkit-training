using System.Data;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the use of the DataTable class to create and manipulate a data table.
/// </summary>
public class DataTableDemo
{
    /// <summary>
    /// Executes the DataTable operations demonstration, including adding columns, rows, filtering, and accessing specific data.
    /// </summary>
    public static void RunDataTableDemo()
    {
        // Create a new DataTable named "Students"
        DataTable table = new DataTable("Students");

        // Define columns and add them to the table
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Age", typeof(int));
        table.Columns.Add("Grade", typeof(string));

        // Add rows to the table
        table.Rows.Add(1, "Alice", 20, "A");
        table.Rows.Add(2, "Bob", 21, "B");
        table.Rows.Add(3, "Charlie", 19, "A");
        table.Rows.Add(4, "Diana", 22, "C");

        // Display the structure of the DataTable (columns)
        Console.WriteLine("DataTable: " + table.TableName);
        Console.WriteLine("Columns: ");
        foreach (DataColumn column in table.Columns)
        {
            // Display each column's name
            Console.Write(column.ColumnName + "\t");
        }

        // Display the rows of the DataTable
        Console.WriteLine("\nRows: ");
        foreach (DataRow row in table.Rows)
        {
            // Display each row's values
            foreach (var item in row.ItemArray)
            {
                Console.Write(item + "\t");
            }

            Console.WriteLine();
        }

        // Filtering data using the Select method (selecting rows where Age > 20)
        Console.WriteLine("\nFiltered Rows (Age > 20):");
        DataRow[] filteredRows = table.Select("Age > 20");
        foreach (DataRow row in filteredRows)
        {
            // Display the filtered rows
            Console.WriteLine($"{row["ID"]}\t{row["Name"]}\t{row["Age"]}\t{row["Grade"]}");
        }

        // Accessing specific data from a specific row (Row 2, Name column)
        Console.WriteLine("\nAccessing specific data:");
        Console.WriteLine($"Student Name (Row 2): {table.Rows[1]["Name"]}");
    }
}