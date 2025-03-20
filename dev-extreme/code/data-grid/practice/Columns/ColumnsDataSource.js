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
    })
    .dxDataGrid("instance");
});
