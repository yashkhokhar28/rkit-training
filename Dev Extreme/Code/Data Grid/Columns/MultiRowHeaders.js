$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      showBorders: true,
      keyExpr: "ID",
      columns: [
        {
          caption: "Names",
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
        },
        {
          dataField: "First_Appearance",
          caption: "First Appearance",
          dataType: "number",
          alignment: "center",
        },
        {
          dataField: "Abilities",
          caption: "Superpowers",
          dataType: "string",
          alignment: "center",
        },
        {
          dataField: "Status",
          caption: "Status",
          dataType: "string",
          alignment: "center",
        },
      ],
    })
    .dxDataGrid("instance");
});
