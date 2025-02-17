$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      keyExpr: "userID",
      dataSource: users,
      customizeColumns: (columns) => {
        columns[0].width = 100;
      },
      allowColumnReordering: true,
      filterRow: {
        visible: true,
        applyFilter: "onClick",
        showOperationChooser: true,
      },
      searchPanel: {
        visible: true,
      },
      headerFilter: {
        visible: true,
        allowSearch: true,
      },
      filterPanel: {
        visible: true,
      },
      paging: {
        enabled: true,
        pageSize: 10,
      },
      groupPanel: {
        visible: true,
      },
    })
    .dxDataGrid("instance");
});
