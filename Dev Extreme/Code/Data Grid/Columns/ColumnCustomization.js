$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      editing: {
        mode: "batch",
        allowAdding: true,
        allowUpdating: true,
        allowDeleting: true,
      },
      showBorders: true,
      keyExpr: "ID",
      allowColumnReordering: true,
      allowColumnResizing: true,
      columnAutoWidth: true,
      columnChooser: {
        enabled: true,
        allowSearch: true,
        mode: "dragAndDrop",
      },
      columnFixing: {
        enabled: true,
      },
      cellHintEnabled: true,
      columnHidingEnabled: false,
      columnMinWidth: 10,
      columnResizingMode: "nextColumn",
      filterRow: {
        visible: true,
        applyFilter: "onClick",
        showOperationChooser: true,
      },
      columns: [
        {
          caption: "Names",
          columnMinWidth: 70,
          alignment: "center",
          columns: [
            {
              dataField: "Name",
              caption: "Character Name",
              alignment: "center",
            },
            {
              dataField: "Real_Name",
              caption: "Real Name",
              alignment: "center",
            },
          ],
        },
        {
          dataField: "Affiliation",
          customizeText: function (cellInfo) {
            return cellInfo.value + " &deg;C";
          },
          caption: "Team/Group",
          dataType: "string",
          alignment: "center",
          width: 100,
          headerFilter: {
            allowSearch: true,
            searchMode: "startswith",
          },
        },
        {
          dataField: "First_Appearance",
          caption: "First Appearance",
          dataType: "number",
          alignment: "center",
          columnMinWidth: 70,
          format: {
            type: "fixedPoint",
            precision: 2,
          },
          headerFilter: {
            allowSearch: true,
            searchMode: "startswith",
          },
          hidingPriority: 0,
        },
        {
          dataField: "Abilities",
          caption: "Superpowers",
          dataType: "string",
          alignment: "center",
          columnMinWidth: 70,
          filterType: "include",
          fixed: true,
          fixedPosition: "left",
        },
        {
          dataField: "Status",
          caption: "Status",
          dataType: "string",
          columnMinWidth: 70,
          alignment: "center",
          allowEditing: false,
          allowExporting: true,
          allowFiltering: false,
          allowFixing: false,
          allowGrouping: false,
          allowHeaderFiltering: false,
          allowHiding: false,
          allowReordering: false,
          allowResizing: false,
          allowSearch: false,
          allowSorting: false,
          encodeHtml: true,
        },

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
                alert(e.row.data.Name);
              },
            },
          ],
        },
      ],
    })
    .dxDataGrid("instance");
});
