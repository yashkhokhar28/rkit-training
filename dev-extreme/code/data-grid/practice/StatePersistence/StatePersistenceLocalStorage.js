$(() => {
  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      showBorders: true,
      keyExpr: "ID",
      stateStoring: {
        enabled: true,
        type: "localStorage",
        storageKey: "localStorageKey",
      },
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
          //   visible: false,
        },
      ],
    })
    .dxDataGrid("instance");

  $("#hide").dxButton({
    text: "Hide Column",
    onClick: function () {
      dataGrid.columnOption(6, "visible", false);
    },
  });

  $("#load").dxButton({
    text: "Load State",
    onClick: function () {
      const state = JSON.parse(localStorage.getItem("localStorageKey"));
      dataGrid.state(state);
    },
  });
});
