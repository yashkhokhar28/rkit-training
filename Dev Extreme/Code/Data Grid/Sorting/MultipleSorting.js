$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      showBorders: true,
      sorting: {
        mode: "multiple",
        showSortIndexes: true,
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
          sortIndex: 1,
          sortOrder: "desc",
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
          sortIndex: 2,
          sortOrder: "asc",
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
