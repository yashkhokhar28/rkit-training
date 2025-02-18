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
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      showBorders: true,
      refreshMode: "full",
      editing: {
        mode: "row",
        allowAdding: true,
        allowDeleting: true,
        allowUpdating: true,
        confirmDelete: true,
        useIcons: true,
      },
      onEditingStart: onEditingStart,
      onInitNewRow: onInitNewRow,
      onRowInserting: onRowInserting,
      onRowInserted: onRowInserted,
      onRowUpdating: onRowUpdating,
      onRowUpdated: onRowUpdated,
      onRowRemoving: onRowRemoving,
      onRowRemoved: onRowRemoved,
      onSaving: onSaving,
      onSaved: onSaved,
      onEditCanceling: onEditCanceling,
      onEditCanceled: onEditCanceled,
      columns: [
        "Name",
        "Real_Name",
        "Affiliation",
        "First_Appearance",
        "Abilities",
        "Status",
        {
          caption: "Buttons",
          type: "buttons",
          buttons: [
            {
              text: "My Command",
              icon: "edit",
              hint: "Edit Button",
            },
            {
              text: "My Command",
              icon: "trash",
              hint: "Delete Button",
            },
            {
              text: "My Command",
              icon: "info",
              hint: "",
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
