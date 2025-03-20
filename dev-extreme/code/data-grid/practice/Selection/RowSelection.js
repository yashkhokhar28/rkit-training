$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      showBorders: true,
      selection: {
        mode: "multiple",
        selectAllMode: "page",
        showCheckBoxesMode: "always",
        allowSelectAll: true,
      },
      hoverStateEnabled: true,
      onSelectionChanged(selectedItems) {
        selectedItems.selectedRowsData.forEach((data) => {
          console.log(`Row with Name ${data["Name"]} Selected`);
        });
      },
      columns: [
        {
          dataField: "Name",
          caption: "Character Name",
          dataType: "string",
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
        },
        {
          dataField: "First_Appearance",
          caption: "First Appearance",
          dataType: "number",
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
        },
      ],
    })
    .dxDataGrid("instance");
});
