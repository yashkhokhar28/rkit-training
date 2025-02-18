$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      showBorders: true,
      keyExpr: "ID",
    })
    .dxDataGrid("instance");
});
