$(() => {
  const storageKey = "customKey"; // Define storage key

  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      showBorders: true,
      keyExpr: "ID",
      filterPanel: {
        filterEnabled: true,
        visible: true,
      },

      stateStoring: {
        enabled: true,
        type: "custom",
        storageKey: storageKey,
        customLoad() {
          return new Promise((resolve) => {
            const savedState = localStorage.getItem(storageKey);
            resolve(savedState ? JSON.parse(savedState) : null);
          });
        },
        customSave(state) {
          console.log("Saving state before modification:", state);

          if (state) {
            state.customProperty = "Custom Value";
            state.pageSize = 50;
            state.columns.forEach((column) => {
              if (column.dataField === "Status") {
                column.sortOrder = "desc";
              }
            });
          }

          console.log("Saving state after modification:", state);
          localStorage.setItem(storageKey, JSON.stringify(state));
        },
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
        },
      ],
    })
    .dxDataGrid("instance");
});
