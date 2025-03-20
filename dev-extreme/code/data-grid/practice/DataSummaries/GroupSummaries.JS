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
      summary: {
        groupItems: [
          {
            column: "CountryName",
            summaryType: "count",
            displayFormat: "{0} {1}",
            showInGroupFooter: true,
            alignByColumn: true,
            customizeText(itemInfo) {
              return `Total Country : ${itemInfo.value}`;
            },
          },
          {
            column: "StateName",
            summaryType: "count",
            displayFormat: "{0} {1}",
            showInGroupFooter: true,
            alignByColumn: true,
            customizeText(itemInfo) {
              return `Total State : ${itemInfo.value}`;
            },
            showInColumn: "CityName",
          },
          {
            column: "CityName",
            summaryType: "count",
            displayFormat: "{0} {1}",
            showInGroupFooter: true,
            alignByColumn: true,
            customizeText(itemInfo) {
              return `Total City : ${itemInfo.value}`;
            },
          },
        ],
      },
    })
    .dxDataGrid("instance");
});
