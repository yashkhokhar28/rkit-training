// Document ready event handler using jQuery shorthand
$(() => {
  // Initialize DataGrid widget and store instance
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      // Set the data source to marvelCharacters array/object
      dataSource: marvelCharacters,

      // Display borders around the entire grid
      showBorders: true,

      // Show vertical lines between columns
      showColumnLines: true,

      // Show horizontal lines between rows
      showRowLines: true,

      // Enable alternating row colors for better readability
      rowAlternationEnabled: true,
    })
    // Get the DataGrid instance
    .dxDataGrid("instance");
});
