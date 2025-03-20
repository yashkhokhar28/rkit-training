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
        mode: "form",
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
        {
          dataField: "Name",
          caption: "Character Name",
          dataType: "string",
          validationRules: [{ type: "required", message: "Name is required" }],
        },
        {
          dataField: "Real_Name",
          caption: "Real Name",
          dataType: "string",
        },
        {
          dataField: "Affiliation",
          caption: "Team/Group",
          dataType: "string",
          validationRules: [
            { type: "required", message: "Affiliation is required" },
          ],
        },
        {
          dataField: "First_Appearance",
          caption: "First Appearance",
          dataType: "number",
          validationRules: [
            { type: "required", message: "Year is required" },
            {
              type: "range",
              min: 1900,
              max: 2025,
              message: "Enter a valid year",
            },
          ],
        },
        {
          dataField: "Abilities",
          caption: "Superpowers",
          dataType: "string",
        },
        {
          dataField: "Status",
          caption: "Status",
          dataType: "string",
          validationRules: [
            { type: "required", message: "Status is required" },
          ],
        },
      ],
    })
    .dxDataGrid("instance");
});
