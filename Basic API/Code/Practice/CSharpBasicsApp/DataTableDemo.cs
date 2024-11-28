using System;
using System.Data;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the use of the DataTable, DataRow, DataColumn, and DataSet classes to create and manipulate data.
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
        DataColumn idColumn = new DataColumn("ID", typeof(int))
        {
            AllowDBNull = false,
            Unique = true,
            AutoIncrement = true,
            AutoIncrementSeed = 1,
            AutoIncrementStep = 1
        };
        table.Columns.Add(idColumn);

        table.Columns.Add(new DataColumn("Name", typeof(string)) { MaxLength = 50 });
        table.Columns.Add(new DataColumn("Age", typeof(int)));
        table.Columns.Add(new DataColumn("Grade", typeof(string)));

        // Add rows to the table using DataRow
        DataRow row1 = table.NewRow();
        row1["Name"] = "Alice";
        row1["Age"] = 20;
        row1["Grade"] = "A";
        table.Rows.Add(row1);

        DataRow row2 = table.NewRow();
        row2["Name"] = "Bob";
        row2["Age"] = 21;
        row2["Grade"] = "B";
        table.Rows.Add(row2);

        table.Rows.Add(null, "Charlie", 19, "A"); // ID auto-generated
        table.Rows.Add(null, "Diana", 22, "C");  // ID auto-generated

        // Display the DataTable structure (columns and rows)
        Console.WriteLine($"DataTable: {table.TableName}");
        DisplayDataTable(table);

        // Filtering data using the Select method (selecting rows where Age > 20)
        Console.WriteLine("\nFiltered Rows (Age > 20):");
        DataRow[] filteredRows = table.Select("Age > 20");
        foreach (DataRow row in filteredRows)
        {
            Console.WriteLine($"{row["ID"]}\t{row["Name"]}\t{row["Age"]}\t{row["Grade"]}");
        }

        // Accessing specific data from a specific row (Row 2, Name column)
        Console.WriteLine("\nAccessing specific data:");
        Console.WriteLine($"Student Name (Row 2): {table.Rows[1]["Name"]}");

        // Demonstrate DataSet containing multiple tables
        DataSet dataSet = new DataSet("School");
        dataSet.Tables.Add(table);

        // Add another DataTable for demonstration
        DataTable coursesTable = new DataTable("Courses");
        coursesTable.Columns.Add("CourseID", typeof(int));
        coursesTable.Columns.Add("CourseName", typeof(string));
        coursesTable.Rows.Add(1, "Mathematics");
        coursesTable.Rows.Add(2, "Physics");
        coursesTable.Rows.Add(3, "Computer Science");
        dataSet.Tables.Add(coursesTable);

        Console.WriteLine("\nDataSet: " + dataSet.DataSetName);
        foreach (DataTable dt in dataSet.Tables)
        {
            Console.WriteLine($"Table: {dt.TableName}");
            DisplayDataTable(dt);
        }

        // Use Clone and Copy methods of DataTable
        DataTable clonedTable = table.Clone(); // Structure only
        Console.WriteLine("\nCloned Table Structure (No Rows):");
        DisplayDataTable(clonedTable);

        DataTable copiedTable = table.Copy(); // Structure and data
        Console.WriteLine("\nCopied Table Structure and Data:");
        DisplayDataTable(copiedTable);
    }

    /// <summary>
    /// Displays the structure and rows of a DataTable.
    /// </summary>
    /// <param name="table">The DataTable to display.</param>
    private static void DisplayDataTable(DataTable table)
    {
        Console.WriteLine("Columns:");
        foreach (DataColumn column in table.Columns)
        {
            Console.Write(column.ColumnName + "\t");
        }
        Console.WriteLine("\nRows:");
        foreach (DataRow row in table.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
        }
    }
}
