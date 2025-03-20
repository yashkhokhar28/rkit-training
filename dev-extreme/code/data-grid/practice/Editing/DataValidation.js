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
        mode: "cell",
        allowAdding: true,
        allowDeleting: true,
        allowUpdating: true,
        confirmDelete: true,
        useIcons: true,
        selectTextOnEditStart: true,
        startEditAction: "click",
      },
      scrolling: {
        mode: "virtual",
      },
      toolbar: {
        items: [
          {
            name: "addRowButton",
            showText: "always",
          },
          {
            location: "after",
            widget: "dxButton",
            options: {
              text: "Delete Selected Records",
              icon: "trash",
              disabled: true,
              onClick() {
                dataGrid.getSelectedRowKeys().forEach((key) => {
                  marvelCharacters.remove(key);
                });
                dataGrid.refresh();
              },
            },
          },
        ],
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
      ],
    })
    .dxDataGrid("instance");
});
