$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      keyExpr: "userID",
      dataSource: users,
      customizeColumns: (columns) => {
        columns[0].width = 100;
      },
      allowColumnReordering: true,
      grouping: {
        allowCollapsing: true,
        autoExpandAll: true,
        contextMenuEnabled: true,
        expandMode: "buttonClick",
      },
      searchPanel: {
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
