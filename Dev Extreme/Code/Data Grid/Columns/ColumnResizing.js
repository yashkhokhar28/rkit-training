$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      showBorders: true,
      keyExpr: "ID",
      allowColumnResizing: true,
      showBorders: true,
      columnMinWidth: 50,
      columnAutoWidth: true,
      allowColumnResizing: true,
      columnResizingMode: "widget",
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
          caption: "Team/Group",
          dataType: "string",
          alignment: "center",
          width: 100,
        },
        {
          dataField: "First_Appearance",
          caption: "First Appearance",
          dataType: "number",
          alignment: "center",
          columnMinWidth: 70,
        },
        {
          dataField: "Abilities",
          caption: "Superpowers",
          dataType: "string",
          alignment: "center",
          columnMinWidth: 70,
        },
        {
          dataField: "Status",
          caption: "Status",
          dataType: "string",
          columnMinWidth: 70,
          alignment: "center",
        },
      ],
    })
    .dxDataGrid("instance");
});
