// Document ready event handler using jQuery shorthand
$(() => {
  // Initialize the DataGrid widget and store its instance
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      // Define the data source (could be an array or object of data items)
      dataSource: marvelCharacters,

      // Editing configuration for inline modifications
      editing: {
        // Set the editing mode to "batch" so changes are saved together
        mode: "batch",

        // Enable adding new rows to the grid
        allowAdding: true,

        // Enable updating existing rows
        allowUpdating: true,

        // Enable deleting rows
        allowDeleting: true,
      },

      // Display borders around the entire grid
      showBorders: true,
      headerFilter: {
        visible: true,
        allowSearch: true,
      },

      // Specify the unique key field for each data row
      keyExpr: "ID",

      // Allow users to reorder columns via drag and drop
      allowColumnReordering: true,

      // Allow users to resize columns by dragging their edges
      allowColumnResizing: true,

      // Automatically adjust column widths based on their content
      columnAutoWidth: true,

      // Column chooser configuration to enable column selection at runtime
      columnChooser: {
        // Enable the column chooser feature
        enabled: true,

        // Allow users to search within the column chooser
        allowSearch: true,

        // Set the interaction mode to drag and drop for reordering columns
        mode: "dragAndDrop",
      },

      // Column fixing configuration to freeze columns in place
      columnFixing: {
        // Enable fixed (frozen) columns
        enabled: true,
      },

      // Enable tooltips on cells when their content is truncated
      cellHintEnabled: true,

      // Disable column hiding to ensure all columns remain visible
      columnHidingEnabled: false,

      // Set a minimum width (in pixels) for columns
      columnMinWidth: 10,

      // Define column resizing behavior relative to the next column / widget
      columnResizingMode: "nextColumn",

      // Filter row configuration for inline data filtering
      filterRow: {
        // Make the filter row visible
        visible: true,

        // Apply filters only when the user clicks the "Apply" button
        applyFilter: "onClick",

        // Show an operation chooser (e.g., "contains", "startswith") in the filter row
        showOperationChooser: true,
      },

      // Define the columns that will appear in the grid
      columns: [
        {
          // Group column for organizing name-related fields
          caption: "Names",

          // Set a minimum width for this group
          columnMinWidth: 70,

          // Center-align the header text
          alignment: "center",

          // Define nested columns within the group
          columns: [
            {
              // Column for the character's name
              dataField: "Name",

              // Header caption for the column
              caption: "Character Name",

              // Center-align the cell content
              alignment: "center",
            },

            {
              // Column for the character's real name
              dataField: "Real_Name",

              // Header caption for the column
              caption: "Real Name",

              // Center-align the cell content
              alignment: "center",
            },
          ],
        },

        {
          // Column for team or affiliation information
          dataField: "Affiliation",

          // Custom function to adjust how cell text is displayed
          customizeText: function (cellInfo) {
            // Append a degree symbol (°C) as an example (ensure this is contextually appropriate)
            return cellInfo.value + " °C";
          },

          // Header caption for the column
          caption: "Team/Group",

          // Data type for the column
          dataType: "string",

          // Center-align the cell content
          alignment: "center",

          // Set a fixed width for the column
          width: 100,

          // Header filter configuration for the column
          headerFilter: {
            // Enable searching within the header filter
            allowSearch: true,

            // Set the search mode to "startswith" for matching beginnings of strings
            searchMode: "startswith",
          },
        },

        {
          // Column for displaying the character's first appearance
          dataField: "First_Appearance",

          // Header caption for the column
          caption: "First Appearance",

          // Data type for the column
          dataType: "number",

          // Center-align the cell content
          alignment: "center",

          // Set a minimum width for the column
          columnMinWidth: 70,

          // Number formatting configuration
          format: {
            // Format type: fixed point notation
            type: "fixedPoint",

            // Set precision to two decimal places
            precision: 2,
          },

          // Header filter configuration for the column
          headerFilter: {
            visible: true,
            // Enable searching within the header filter
            allowSearch: true,

            // Set the search mode to "startswith" for matching beginnings of strings
            searchMode: "startswith",
          },

          // Specifies the order in which columns are hidden when the UI component adapts to the screen or container size. Ignored if allowColumnResizing is true and columnResizingMode is "widget".
          // Set a hiding priority (lower value means this column hides first when space is limited)
          hidingPriority: 0,
        },

        {
          // Column for character abilities
          dataField: "Abilities",

          // Header caption for the column
          caption: "Superpowers",

          // Data type for the column
          dataType: "string",

          // Center-align the cell content
          alignment: "center",

          // Set a minimum width for the column
          columnMinWidth: 70,

          // Specify filtering mode based on inclusion
          filterType: "include",

          // Fix the column position during horizontal scrolling
          fixed: true,

          // Set the fixed position to the left side of the grid
          fixedPosition: "left",
        },

        {
          // Column for character status with restrictions on user modifications
          dataField: "Status",

          // Header caption for the column
          caption: "Status",

          // Data type for the column
          dataType: "string",

          // Set a minimum width for the column
          columnMinWidth: 70,

          // Center-align the cell content
          alignment: "center",

          // Disable editing for this column
          allowEditing: false,

          // Permit exporting data from this column
          allowExporting: true,

          // Disable filtering to prevent user interaction
          allowFiltering: false,

          // Prevent this column from being fixed or grouped
          allowFixing: false,

          // Disable grouping functionality for this column
          allowGrouping: false,

          // Disable header filtering for a consistent display
          allowHeaderFiltering: false,

          // Do not allow the column to be hidden
          allowHiding: false,

          // Disable column reordering to lock its position
          allowReordering: false,

          // Disable resizing to maintain a fixed width
          allowResizing: false,

          // Exclude this column from search operations
          allowSearch: false,

          // Disable sorting to maintain a static order
          allowSorting: false,

          // Encode HTML to ensure safe content display
          encodeHtml: true,
        },

        {
          // Column for command buttons that trigger custom actions
          caption: "Buttons",

          // Specify that this column contains command buttons
          type: "buttons",

          // Define the buttons to be rendered in this column
          buttons: [
            {
              // Button for editing actions
              text: "My Command",

              // Icon for the button
              icon: "edit",

              // Tooltip hint for the button
              hint: "Edit Button",
            },

            {
              // Button for deletion actions
              text: "My Command",

              // Icon for the button
              icon: "trash",

              // Tooltip hint for the button
              hint: "Delete Button",
            },

            {
              // Button to display additional information
              text: "My Command",

              // Icon for the button
              icon: "info",

              // Tooltip hint (empty in this case)
              hint: "",

              // Custom onClick handler to perform an action when clicked
              onClick: function (e) {
                // Display an alert showing the character's name
                alert(e.row.data.Name);
              },
            },
          ],
        },
      ],
    })
    .dxDataGrid("instance");
});
