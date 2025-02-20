$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      showBorders: true,
      showColumnLines: true,
      showRowLines: true,
      rowAlternationEnabled: true,
    })
    .dxDataGrid("instance");
});
