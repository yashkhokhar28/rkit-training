$(() => {
  // Initialize the DataGrid widget instance
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      // Set the data source for the grid
      dataSource: marvelCharacters,

      // Display borders around the grid
      showBorders: true,

      // Specify the unique key field for each data row
      keyExpr: "ID",

      // Allow users to resize columns
      allowColumnResizing: true,

      // Set the minimum width for columns
      columnMinWidth: 50,

      // Enable automatic adjustment of column widths
      columnAutoWidth: true,

      // Set the column resizing mode to "widget"
      columnResizingMode: "widget",

      // Define the columns for the grid
      columns: [
        {
          // Group column for names
          caption: "Names",

          // Set the minimum width for this column group
          columnMinWidth: 70,

          // Center-align the header text for the group
          alignment: "center",

          // Define nested columns within the group
          columns: [
            {
              // Column for the character's name
              dataField: "Name",

              // Set the header caption for the column
              caption: "Character Name",

              // Center-align the cell content
              alignment: "center",
            },

            {
              // Column for the character's real name
              dataField: "Real_Name",

              // Set the header caption for the column
              caption: "Real Name",

              // Center-align the cell content
              alignment: "center",
            },
          ],
        },

        {
          // Column for team or group information
          dataField: "Affiliation",

          // Set the header caption for the column
          caption: "Team/Group",

          // Specify the data type for the column
          dataType: "string",

          // Center-align the cell content
          alignment: "center",

          // Set a fixed width for the column
          width: 100,
        },

        {
          // Column for displaying the character's first appearance
          dataField: "First_Appearance",

          // Set the header caption for the column
          caption: "First Appearance",

          // Specify the data type for the column
          dataType: "number",

          // Center-align the cell content
          alignment: "center",

          // Set the minimum width for the column
          columnMinWidth: 70,
        },

        {
          // Column for displaying character abilities
          dataField: "Abilities",

          // Set the header caption for the column
          caption: "Superpowers",

          // Specify the data type for the column
          dataType: "string",

          // Center-align the cell content
          alignment: "center",

          // Set the minimum width for the column
          columnMinWidth: 70,
        },

        {
          // Column for displaying the character's status
          dataField: "Status",

          // Set the header caption for the column
          caption: "Status",

          // Specify the data type for the column
          dataType: "string",

          // Set the minimum width for the column
          columnMinWidth: 70,

          // Center-align the cell content
          alignment: "center",
        },
      ],
    })
    .dxDataGrid("instance");
});
