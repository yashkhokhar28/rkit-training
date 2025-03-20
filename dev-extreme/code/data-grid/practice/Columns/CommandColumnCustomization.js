import {
  onEditCanceled,
  onEditCanceling,
  onEditingStart,
  onInitNewRow,
  onRowInserted,
  onRowInserting,
  onRowRemoved,
  onRowRemoving,
  onRowUpdated,
  onRowUpdating,
  onSaved,
  onSaving,
} from "../Event.js";

$(() => {
  // Initialize the DataGrid widget instance
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      // Set the data source for the grid
      dataSource: marvelCharacters,

      // Display borders around the grid
      showBorders: true,

      // Set the grid's refresh mode to "full"
      refreshMode: "full",

      // Configure editing behavior for the grid
      editing: {
        // Set the editing mode to "row"
        mode: "row",

        // Enable adding new rows
        allowAdding: true,

        // Enable deleting rows
        allowDeleting: true,

        // Enable updating rows
        allowUpdating: true,

        // Ask for confirmation before deleting a row
        confirmDelete: true,

        // Use icons for editing actions
        useIcons: true,
      },

      // Event handler: Triggered when editing starts
      onEditingStart: onEditingStart,

      // Event handler: Triggered when initializing a new row
      onInitNewRow: onInitNewRow,

      // Event handler: Triggered before a row is inserted
      onRowInserting: onRowInserting,

      // Event handler: Triggered after a row has been inserted
      onRowInserted: onRowInserted,

      // Event handler: Triggered before a row is updated
      onRowUpdating: onRowUpdating,

      // Event handler: Triggered after a row has been updated
      onRowUpdated: onRowUpdated,

      // Event handler: Triggered before a row is removed
      onRowRemoving: onRowRemoving,

      // Event handler: Triggered after a row has been removed
      onRowRemoved: onRowRemoved,

      // Event handler: Triggered before saving changes
      onSaving: onSaving,

      // Event handler: Triggered after changes have been saved
      onSaved: onSaved,

      // Event handler: Triggered when edit canceling starts
      onEditCanceling: onEditCanceling,

      // Event handler: Triggered after edit cancelation is complete
      onEditCanceled: onEditCanceled,

      // Define the columns for the grid
      columns: [
        "Name",

        "Real_Name",

        "Affiliation",

        "First_Appearance",

        "Abilities",

        "Status",

        {
          // Column configuration for command buttons
          caption: "Buttons",

          // Specify that this column contains buttons
          type: "buttons",

          // Define the buttons within this column
          buttons: [
            {
              // Button configuration for editing
              text: "My Command",

              // Icon for the edit button
              icon: "edit",

              // Tooltip hint for the edit button
              hint: "Edit Button",
            },

            {
              // Button configuration for deletion
              text: "My Command",

              // Icon for the delete button
              icon: "trash",

              // Tooltip hint for the delete button
              hint: "Delete Button",
            },

            {
              // Button configuration for additional information
              text: "My Command",

              // Icon for the info button
              icon: "info",

              // Tooltip hint (empty in this case)
              hint: "",

              // Custom onClick handler for the info button
              onClick: function (e) {
                console.log(e.row.data.Name);
              },
            },
          ],
        },
      ],
    })
    .dxDataGrid("instance");
});
